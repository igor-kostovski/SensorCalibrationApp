using System.Data.Entity;
using SensorCalibrationApp.EntityFramework.Data.Entities;
using SensorCalibrationApp.EntityFramework.Data.EntityConfigurations;

namespace SensorCalibrationApp.EntityFramework.Data
{
    public class DataContext : DbContext
    {
        public DataContext() : base()
        { }

        public DbSet<Ecu> Ecus { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Frame> Frames { get; set; }
        public DbSet<Signal> Signals { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EcuConfiguration());
            modelBuilder.Configurations.Add(new DeviceConfiguration());
            modelBuilder.Configurations.Add(new FrameConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
