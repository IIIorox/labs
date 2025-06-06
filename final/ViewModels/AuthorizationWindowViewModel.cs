using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using final.Commands;
using final.Services;
using final.ViewModels.Base;

namespace final.ViewModels
{
    internal class AuthorizationWindowViewModel : ViewModel
    {
        private readonly AuthService _authService;

        private string _login;
        private string _password;

        private string _error;

        public string Login
        {
            get => _login;
            set => Set(ref _login, value);
        }

        public string Password
        {
            get => _password;
            set => Set(ref _password, value);
        }

        public string Error
        {
            get => _error;
            set => Set(ref _error, value);
        }

        public ICommand LoginCommand { get; }

        public Action? OnAuthorizationSuccess { get; set; }

        public AuthorizationWindowViewModel(AuthService authService)
        {
            _authService = authService;
            LoginCommand = new RelayCommand(async () => await AuthorizeAsync());
        }

        private async Task AuthorizeAsync()
        {
            if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password))
            {
                Error = "Все поля должны быть заполнены";
                return;
            }

            var (isSuccess, message, user) = await _authService.LoginAsync(Login, Password);
            Error = message;

            if (isSuccess)
            {
                UserSession.SetUser(user);
                OnAuthorizationSuccess?.Invoke();
            }
        }
    }
}
