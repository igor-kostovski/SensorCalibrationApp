using Autofac;
using RimacLINBusInterfacesLib.LinInterfaces.PEAK;
using SensorCalibrationApp.Common;
using SensorCalibrationApp.Common.Enums;
using SensorCalibrationApp.DeviceSelection;
using SensorCalibrationApp.Diagnostics;
using SensorCalibrationApp.Domain;
using SensorCalibrationApp.Domain.Interfaces;
using SensorCalibrationApp.Domain.Services;
using SensorCalibrationApp.Domain.Services.CommandService;
using SensorCalibrationApp.FileDb.Services;
using SensorCalibrationApp.FrameConfiguration;

namespace SensorCalibrationApp
{
    public class AutofacConfig
    {
        public static IContainer Initialize()
        {
            var builder = new ContainerBuilder();

            var container = builder.Build();

            builder.RegisterType<CommandService>().As<ICommandService>().SingleInstance();
            builder.RegisterType<FrameService>().As<IFrameService>().SingleInstance();
            builder.RegisterType<EcuService>().As<IEcuService>().SingleInstance();
            builder.RegisterType<EventManager>().SingleInstance();

            var linConfig = new LinConfiguration
            {
                BaudRate = BaudRate.Baud_192,
                HardwareMode = HardwareMode.Master
            };
            builder.Register(b => new PeakLinInterface(linConfig)).As<ILinProvider>().SingleInstance();

            builder.RegisterType<DeviceSelectionViewModel>().SingleInstance();
            builder.RegisterType<FrameConfigurationViewModel>().SingleInstance();
            builder.RegisterType<DiagnosticsViewModel>().SingleInstance();

            return container;
        }
    }
}
