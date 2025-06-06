using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace lab8.Model
{
    class Student : INotifyPropertyChanged
    {
        private string name;
        private int age;
        private double gpa;

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler StudentChanged;

        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged();
                    OnStudentChanged();
                }
            }
        }

        public int Age
        {
            get => age;
            set
            {
                if (age != value)
                {
                    age = value;
                    OnPropertyChanged();
                    OnStudentChanged();
                }
            }
        }

        public double GPA
        {
            get => gpa;
            set
            {
                if (gpa != value)
                {
                    gpa = value;
                    OnPropertyChanged();
                    OnStudentChanged();
                }
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected virtual void OnStudentChanged() =>
            StudentChanged?.Invoke(this, EventArgs.Empty);
    }
}
