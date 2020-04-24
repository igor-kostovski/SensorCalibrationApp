using System;
using SensorCalibrationApp.Domain.Devices;

namespace SensorCalibrationApp.Domain
{
    public class EventManager : DeviceBound
    {
        public void HandleError(object sender, string message)
        {
            Console.WriteLine($"Error: {message}");
        }

        public void HandleRead(object sender, byte[] data)
        {
            var message = "";

            if (_device != null)
                ReadWithDevice(data, ref message);
            else
                message = BitConverter.ToString(data).Replace('-', '|');

            Console.WriteLine($"Received: {message}");
        }

        private void ReadWithDevice(byte[] data, ref string message)
        {
            _device.ProcessData(data);

            if (_device.HasError)
                HandleError(this, _device.Message);
            else
                message = _device.Message;

            _device.ClearData();
        }
    }
}
