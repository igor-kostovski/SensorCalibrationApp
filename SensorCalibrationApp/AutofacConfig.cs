using Autofac;
using RimacLINBusInterfacesLib.LinInterfaces.PEAK;
using SensorCalibrationApp.Common;
using SensorCalibrationApp.Common.Enums;
using SensorCalibrationApp.Domain;
using SensorCalibrationApp.Domain.Interfaces;
using SensorCalibrationApp.Domain.Services;
using SensorCalibrationApp.Domain.Services.CommandService;
using SensorCalibrationApp.FileDb;
using SensorCalibrationApp.FileDb.Services;
using SensorCalibrationApp.Screens.DeviceSelection;
using SensorCalibrationApp.Screens.Diagnostics;
using SensorCalibrationApp.Screens.FrameConfiguration;
using SensorCalibrationApp.Screens.Main;

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

            //set in config file whether you want FileDb or EF and depending on that variable register types
            //if(DbType == FileDb)
            //    //register FileDb
            //else
            //    //register EF 

            //registering FileDb types
            builder.RegisterType<FrameService>().As<IFrameService>().SingleInstance();
            builder.RegisterType<EcuService>().As<IEcuService>().SingleInstance();
            builder.RegisterType<FileDatabase>().AsSelf().SingleInstance();
            builder.RegisterType<FileDatabase.Seeder>().As<ISeeder>().SingleInstance();

            //registering EF types -> to be implemented

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
            builder.RegisterType<MainWindowViewModel>().AsSelf().SingleInstance();

            //registering main view
            builder.Register(b => new MainWindow(b.Resolve<MainWindowViewModel>())).AsSelf().SingleInstance();

            return builder.Build();
        }
    }
}
