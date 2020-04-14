using SensorCalibrationApp.DeviceSelection;
using SensorCalibrationApp.Diagnostics;
using SensorCalibrationApp.FrameConfiguration;

namespace SensorCalibrationApp
{
    class MainWindowViewModel : ViewModelBase
    {
        private DeviceSelectionViewModel _deviceSelectionViewModel = new DeviceSelectionViewModel();
        private FrameConfigurationViewModel _frameConfigurationViewModel = new FrameConfigurationViewModel();
        private DiagnosticsViewModel _diagnosticsViewModel = new DiagnosticsViewModel();

        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand Forward { get; set; }
        public RelayCommand Back { get; set; }

        public MainWindowViewModel()
        {
            Forward = new RelayCommand(OnForward);
            Back = new RelayCommand(OnBack);
            CurrentViewModel = _deviceSelectionViewModel;
        }

        private void OnBack()
        {
            throw new System.NotImplementedException();
        }

        private void OnForward()
        {
            CurrentViewModel = _frameConfigurationViewModel;
        }
    }
}
