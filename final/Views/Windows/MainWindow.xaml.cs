using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using final.Database;
using final.Models;
using final.Services;
using final.ViewModels;

namespace final.Views.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var db = new AppDbContext();
            var viewModel = new MainWindowViewModel();

            DataContext = viewModel;
        }

        private void Close_Click(object sender, RoutedEventArgs e) => Close();

        private void Minimize_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        private void TopBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void TaskItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is UserTask task)
            {
                var vm = DataContext as MainWindowViewModel;
                if (vm != null)
                    vm.SelectedTask = task;
            }
        }
    }
}
