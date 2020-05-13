using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using RimacLINBusInterfacesLib.Enums;
using SensorCalibrationApp.Domain.Dtos;
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
                Save.RaiseCanExecuteChanged();
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
            InitializeConstants();
        }

        private void InitializeConstants()
        {
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
            Devices = new ObservableCollection<DeviceModel>(await _deviceService.GetAll());

            SelectedFrame = new FrameModel();
            SelectedFrame.RunValidation = Save.RaiseCanExecuteChanged;
        }

        private void OnClear()
        {
            SelectedFrame = null;
            SelectedFrame = new FrameModel();
            SelectedFrame.RunValidation = Save.RaiseCanExecuteChanged;
        }

        private bool CanDelete()
        {
            return SelectedFrame?.Id != 0;
        }

        private async void OnDelete()
        {
            await _frameService.Delete(SelectedFrame.Id);
            Frames.Remove(SelectedFrame);
            OnClear();
        }

        private bool CanSave()
        {
            return SelectedDevice != null && 
                   SelectedFrame.Length <= 8 && 
                   SelectedFrame.Length > 0 &&
                   !string.IsNullOrWhiteSpace(SelectedFrame.Name);
        }

        private async void OnSave()
        {
            AddBytesAndDevice();

            await _deviceService.Update(SelectedDevice);

            if (SelectedFrame.Id != 0)
                await _frameService.Update(SelectedFrame);
            else
            {
                Frames.Add(await _frameService.Create(SelectedFrame));
                OnClear();
            }
        }

        private void AddBytesAndDevice()
        {
            SelectedFrame.Bytes = _frameBytes?.Select(x => x.Value).ToArray();
            SelectedFrame.DeviceId = SelectedDevice.Id;
            SelectedFrame.Device = DeviceDto.MapFrom(SelectedDevice);
        }

        private void SelectDevice()
        {
            SelectedDevice = SelectedFrame != null ? 
                Devices?.FirstOrDefault(x => x.Id == _selectedFrame.DeviceId) : 
                null;
        }
    }
}
