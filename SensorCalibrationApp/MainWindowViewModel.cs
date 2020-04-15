using System.Collections.Generic;
using System.Linq;
using SensorCalibrationApp.DeviceSelection;
using SensorCalibrationApp.Diagnostics;
using SensorCalibrationApp.FrameConfiguration;

namespace SensorCalibrationApp
{
    class MainWindowViewModel : ViewModelBase
    {
        private readonly DeviceSelectionViewModel _deviceSelectionViewModel = new DeviceSelectionViewModel();
        private readonly FrameConfigurationViewModel _frameConfigurationViewModel = new FrameConfigurationViewModel();
        private readonly DiagnosticsViewModel _diagnosticsViewModel = new DiagnosticsViewModel();
        private readonly List<ViewModelBase> _navigationStack;

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

            _navigationStack = new List<ViewModelBase>
            {
                _deviceSelectionViewModel,
                _frameConfigurationViewModel,
                _diagnosticsViewModel
            };
        }

        private void OnBack()
        {
            var currentIndex = _navigationStack.IndexOf(CurrentViewModel);
            var backIndex = --currentIndex;

            if(backIndex > -1)
                CurrentViewModel = _navigationStack.ElementAt(backIndex);
        }

        private void OnForward()
        {
            var currentIndex = _navigationStack.IndexOf(CurrentViewModel);
            var forwardIndex = ++currentIndex;

            if(forwardIndex < _navigationStack.Count)
                CurrentViewModel = _navigationStack.ElementAt(forwardIndex);
        }
    }
}
