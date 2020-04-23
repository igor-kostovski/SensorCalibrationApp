using RimacLINBusInterfacesLib.LinInterfaces.PEAK;
using SensorCalibrationApp.Common;
using SensorCalibrationApp.Common.Enums;
using SensorCalibrationApp.Domain.Enums;
using SensorCalibrationApp.Domain.Interfaces;
using SensorCalibrationApp.Domain.Models;
using SensorCalibrationApp.Domain.Services;
using SensorCalibrationApp.Domain.Services.CommandService;
using SensorCalibrationApp.FileDb;
using SensorCalibrationApp.FileDb.Services;

namespace SensorCalibrationApp.FrameConfiguration
{
    class FrameConfigurationViewModel : ViewModelBase
    {
        private readonly ICommandService _commandService;
        private readonly FileDatabase _db;
        private readonly IFrameService _frameService;
        private readonly ILinProvider _linProvider;

        public FrameConfigurationViewModel()
        {
            var linConfig = new LinConfiguration
            {
                BaudRate = BaudRate.Baud_192,
                HardwareMode = HardwareMode.Master
            };
            _linProvider = new PeakLinInterface(linConfig);
            _commandService = new CommandService(_linProvider);
            _db = new FileDatabase();
            _frameService = new FrameService(_db);
        }

        private DeviceType _frameDeviceType;
        private int _frameDbId;

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

        public void Set(int id, DeviceType device)
        {
            _frameDbId = id;
            _frameDeviceType = device;
        }

        public void LoadFrame()
        {
            _commandService.Load(_frameDeviceType);
            //Frame = _commandService.Get(_frameDbId);
        }
    }
}
