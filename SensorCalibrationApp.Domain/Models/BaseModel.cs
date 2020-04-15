using System;
using System.Collections.Generic;
using System.Text;

namespace SensorCalibrationApp.Domain.Models
{
    public abstract class BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
