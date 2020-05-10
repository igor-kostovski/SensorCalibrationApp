using SensorCalibrationApp.Domain.Enums;
using SensorCalibrationApp.Domain.Models;

namespace SensorCalibrationApp.Domain.Dtos
{
    public class DeviceDto : BaseModel
    {
        public bool IncludeSaveConfig { get; set; }
        public DeviceType Type { get; set; }
    }
}
