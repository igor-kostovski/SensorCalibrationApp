using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using SensorCalibrationApp.Domain.Enums;
using SensorCalibrationApp.Domain.Models;
using SensorCalibrationApp.Domain.Services.CommandService;
using EventManager = SensorCalibrationApp.Domain.EventManager;

namespace SensorCalibrationApp.FrameConfiguration
{
    class FrameConfigurationViewModel : ViewModelBase
    {
        private readonly ICommandService _commandService;
        private readonly EventManager _eventManager;

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

        private ObservableCollection<SignalValue> _signals;
        public ObservableCollection<SignalValue> Signals
        {
            get { return _signals;}
            set
            {
                _signals = value;
                OnPropertyChanged();
            }
        }

        public FrameConfigurationViewModel(ICommandService commandService, EventManager eventManager)
        {
            _commandService = commandService;
            _eventManager = eventManager;
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

            _eventManager.PushData += OnNewData;
            Signals = new ObservableCollection<SignalValue>();
        }

        public override void Unload()
        {
            RemoveTransmitThread();
            RemoveDevice();

            _eventManager.PushData -= OnNewData;
        }

        private void OnNewData(object sender, object signal)
        {
            if(Signals.Count > 4)
                Application.Current?.Dispatcher.Invoke(() =>
                {
                    Signals.Clear();
                });

            Application.Current?.Dispatcher.Invoke(() =>
            {
                Signals.Add(new SignalValue
                {
                    Data = signal as string,
                    Time = DateTime.Now.ToString("HH:mm:ss")
                });
            });
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

        private void RemoveTransmitThread()
        {
            _shouldTxThreadBeAlive = false;
            _txThread = null;
        }

        private void InjectDevice()
        {
            _commandService.SetDevice(_frameDeviceType);
            _eventManager.SetDevice(_frameDeviceType);
        }

        private void RemoveDevice()
        {
            _commandService.ResetDevice();
            _eventManager.ResetDevice();
        }
    }
}
