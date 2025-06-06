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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using final.Database;
using final.Services;
using final.ViewModels;

namespace final.Views.Windows
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();

            var db = new AppDbContext();
            var authService = new AuthService(db);
            var viewModel = new RegistrationWindowViewModel(authService);

            DataContext = viewModel;

            if (DataContext is RegistrationWindowViewModel vm)
            {
                vm.OnRegistrationSuccess = AnimateSuccess;
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void TopBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void AuthorizationLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var authorizationWindow = new AuthorizationWindow
            {
                Left = this.Left,
                Top = this.Top
            };
            authorizationWindow.Show();
            this.Close();
        }

        private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegistrationWindowViewModel vm)
                vm.Password = ((PasswordBox)sender).Password;
        }

        private void ConfirmPasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegistrationWindowViewModel vm)
                vm.ConfirmPassword = ((PasswordBox)sender).Password;
        }

        private async void AnimateSuccess()
        {
            await Dispatcher.InvokeAsync(async () =>
            {
                // Анимация скрытия формы
                var fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(300));
                FormPanel.BeginAnimation(OpacityProperty, fadeOut);
                await Task.Delay(300);

                FormPanel.Visibility = Visibility.Collapsed;
                SuccessPanel.Visibility = Visibility.Visible;

                // Анимация появления "успешно"
                var fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(400));
                SuccessPanel.BeginAnimation(OpacityProperty, fadeIn);

                await Task.Delay(2000); // Подождать немного

                // Переход на главное окно
                var mainWindow = new MainWindow
                {
                    Left = this.Left,
                    Top = this.Top
                };

                mainWindow.Show();
                this.Close();
            });
        }
    }
}
