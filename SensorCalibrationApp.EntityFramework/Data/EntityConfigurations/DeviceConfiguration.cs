using System.Data.Entity.ModelConfiguration;
using SensorCalibrationApp.EntityFramework.Data.Entities;

namespace SensorCalibrationApp.EntityFramework.Data.EntityConfigurations
{
    public class DeviceConfiguration : EntityTypeConfiguration<Device>
    {
        public DeviceConfiguration()
        {
            HasMany(x => x.Frames)
                .WithMany(x => x.Devices)
                .Map(x =>
                {
                    x.MapLeftKey("DeviceId");
                    x.MapRightKey("Frame_Id");
                    x.ToTable("DeviceFrames");
                });
        }
    }
}
