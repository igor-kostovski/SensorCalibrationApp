using System.Data.Entity;
using SensorCalibrationApp.Domain.Models;

namespace SensorCalibrationApp.EntityFramework
{
    public class DataContext : DbContext
    {
        public DataContext() : base()
        { }

        public DbSet<EcuModel> Ecus { get; set; }
        public DbSet<FrameModel> Frames { get; set; }
        public DbSet<DeviceModel> Devices { get; set; }
        public DbSet<SignalModel> Signals { get; set; }
    }
}
