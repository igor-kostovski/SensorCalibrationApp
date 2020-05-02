using System.Collections.Generic;

namespace SensorCalibrationApp.EntityFramework.Data.Entities
{
    public class Signal : BaseEntity
    {
        public List<Frame> Frames { get; set; }
    }
}
