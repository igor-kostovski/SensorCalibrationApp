using System.Collections.ObjectModel;
using System.Threading.Tasks;
using SensorCalibrationApp.Domain;
using SensorCalibrationApp.Domain.Models;
using SensorCalibrationApp.Domain.Services;
using SensorCalibrationApp.Domain.Services.CommandService;

namespace SensorCalibrationApp.Diagnostics
{
    class DiagnosticsViewModel : ViewModelBase
    {
        private readonly ICommandService _commandService;
        private readonly EventManager _eventManager;
        private readonly IFrameService _frameService;

        public DiagnosticsViewModel(ICommandService commandService, EventManager eventManager, IFrameService frameService)
        {
            _commandService = commandService;
            _eventManager = eventManager;
            _frameService = frameService;
        }

        private FrameModel _frame;
        public FrameModel Frame
        {
            get { return _frame;}
            set
            {
                _frame = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Command> _commands;
        public ObservableCollection<Command> Commands
        {
            get { return _commands; }
            set
            {
                _commands = value;
                OnPropertyChanged();
            }
        }

        private byte _frameId;

        public void Load()
        {
            _eventManager.PushData += OnNewData;
            Commands = CommandsFactory.CreateCommands(ref _frameId);
        }

        public override void Unload()
        {
            _eventManager.PushData -= OnNewData;
        }

        public void Set(FrameModel frame)
        {
            Frame = frame;
            _frameId = Frame.FrameId;
        }

        private void OnNewData(object sender, string e)
        {
            throw new System.NotImplementedException();
        }
    }
}
