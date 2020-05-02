using System.Data.Entity.ModelConfiguration;
using SensorCalibrationApp.EntityFramework.Data.Entities;

namespace SensorCalibrationApp.EntityFramework.Data.EntityConfigurations
{
    public class EcuConfiguration : EntityTypeConfiguration<Ecu>
    {
        public EcuConfiguration()
        {
            HasMany(x => x.Devices)
                .WithMany(x => x.Ecus)
                .Map(x =>
                {
                    x.MapLeftKey("EcuId");
                    x.MapRightKey("DeviceId");
                    x.ToTable("EcuDevices");
                });
        }
    }
}
