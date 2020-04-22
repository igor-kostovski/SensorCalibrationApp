using System;
using SensorCalibrationApp.Domain.Interfaces;

namespace SensorCalibrationApp.Domain
{
    public class EventManager
    {
        private readonly IDevice _device;

        public bool UseDeviceParser { get; set; }

        public EventManager(IDevice device)
        {
            _device = device;
        }

        public void HandleError(object sender, string message)
        {
            Console.WriteLine($"Error: {message}");
        }

        public void HandleRead(object sender, byte[] data)
        {
            var message = "";

            if (UseDeviceParser)
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
