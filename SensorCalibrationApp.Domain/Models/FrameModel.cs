using System;
using System.Collections.Generic;
using System.Text;

namespace SensorCalibrationApp.Domain.Models
{
    public class FrameModel : BaseModel
    {
        public byte FrameId { get; set; }
        public List<SignalModel> Signals { get; set; }
    }
}
