using System.Collections.Generic;
using SensorCalibrationApp.Domain.Enums;

namespace SensorCalibrationApp.EntityFramework.Data.Entities
{
    public class Device : BaseEntity
    {
        public DeviceType Type { get; set; }
        public bool IncludeSaveConfig { get; set; }

        public List<Frame> Frames { get; set; }
        public List<Ecu> Ecus { get; set; }
    }
}
