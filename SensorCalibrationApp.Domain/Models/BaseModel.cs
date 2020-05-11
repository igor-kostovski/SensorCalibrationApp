using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SensorCalibrationApp.Domain.Models
{
    [Serializable]
    public abstract class BaseModel : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
