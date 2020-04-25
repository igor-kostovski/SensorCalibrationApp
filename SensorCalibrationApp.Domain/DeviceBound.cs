using SensorCalibrationApp.Domain.Enums;
using SensorCalibrationApp.Domain.Factories;
using SensorCalibrationApp.Domain.Interfaces;

namespace SensorCalibrationApp.Domain
{
    public abstract class DeviceBound : IDeviceBound
    {
        protected IDevice _device;

        public void SetDevice(DeviceType device)
        {
            _device = DeviceFactory.CreateDevice(device);
        }

        public void ResetDevice()
        {
            _device = null;
        }
    }
}
