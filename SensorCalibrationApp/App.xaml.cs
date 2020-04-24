using System.Windows;
using Autofac;

namespace SensorCalibrationApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var container = AutofacConfig.Initialize();

            Window main = container.Resolve<MainWindow>();

            main.Show();
        }
    }
}
