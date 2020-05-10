using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SensorCalibrationApp.Screens.FrameManagement
{
    public class ByteValue : INotifyPropertyChanged
    {
        private byte _value;
        public byte Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }

        public ByteValue(byte value)
        {
            Value = value;
        }

        public static ObservableCollection<ByteValue> GetFrameBytes(byte[] byteArr)
        {
            var frameBytes = new ObservableCollection<ByteValue>();
            foreach (var b in byteArr)
            {
                frameBytes.Add(new ByteValue(b));
            }

            return frameBytes;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
