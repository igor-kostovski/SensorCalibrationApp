using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SensorCalibrationApp.Diagnostics
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
        public static ObservableCollection<Command> CreateCommands(byte frameId)
        {
            var assignFrameId = CreateAssignFrameIdCommand(frameId);
            var readById = CreateReadByIdCommand();

            return new ObservableCollection<Command>
            {
                readById,
                assignFrameId
            };
        }

        private static Command CreateReadByIdCommand()
        {
            return new Command
            {
                Name = "Read by identifier",
                Description = "Generic command to check if communication with LIN device is enabled",
                Signals = new List<Signal>
                {
                    new Signal
                    {
                        Value = 0x7F
                    },
                    new Signal
                    {
                        Value = 0x06
                    },
                    new Signal
                    {
                        Value = 0xB2
                    },
                    new Signal
                    {
                        Value = 0x00
                    },
                    new Signal
                    {
                        Value = 0xFF
                    },
                    new Signal
                    {
                        Value = 0x7F
                    },
                    new Signal
                    {
                        Value = 0x7F
                    },
                    new Signal
                    {
                        Value = 0x7F
                    }
                }
            };
        }

        private static Command CreateAssignFrameIdCommand(byte frameId)
        {
            return new Command
            {
                Name = "Assign frame ID",
                Description = "Updates frame ID to avoid possible conflicts due to same IDs on multiple devices",
                Signals = new List<Signal>
                {
                    new Signal
                    {
                        Value = 0x7F
                    },
                    new Signal
                    {
                        Value = 0x06
                    },
                    new Signal
                    {
                        Value = 0xB7
                    },
                    new Signal
                    {
                        Value = 0x02
                    },
                    new Signal
                    {
                        Value = frameId,
                        IsEnabled = true
                    },
                    new Signal
                    {
                        Value = 0xFF
                    },
                    new Signal
                    {
                        Value = 0xFF
                    },
                    new Signal
                    {
                        Value = 0xFF
                    }
                }
            };
        }
    }
}
