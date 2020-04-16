using System.Collections.ObjectModel;
using SensorCalibrationApp.Domain.Models;

namespace SensorCalibrationApp.DeviceSelection
{
    class DeviceSelectionViewModel : ViewModelBase
    {
        private ObservableCollection<EcuModel> _ecuModels;

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


    }
}
