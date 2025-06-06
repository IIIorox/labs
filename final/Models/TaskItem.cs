using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using final.ViewModels.Base;

namespace final.Models
{
    class TaskItem : ViewModel
    {
        private string _title;
        private string _description;
        private bool _isCompleted;
        private DateTime _createdAt;

        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        public string Description
        {
            get => _description;
            set => Set(ref _description, value);
        }

        public bool IsCompleted
        {
            get => _isCompleted;
            set => Set(ref _isCompleted, value);
        }

        public DateTime CreatedAt
        {
            get => _createdAt;
            set => Set(ref _createdAt, value);
        }
    }
}
