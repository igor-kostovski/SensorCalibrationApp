using System;
using System.Collections.ObjectModel;
using SensorCalibrationApp.Domain.Enums;
using SensorCalibrationApp.Domain.Models;
using SensorCalibrationApp.Domain.Services;

namespace SensorCalibrationApp.Screens.DeviceSelection
{
    class DeviceSelectionViewModel : ViewModelBase
    {
        private readonly IEcuService _ecuService;

        public event EventHandler<bool> SelectionChanged;

        private ObservableCollection<EcuModel> _ecuModels;
        public ObservableCollection<EcuModel> EcuModels
        {
            get { return _ecuModels; }
            set
            {
                _ecuModels = value;
                OnPropertyChanged();
            }
        }

        private EcuModel _selectedEcu;
        public EcuModel SelectedEcu
        {
            get { return _selectedEcu; }
            set
            {
                _selectedEcu = value;
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

                CheckIfSupported();

                OnPropertyChanged();
            }
        }

        private FrameModel _selectedFrame;
        public FrameModel SelectedFrame
        {
            get { return _selectedFrame; }
            set
            {
                _selectedFrame = value;
                OnPropertyChanged();

                SelectionChanged?.Invoke(this, _selectedFrame != null);
            }
        }

        private bool _isDeviceSupported;
        public bool IsDeviceSupported
        {
            get { return _isDeviceSupported; }
            set
            {
                _isDeviceSupported = value;
                OnPropertyChanged();
            }
        }

        public DeviceSelectionViewModel(IEcuService ecuService)
        {
            _ecuService = ecuService;
            IsDeviceSupported = true;
        }

        public async void LoadECUs()
        {
            EcuModels = new ObservableCollection<EcuModel>(await _ecuService.GetAll());
        }

        private void CheckIfSupported()
        {
            IsDeviceSupported = _selectedDevice == null ||
                                (_selectedDevice?.Frames.Count != 0 && _selectedDevice?.Type == DeviceType.PTSensor);
        }
    }
}
