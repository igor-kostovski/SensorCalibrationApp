using SensorCalibrationApp.Domain.Enums;

namespace SensorCalibrationApp.Domain.Interfaces
{
    public interface IDeviceBound
    {
        void SetDevice(DeviceType device);
        void ResetDevice();
    }
}
