using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using SensorCalibrationApp.Common.Extensions;

namespace SensorCalibrationApp.Common
{
    public delegate Task CommandWithParams<T>(T param);
    public delegate Task CommandWithoutParams();

    public class Command : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly CommandWithParams<byte> _withByteParam;
        private readonly CommandWithoutParams _withoutParams;

        public Command(CommandWithoutParams command)
        {
            _withoutParams = command;
        }

        public Command(CommandWithParams<byte> command)
        {
            _withByteParam = command;
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

        public async Task Run()
        {
            if (_withByteParam != null)
            {
                var param = this.GetFrameId();
                await _withByteParam.Invoke(param);
            }

            if (_withoutParams != null)
                await _withoutParams.Invoke();
        }
    }
}
