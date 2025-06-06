using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using final.Database;
using final.Services;
using final.ViewModels;

namespace final.Views.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();

            var db = new AppDbContext();
            var authService = new AuthService(db);
            var viewModel = new AuthorizationWindowViewModel(authService);

            DataContext = viewModel;

            if (DataContext is AuthorizationWindowViewModel vm)
            {
                vm.OnAuthorizationSuccess = AnimateSuccess;
            }
        }
        private void Close_Click(object sender, RoutedEventArgs e) => Close();

        private void Minimize_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        private void TopBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void RegisterLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var registrationWindow = new RegistrationWindow
            {
                Left = this.Left,
                Top = this.Top
            };
            registrationWindow.Show();
            Close();
        }

        private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is AuthorizationWindowViewModel vm)
                vm.Password = ((PasswordBox)sender).Password;
        }

        private async void AnimateSuccess()
        {
            await Dispatcher.InvokeAsync(async () =>
            {
                var fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(300));
                FormPanel.BeginAnimation(OpacityProperty, fadeOut);
                await Task.Delay(300);

                FormPanel.Visibility = Visibility.Collapsed;
                SuccessPanel.Visibility = Visibility.Visible;

                var fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(400));
                SuccessPanel.BeginAnimation(OpacityProperty, fadeIn);

                await Task.Delay(2000);

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