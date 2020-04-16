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
            }
        }

        public async void LoadECUs()
        {
            EcuModels = new ObservableCollection<EcuModel>(await _ecuService.GetAll());
        }
    }
}
