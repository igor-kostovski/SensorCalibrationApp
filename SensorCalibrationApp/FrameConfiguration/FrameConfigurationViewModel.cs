using System.Threading;
using SensorCalibrationApp.Domain;
using SensorCalibrationApp.Domain.Enums;
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
        private volatile bool _shouldTxThreadBeAlive;
        private Thread _txThread;

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
            InjectDevice();
            SetupTransmitThread();
        }

        public override void Unload()
        {
            RemoveDevice();
            RemoveTransmitThread();
        }

        private void SetupTransmitThread()
        {
            _shouldTxThreadBeAlive = true;
            _txThread = new Thread(async () =>
            {
                while (_shouldTxThreadBeAlive)
                {
                    await _commandService.SendDeviceSpecificFrame(Frame);
                    Thread.Sleep(1000);
                }
            });

            _txThread.IsBackground = true;
            _txThread.Start();
        }

        private void InjectDevice()
        {
            _commandService.SetDevice(_frameDeviceType);
            _eventManager.SetDevice(_frameDeviceType);
        }

        private void RemoveTransmitThread()
        {
            _shouldTxThreadBeAlive = false;
            _txThread = null;
        }

        private void RemoveDevice()
        {
            _commandService.ResetDevice();
            _eventManager.ResetDevice();
        }
    }
}
