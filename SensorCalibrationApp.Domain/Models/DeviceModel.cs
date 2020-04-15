using System;
using System.Collections.Generic;
using System.Text;

namespace SensorCalibrationApp.Domain.Models
{
    public class DeviceModel : BaseModel
    {
        public List<FrameModel> Frames { get; set; }
    }
}
