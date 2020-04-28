using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using SensorCalibrationApp.Domain.Services.CommandService;

namespace SensorCalibrationApp.Diagnostics
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
            if(_withByteParam != null)
            {
                var param = Signals.Single(x => x.IsEnabled).Value;
                await _withByteParam.Invoke(param);
            }

            if (_withoutParams != null)
                await _withoutParams.Invoke();
        }
    }

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
    }

    public static class CommandsFactory
    {
        public static ObservableCollection<Command> CreateCommands(byte frameId, ICommandService service)
        {
            var assignFrameId = CreateAssignFrameIdCommand(frameId, service.UpdateFrameId);
            var readById = CreateReadByIdCommand(service.ReadById);

            return new ObservableCollection<Command>
            {
                readById,
                assignFrameId
            };
        }

        private static Command CreateReadByIdCommand(CommandWithoutParams command)
        {
            return new Command(command)
            {
                Name = "Read by identifier",
                Description = "Generic command to check if communication with LIN device is enabled",
                Signals = new List<Signal>
                {
                    new Signal {Value = 0x7F},
                    new Signal {Value = 0x06},
                    new Signal {Value = 0xB2},
                    new Signal {Value = 0x00},
                    new Signal {Value = 0xFF},
                    new Signal {Value = 0x7F},
                    new Signal {Value = 0x7F},
                    new Signal {Value = 0x7F}
                }
            };
        }

        private static Command CreateAssignFrameIdCommand(byte frameId, CommandWithParams<byte> command)
        {
            return new Command(command)
            {
                Name = "Assign frame ID",
                Description = "Updates frame ID to avoid possible conflicts due to same IDs on multiple devices",
                Signals = new List<Signal>
                {
                    new Signal {Value = 0x7F},
                    new Signal {Value = 0x06},
                    new Signal {Value = 0xB7},
                    new Signal {Value = 0x02},
                    new Signal {Value = frameId, IsEnabled = true},
                    new Signal {Value = 0xFF},
                    new Signal {Value = 0xFF},
                    new Signal {Value = 0xFF}
                }
            };
        }
    }
}
