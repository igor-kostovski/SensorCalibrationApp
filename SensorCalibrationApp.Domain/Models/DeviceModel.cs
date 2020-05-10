using System.Collections.Generic;
using SensorCalibrationApp.Domain.Enums;

namespace SensorCalibrationApp.Domain.Models
{
    public class DeviceModel : BaseModel
    {
        public bool IncludeSaveConfig { get; set; }
        public List<FrameModel> Frames { get; set; }
        public DeviceType Type { get; set; }
    }
}
