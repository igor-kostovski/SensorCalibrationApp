using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SensorCalibrationApp.Common.Enums;

namespace SensorCalibrationApp.Common
{
    public class Command : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public List<Signal> Signals { get; set; }
        public CommandType Type { get; set; }
        public int Version { get; set; }
    }
}
