using System.Collections.Generic;

namespace SensorCalibrationApp.EntityFramework.Data.Entities
{
    public class Ecu : BaseEntity
    {
        public List<Device> Devices { get; set; }
    }
}
