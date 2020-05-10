using System.Collections.Generic;
using SensorCalibrationApp.Common.Enums;
using SensorCalibrationApp.Domain.Dtos;

namespace SensorCalibrationApp.Domain.Models
{
    public class FrameModel : BaseModel
    {
        public byte FrameId { get; set; }
        public Direction Direction { get; set; }
        public byte[] Bytes { get; set; }

        public List<SignalModel> Signals { get; set; }

        public int DeviceId { get; set; }
        public DeviceDto Device { get; set; }
    }
}
