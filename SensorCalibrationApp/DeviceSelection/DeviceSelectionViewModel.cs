using System;
using System.Collections.ObjectModel;
using SensorCalibrationApp.Domain.Models;
using SensorCalibrationApp.Domain.Services;
using SensorCalibrationApp.FileDb;
using SensorCalibrationApp.FileDb.Services;

namespace SensorCalibrationApp.DeviceSelection
{
    class DeviceSelectionViewModel : ViewModelBase
    {
        private FileDatabase _db;
        private IEcuService _ecuService;

        public DeviceSelectionViewModel()
        {
            _db = new FileDatabase();
            _ecuService = new EcuService(_db);
            IsDeviceSupported = true;
        }

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

                SelectionDone?.Invoke(this, EventArgs.Empty);
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

        public async void LoadECUs()
        {
            if(EcuModels == null)
                EcuModels = new ObservableCollection<EcuModel>(await _ecuService.GetAll());
        }

        private void CheckIfSupported()
        {
            IsDeviceSupported = _selectedDevice?.Frames.Count != 0;
        }

        public event EventHandler SelectionDone;
    }
}
