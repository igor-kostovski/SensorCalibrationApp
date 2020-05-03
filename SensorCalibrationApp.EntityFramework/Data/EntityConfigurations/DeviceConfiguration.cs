using System.Data.Entity.ModelConfiguration;
using SensorCalibrationApp.EntityFramework.Data.Entities;

namespace SensorCalibrationApp.EntityFramework.Data.EntityConfigurations
{
    public class DeviceConfiguration : EntityTypeConfiguration<Device>
    {
        public DeviceConfiguration()
        {
            HasMany(x => x.Frames)
                .WithRequired(x => x.Device)
                .HasForeignKey(x => x.DeviceId);
        }
    }
}
