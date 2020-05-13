using System.Windows;
using Autofac;
using RimacLINBusInterfacesLib.Interfaces;
using SensorCalibrationApp.Domain.Services;
using SensorCalibrationApp.Domain.Services.CommandService;
using SensorCalibrationApp.Screens.Main;

namespace SensorCalibrationApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IContainer Container { get; set; }

        private async void App_OnStartup(object sender, StartupEventArgs e)
        {
            Container = AutofacConfig.Initialize();

            var seeder = Container.Resolve<ISeeder>();
            await seeder.Seed();

            Window main = Container.Resolve<MainWindow>();
            main.Show();
        }

        private void App_OnExit(object sender, ExitEventArgs e)
        {
            var linProvider = Container.Resolve<ILinProvider>();
            linProvider.CloseConnection();

            var mainWindowViewModel = Container.Resolve<MainWindowViewModel>();
            mainWindowViewModel.Unload();

            var commandService = Container.Resolve<ICommandService>();
            commandService.Dispose();
        }
    }
}
