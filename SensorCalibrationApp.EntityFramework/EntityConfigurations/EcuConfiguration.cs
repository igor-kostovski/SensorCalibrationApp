using System.Data.Entity.ModelConfiguration;
using SensorCalibrationApp.Domain.Models;

namespace SensorCalibrationApp.EntityFramework.EntityConfigurations
{
    public class EcuConfiguration : EntityTypeConfiguration<EcuModel>
    {
        public EcuConfiguration()
        {
            
        }
    }
}
