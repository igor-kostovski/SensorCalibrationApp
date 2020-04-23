using System;
using SensorCalibrationApp.Domain.Devices;
using SensorCalibrationApp.Domain.Enums;
using SensorCalibrationApp.Domain.Interfaces;

namespace SensorCalibrationApp.Domain.Factories
{
    public static class DeviceFactory
    {
        public static IDevice CreateDevice(DeviceType device)
        {
            switch (device)
            {
                case DeviceType.PTSensor:
                    return new PTSensor();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
