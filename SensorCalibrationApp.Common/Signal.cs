using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SensorCalibrationApp.Common
{
    public class Signal : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private byte _value;
        public byte Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }

        public bool IsEnabled { get; set; }

        public string Description { get; set; }

        public Signal(string description)
        {
            Description = description;
        }
    }
}
