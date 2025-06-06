using System.Configuration;
using System.Data;
using System.Windows;
using final.Database;
using final.Views.Windows;

namespace final
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            AppDbContext.EnsureDatabase();
        }
    }
}
