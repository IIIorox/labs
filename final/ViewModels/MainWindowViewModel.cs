using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using final.Models;
using final.Services;
using System.Windows.Input;
using final.ViewModels.Base;
using final.Commands;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using System.Windows;

namespace final.ViewModels
{

    public class StatusFilterItem
    {
        public string Name { get; set; } = string.Empty;
        public bool? Value { get; set; }
    }
    class MainWindowViewModel : ViewModel
    {
        private readonly TaskService _taskService;

        public ObservableCollection<UserTask> Tasks { get; } = new();

        private UserTask? _selectedTask;
        private UserTask? _editTaskBackup;
        public UserTask? SelectedTask
        {
            get => _selectedTask;
            set
            {
                if (Set(ref _selectedTask, value))
                {
                    if (_selectedTask != null)
                    {
                        _editTaskBackup = new UserTask
                        {
                            Id = _selectedTask.Id,
                            Title = _selectedTask.Title,
                            Description = _selectedTask.Description,
                            IsCompleted = _selectedTask.IsCompleted,
                            CreatedAt = _selectedTask.CreatedAt,
                            UserId = _selectedTask.UserId,
                            User = _selectedTask.User
                        };
                    }
                    else
                    {
                        _editTaskBackup = null;
                    }
                }
            }
        }

        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (Set(ref _searchText, value))
                    ApplyFilter();
            }
        }

        public ObservableCollection<StatusFilterItem> StatusFilterItems { get; } = new()
        {
            new StatusFilterItem { Name = "Все", Value = null },
            new StatusFilterItem { Name = "Выполнена", Value = true },
            new StatusFilterItem { Name = "Не выполнена", Value = false }
        };

        private bool _isFilterPanelOpen;
        public bool IsFilterPanelOpen
        {
            get => _isFilterPanelOpen;
            set => Set(ref _isFilterPanelOpen, value);
        }

        private bool? _selectedStatusFilter = null;
        public bool? SelectedStatusFilter
        {
            get => _selectedStatusFilter;
            set
            {
                if (Set(ref _selectedStatusFilter, value))
                    ApplyFilter();
            }
        }

        private DateTime? _createdFromFilter;
        public DateTime? CreatedFromFilter
        {
            get => _createdFromFilter;
            set
            {
                if (Set(ref _createdFromFilter, value))
                    ApplyFilter();
            }
        }

        private DateTime? _createdToFilter;
        public DateTime? CreatedToFilter
        {
            get => _createdToFilter;
            set
            {
                if (Set(ref _createdToFilter, value))
                    ApplyFilter();
            }
        }

        private ICollectionView? _tasksView;
        public ICollectionView? TasksView
        {
            get => _tasksView;
            private set => Set(ref _tasksView, value);
        }

        public ICommand AddTaskCommand { get; }
        public ICommand SaveTaskCommand { get; }
        public ICommand CancelEditCommand { get; }
        public ICommand DeleteTaskCommand { get; }
        public ICommand ApplyFilterCommand { get; }
        public ICommand ResetFilterCommand { get; }
        public MainWindowViewModel()
        {
            _taskService = new TaskService(new Database.AppDbContext());

            AddTaskCommand = new RelayCommand(async () => await AddTaskAsync());
            SaveTaskCommand = new RelayCommand(async () => await SaveTaskAsync(), () => SelectedTask != null);
            CancelEditCommand = new RelayCommand(CancelEdit);
            DeleteTaskCommand = new RelayCommand(async () => await DeleteTaskAsync(), () => SelectedTask != null);

            ApplyFilterCommand = new RelayCommand(ApplyFilter);
            ResetFilterCommand = new RelayCommand(ResetFilters);

            LoadTasksAsync();
        }

        private async void LoadTasksAsync()
        {
            var tasksFromDb = await _taskService.GetTasksAsync();

            Tasks.Clear();
            foreach (var task in tasksFromDb)
                Tasks.Add(task);

            TasksView = CollectionViewSource.GetDefaultView(Tasks);
            TasksView.Filter = FilterTasks;
        }

        private bool FilterTasks(object obj)
        {
            if (obj is not UserTask task)
                return false;

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                if (!task.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                    return false;
            }

            if (SelectedStatusFilter.HasValue)
            {
                if (task.IsCompleted != SelectedStatusFilter.Value)
                    return false;
            }

            if (CreatedFromFilter.HasValue && task.CreatedAt < CreatedFromFilter.Value.Date)
                return false;
            if (CreatedToFilter.HasValue && task.CreatedAt > CreatedToFilter.Value.Date.AddDays(1).AddTicks(-1))
                return false;

            return true;
        }

        private void ApplyFilter()
        {
            IsFilterPanelOpen = false;
            TasksView?.Refresh();
        }

        private void ResetFilters()
        {
            SearchText = string.Empty;
            SelectedStatusFilter = null;
            CreatedFromFilter = null;
            CreatedToFilter = null;
            IsFilterPanelOpen = false;
            ApplyFilter();
        }

        private async Task AddTaskAsync()
        {
            var newTask = await _taskService.CreateTaskAsync("Новая задача", "Описание...");
            Tasks.Insert(0, newTask);
            SelectedTask = newTask;
        }

        private async Task SaveTaskAsync()
        {
            if (SelectedTask != null)
            {
                await _taskService.UpdateTaskAsync(SelectedTask);
                SelectedTask = null;
            }
        }

        private void CancelEdit()
        {
            if (SelectedTask == null || _editTaskBackup == null) return;

            SelectedTask.Title = _editTaskBackup.Title;
            SelectedTask.Description = _editTaskBackup.Description;
            SelectedTask.IsCompleted = _editTaskBackup.IsCompleted;

            SelectedTask = null;
        }

        private async Task DeleteTaskAsync()
        {
            if (SelectedTask == null) return;

            await _taskService.DeleteTaskAsync(SelectedTask);
            Tasks.Remove(SelectedTask);
            SelectedTask = null;
        }
    }
}
