using RimacLINBusInterfacesLib.LinInterfaces.PEAK;
using SensorCalibrationApp.Common;
using SensorCalibrationApp.Common.Enums;
using SensorCalibrationApp.Domain;
using SensorCalibrationApp.Domain.Enums;
using SensorCalibrationApp.Domain.Factories;
using SensorCalibrationApp.Domain.Interfaces;
using SensorCalibrationApp.Domain.Models;
using SensorCalibrationApp.Domain.Services.CommandService;

namespace SensorCalibrationApp.FrameConfiguration
{
    class FrameConfigurationViewModel : ViewModelBase
    {
        private readonly ICommandService _commandService;
        private readonly ILinProvider _linProvider;
        private readonly EventManager _eventManager;

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

        public FrameConfigurationViewModel()
        {
            var linConfig = new LinConfiguration
            {
                BaudRate = BaudRate.Baud_192,
                HardwareMode = HardwareMode.Master
            };
            _linProvider = new PeakLinInterface(linConfig);
            _commandService = new CommandService(_linProvider);
            _eventManager = new EventManager();
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
