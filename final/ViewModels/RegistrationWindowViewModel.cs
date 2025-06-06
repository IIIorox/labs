using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using final.Commands;
using final.Services;
using final.ViewModels.Base;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;

namespace final.ViewModels
{
    class RegistrationWindowViewModel : ViewModel
    {
        private readonly AuthService _authService;

        private string _login;
        private string _password;
        private string _confirmPassword;
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

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => Set(ref _confirmPassword, value);
        }

        public string Error
        {
            get => _error;
            set => Set(ref _error, value);
        }

        public ICommand RegisterCommand { get; }

        public Action? OnRegistrationSuccess { get; set; }

        public RegistrationWindowViewModel(AuthService authService)
        {
            _authService = authService;
            RegisterCommand = new RelayCommand(async () => await RegisterAsync());
        }

        private async Task RegisterAsync()
        {
            if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                Error = "Все поля должны быть заполнены";
                return;
            }

            if (Password != ConfirmPassword)
            {
                Error = "Пароли не совпадают";
                return;
            }

            var (isSuccess, message, user) = await _authService.RegisterAsync(Login, Password);
            Error = message;

            if (isSuccess)
            {
                UserSession.SetUser(user);
                OnRegistrationSuccess?.Invoke();
            }
        }
    }
}
