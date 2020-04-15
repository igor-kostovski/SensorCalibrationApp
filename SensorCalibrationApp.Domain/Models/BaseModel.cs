using System;

namespace SensorCalibrationApp.Domain.Models
{
    [Serializable]
    public abstract class BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
