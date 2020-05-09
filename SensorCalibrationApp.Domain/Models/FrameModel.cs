using System.Collections.Generic;
using SensorCalibrationApp.Common.Enums;

namespace SensorCalibrationApp.Domain.Models
{
    public class FrameModel : BaseModel
    {
        public byte FrameId { get; set; }
        public Direction Direction { get; set; }

        public List<SignalModel> Signals { get; set; }

        public int DeviceId { get; set; }
        public DeviceModel Device { get; set; }
    }
}
