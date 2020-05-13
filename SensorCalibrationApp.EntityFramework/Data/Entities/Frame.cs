using System.Collections.Generic;
using RimacLINBusInterfacesLib.Enums;

namespace SensorCalibrationApp.EntityFramework.Data.Entities
{
    public class Frame : BaseEntity
    {
        public byte FrameId { get; set; }
        public Direction Direction { get; set; }
        public byte[] Bytes { get; set; }
        public byte Length { get; set; }
        public ChecksumType Checksum { get; set; }

        public int DeviceId { get; set; }
        public Device Device { get; set; }

        public List<Signal> Signals { get; set; }
    }
}
