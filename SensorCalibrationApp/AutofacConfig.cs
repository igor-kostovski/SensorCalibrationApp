using Autofac;
using RimacLINBusInterfacesLib.LinInterfaces.PEAK;
using SensorCalibrationApp.Common;
using SensorCalibrationApp.Common.Enums;
using SensorCalibrationApp.Domain;
using SensorCalibrationApp.Domain.Interfaces;
using SensorCalibrationApp.Domain.Services.CommandService;
using SensorCalibrationApp.Screens.DeviceSelection;
using SensorCalibrationApp.Screens.Diagnostics;
using SensorCalibrationApp.Screens.FrameConfiguration;
using SensorCalibrationApp.Screens.Main;
using System.Configuration;
using SensorCalibrationApp.Screens.FrameManagement;

namespace SensorCalibrationApp
{
    public class AutofacConfig
    {
        public static IContainer Initialize()
        {
            var builder = new ContainerBuilder();

            //registering domain types
            builder.RegisterType<CommandService>().As<ICommandService>().SingleInstance();
            builder.RegisterType<EventManager>().AsSelf().SingleInstance();

            var dbType = ConfigurationManager.AppSettings["DbType"];
            if (dbType == "File")
            {
                //registering FileDb types
                builder.RegisterModule(new FileDb.IoCModule());
            }
            else
            {
                //registering EF types
                builder.RegisterModule(new EntityFramework.IoCModule());
            }

            //registering RimacLinBusInterfaces types
            var linConfig = new LinConfiguration
            {
                BaudRate = BaudRate.Baud_192,
                HardwareMode = HardwareMode.Master
            };
            builder.Register(b => new PeakLinInterface(linConfig)).As<ILinProvider>().SingleInstance();

            //registering view model types
            builder.RegisterType<DeviceSelectionViewModel>().AsSelf().SingleInstance();
            builder.RegisterType<FrameConfigurationViewModel>().AsSelf().SingleInstance();
            builder.RegisterType<DiagnosticsViewModel>().AsSelf().SingleInstance();
            builder.RegisterType<FrameManagementViewModel>().AsSelf().SingleInstance();
            builder.RegisterType<MainWindowViewModel>().AsSelf().SingleInstance();

            //registering main view
            builder.Register(b => new MainWindow(b.Resolve<MainWindowViewModel>())).AsSelf().SingleInstance();

            return builder.Build();
        }
    }
}
