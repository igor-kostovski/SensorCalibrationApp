using System.Data.Entity;
using SensorCalibrationApp.EntityFramework.Data.Entities;

namespace SensorCalibrationApp.EntityFramework
{
    public class DataContext : DbContext
    {
        public DataContext() : base()
        { }

        public DbSet<Ecu> Ecus { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Frame> Frames { get; set; }
        public DbSet<Signal> Signals { get; set; }
    }
}
