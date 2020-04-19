using System;
using System.Collections.Generic;

namespace SensorCalibrationApp.Domain.Models
{
    public class EcuModel : BaseModel
    {
        public List<DeviceModel> Devices { get; set; }
    }
}
