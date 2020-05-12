using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using AutoMapper;
using SensorCalibrationApp.Domain.Services;
using SensorCalibrationApp.EntityFramework.Data;
using SensorCalibrationApp.EntityFramework.Services;
using Module = Autofac.Module;

namespace SensorCalibrationApp.EntityFramework
{
    public class IoCModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EcuService>().As<IEcuService>().SingleInstance();
            builder.RegisterType<FrameService>().As<IFrameService>().SingleInstance();
            builder.RegisterType<DeviceService>().As<IDeviceService>().SingleInstance();
            builder.RegisterType<DataContext>().AsSelf().SingleInstance();
            builder.RegisterType<Seeder>().As<ISeeder>().SingleInstance();

            RegisterAutomapper(builder);

            base.Load(builder);
        }

        private void RegisterAutomapper(ContainerBuilder builder)
        {
            var profiles = GetAutomapperProfiles();
            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            }));

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>().SingleInstance();
        }

        private IEnumerable<Profile> GetAutomapperProfiles()
        {
            var assembly = Assembly.GetExecutingAssembly();

            return assembly
                .GetTypes()
                .Where(x => x.Name.EndsWith("Profile"))
                .Where(x => typeof(Profile).IsAssignableFrom(x) && x.IsPublic && !x.IsAbstract)
                .Select(x => (Profile)Activator.CreateInstance(x));
        }
    }
}
