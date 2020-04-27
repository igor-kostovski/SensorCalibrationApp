using System;
using SensorCalibrationApp.Domain.Devices;

namespace SensorCalibrationApp.Domain
{
    public class EventManager : DeviceBound
    {
        public event EventHandler<string> PushData;
        public event EventHandler<string> PushError;
        
        public void HandleError(object sender, string message)
        {
            PushError?.Invoke(this, message);
        }

        public void HandleRead(object sender, byte[] data)
        {
            var message = "";

            if (_device != null)
                ReadWithDevice(data, ref message);
            else
                message = BitConverter.ToString(data).Replace('-', '|');

            PushData?.Invoke(this, message);
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
