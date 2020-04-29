using System;

namespace SensorCalibrationApp.Domain
{
    public class EventManager : DeviceBound
    {
        public event EventHandler<object> PushData;
        public event EventHandler<string> PushError;
        
        public void HandleError(object sender, string message)
        {
            PushError?.Invoke(this, message);
        }

        public void HandleRead(object sender, byte[] data)
        {
            if (_device != null)
            {
                var message = "";
                ReadWithDevice(data, ref message);
                PushData?.Invoke(this, message);
            }
            else
            {
                PushData?.Invoke(this, data);
            }

            
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
