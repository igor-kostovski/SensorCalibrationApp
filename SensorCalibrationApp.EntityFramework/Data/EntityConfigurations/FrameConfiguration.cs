using System.Data.Entity.ModelConfiguration;
using SensorCalibrationApp.EntityFramework.Data.Entities;

namespace SensorCalibrationApp.EntityFramework.Data.EntityConfigurations
{
    public class FrameConfiguration : EntityTypeConfiguration<Frame>
    {
        public FrameConfiguration()
        {
            HasMany(x => x.Signals)
                .WithMany(x => x.Frames)
                .Map(x =>
                {
                    x.MapLeftKey("Frame_Id");
                    x.MapRightKey("SignalId");
                    x.ToTable("FrameSignals");
                });
        }
    }
}
