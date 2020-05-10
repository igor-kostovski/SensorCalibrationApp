using Autofac;
using SensorCalibrationApp.Domain.Services;
using SensorCalibrationApp.FileDb.Services;

namespace SensorCalibrationApp.FileDb
{
    public class IoCModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FrameService>().As<IFrameService>().SingleInstance();
            builder.RegisterType<EcuService>().As<IEcuService>().SingleInstance();
            builder.RegisterType<DeviceService>().As<IDeviceService>().SingleInstance();
            builder.RegisterType<FileDatabase>().AsSelf().SingleInstance();
            builder.RegisterType<FileDatabase.Seeder>().As<ISeeder>().SingleInstance();

            base.Load(builder);
        }
    }
}
