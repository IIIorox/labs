using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using lab8.Model;
using System.Windows.Input;

namespace lab8.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        private Student student;
        private string infoText;

        public Student Student
        {
            get => student;
            set
            {
                student = value;
                OnPropertyChanged();
            }
        }

        public string InfoText
        {
            get => infoText;
            set
            {
                infoText = value;
                OnPropertyChanged();
            }
        }

        public ICommand ResetCommand { get; set; }

        public MainViewModel()
        {
            Student = new Student { Name = "Иван", Age = 18, GPA = 7.5 };
            Student.StudentChanged += (s, e) =>
            {
                InfoText = $"Изменено: {Student.Name}, {Student.Age} лет, GPA: {Student.GPA:F1}";
            };

            ResetCommand = new RelayCommand(Reset);
        }

        private void Reset()
        {
            Student.Name = "Иван";
            Student.Age = 18;
            Student.GPA = 7.5;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
