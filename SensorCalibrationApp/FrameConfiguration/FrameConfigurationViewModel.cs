using SensorCalibrationApp.Domain;
using SensorCalibrationApp.Domain.Enums;
using SensorCalibrationApp.Domain.Factories;
using SensorCalibrationApp.Domain.Models;
using SensorCalibrationApp.Domain.Services.CommandService;

namespace SensorCalibrationApp.FrameConfiguration
{
    class FrameConfigurationViewModel : ViewModelBase
    {
        private readonly ICommandService _commandService;
        private readonly EventManager _eventManager;

        public FrameConfigurationViewModel(ICommandService commandService, EventManager eventManager)
        {
            _commandService = commandService;
            _eventManager = eventManager;
        }

        private DeviceType _frameDeviceType;

        private FrameModel _frame;
        public FrameModel Frame
        {
            get { return _frame; }
            set
            {
                _frame = value;
                OnPropertyChanged();
            }
        }

        public void Set(FrameModel frame, DeviceType device)
        {
            Frame = frame;
            _frameDeviceType = device;
        }

        public void Load()
        {
            var device = DeviceFactory.CreateDevice(_frameDeviceType);

            _commandService.Load(device);
            _eventManager.Load(device);
        }
    }
}
