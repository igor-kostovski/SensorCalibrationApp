using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using SensorCalibrationApp.Common.Enums;
using SensorCalibrationApp.Domain.Models;
using SensorCalibrationApp.Domain.Services;

namespace SensorCalibrationApp.Screens.FrameManagement
{
    class FrameManagementViewModel : ViewModelBase
    {
        private readonly IFrameService _frameService;
        private readonly IDeviceService _deviceService;

        private ObservableCollection<FrameModel> _frames;
        public ObservableCollection<FrameModel> Frames
        {
            get
            {
                return _frames;
            }
            set
            {
                _frames = value;
                OnPropertyChanged();
            }
        }

        private FrameModel _selectedFrame;
        public FrameModel SelectedFrame
        {
            get
            {
                return _selectedFrame;
            }
            set
            {
                _selectedFrame = value;

                FrameBytes = ByteValue.GetFrameBytes(SelectedFrame?.Bytes ?? new byte[8]);

                OnPropertyChanged();

                Delete.RaiseCanExecuteChanged();
                Clear.RaiseCanExecuteChanged();
                SelectDevice();
            }
        }

        private ObservableCollection<ByteValue> _frameBytes;
        public ObservableCollection<ByteValue> FrameBytes
        {
            get
            {
                return _frameBytes;
            }
            set
            {
                _frameBytes = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<DeviceModel> _devices;
        public ObservableCollection<DeviceModel> Devices
        {
            get
            {
                return _devices;
            }
            set
            {
                _devices = value;
                OnPropertyChanged();
            }
        }

        private DeviceModel _selectedDevice;
        public DeviceModel SelectedDevice
        {
            get { return _selectedDevice; }
            set
            {
                _selectedDevice = value;
                OnPropertyChanged();
            }
        }


        public List<Direction> Directions { get; set; }
        public List<ChecksumType> Checksums { get; set; }

        public RelayCommand Save { get; set; }
        public RelayCommand Delete { get; set; }
        public RelayCommand Clear { get; set; }

        public FrameManagementViewModel(IFrameService frameService, IDeviceService deviceService)
        {
            _frameService = frameService;
            _deviceService = deviceService;
            InitializeCommands();

            Directions = Enum.GetValues(typeof(Direction)).Cast<Direction>().ToList();
            Checksums = Enum.GetValues(typeof(ChecksumType)).Cast<ChecksumType>().ToList();
        }

        private void InitializeCommands()
        {
            Save = new RelayCommand(OnSave, CanSave);
            Delete = new RelayCommand(OnDelete, CanDelete);
            Clear = new RelayCommand(OnClear, CanDelete);
        }

        public async void Load()
        {
            Frames = new ObservableCollection<FrameModel>(await _frameService.GetAll());
            SelectedFrame = new FrameModel();
            Devices = new ObservableCollection<DeviceModel>(await _deviceService.GetAll());
        }

        private bool CanDelete()
        {
            return SelectedFrame?.Id != 0;
        }

        private void OnDelete()
        {
            Frames.Remove(SelectedFrame);
            //_frameService.Delete(SelectedFrame.Id);
            OnClear();
        }

        private bool CanSave()
        {
            return true;
        }

        private void OnSave()
        {
            SelectedFrame.Bytes = _frameBytes?.Select(x => x.Value).ToArray();
            if (SelectedFrame.Id != 0)
            {
                //_frameService.Update(SelectedFrame);
                //var toUpdate = Frames.SingleOrDefault(x => x.Id == SelectedFrame.Id);
                //Update(toUpdate, SelectedFrame);
                Debug.WriteLine("Updated");
            }
            else
            {
                //Frames.Add(await _frameService.Add(SelectedFrame));
                Debug.WriteLine("Added");
            }
            OnClear();
        }

        private void OnClear()
        {
            SelectedFrame = null;
            SelectedFrame = new FrameModel();
        }

        private void SelectDevice()
        {
            SelectedDevice = SelectedFrame != null ? 
                Devices?.FirstOrDefault(x => x.Id == _selectedFrame.DeviceId) : 
                null;
        }
    }
}
