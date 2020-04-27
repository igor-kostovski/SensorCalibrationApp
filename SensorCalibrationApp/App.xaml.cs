﻿using System.Windows;
using Autofac;
using SensorCalibrationApp.Domain.Interfaces;
using SensorCalibrationApp.Domain.Services.CommandService;

namespace SensorCalibrationApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IContainer Container { get; set; }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            Container = AutofacConfig.Initialize();

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
