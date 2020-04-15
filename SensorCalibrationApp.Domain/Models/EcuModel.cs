using System;
using System.Collections.Generic;
using System.Text;

namespace SensorCalibrationApp.Domain.Models
{
    public class EcuModel : BaseModel
    {
        public List<DeviceModel> Devices { get; set; }
    }
}
