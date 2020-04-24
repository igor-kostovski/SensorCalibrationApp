using System.Collections.Generic;
using System.Linq;
using SensorCalibrationApp.DeviceSelection;
using SensorCalibrationApp.Diagnostics;
using SensorCalibrationApp.FrameConfiguration;

namespace SensorCalibrationApp
{
    class MainWindowViewModel : ViewModelBase
    {
        private readonly DeviceSelectionViewModel _deviceSelectionViewModel;
        private readonly FrameConfigurationViewModel _frameConfigurationViewModel;
        private readonly DiagnosticsViewModel _diagnosticsViewModel;

        private List<ViewModelBase> _navigationStack;

        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();

                Back.RaiseCanExecuteChanged();
                Forward.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand Forward { get; set; }
        public RelayCommand Back { get; set; }

        public MainWindowViewModel(DeviceSelectionViewModel deviceSelectionViewModel, 
            FrameConfigurationViewModel frameConfigurationViewModel, 
            DiagnosticsViewModel diagnosticsViewModel)
        {
            _deviceSelectionViewModel = deviceSelectionViewModel;
            _frameConfigurationViewModel = frameConfigurationViewModel;
            _diagnosticsViewModel = diagnosticsViewModel;

            InitializeCommands();
            InitializeNavigationStack();

            CurrentViewModel = _deviceSelectionViewModel;

            _deviceSelectionViewModel.SelectionDone += (sender, args) =>
            {
                _frameConfigurationViewModel.Set(_deviceSelectionViewModel.SelectedFrame, _deviceSelectionViewModel.SelectedDevice.Type);
                Forward.RaiseCanExecuteChanged();
            };
        }

        private void InitializeCommands()
        {
            Forward = new RelayCommand(OnForward, CanGoForward);
            Back = new RelayCommand(OnBack, CanGoBack);
        }

        private void InitializeNavigationStack()
        {
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
            {
                CurrentViewModel.Unload();
                CurrentViewModel = _navigationStack.ElementAt(backIndex);
            }
        }

        private bool CanGoBack()
        {
            return CurrentViewModel != _navigationStack.First();
        }

        private void OnForward()
        {
            var currentIndex = _navigationStack.IndexOf(CurrentViewModel);
            var forwardIndex = ++currentIndex;

            if(forwardIndex < _navigationStack.Count)
            {
                CurrentViewModel.Unload();
                CurrentViewModel = _navigationStack.ElementAt(forwardIndex);
            }
        }

        private bool CanGoForward()
        {
            return _deviceSelectionViewModel.SelectedFrame != null
                && CurrentViewModel != _navigationStack.Last();
        }
    }
}
