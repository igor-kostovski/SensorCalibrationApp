using System;
using System.Collections.Generic;
using SensorCalibrationApp.Common.Enums;

namespace SensorCalibrationApp.EntityFramework.Data.Entities
{
    public class Frame : BaseEntity
    {
        public byte FrameId { get; set; }
        public Direction Direction { get; set; }

        public List<Signal> Signals { get; set; }
        public List<Device> Devices { get; set; }
    }
}
