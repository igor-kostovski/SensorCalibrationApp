using System.Collections.Generic;
using System.Linq;
using SensorCalibrationApp.Common;
using SensorCalibrationApp.DeviceSelection;
using SensorCalibrationApp.Diagnostics;
using SensorCalibrationApp.Domain;
using SensorCalibrationApp.FrameConfiguration;

namespace SensorCalibrationApp
{
    class MainWindowViewModel : ViewModelBase
    {
        private readonly DeviceSelectionViewModel _deviceSelectionViewModel;
        private readonly FrameConfigurationViewModel _frameConfigurationViewModel;
        private readonly DiagnosticsViewModel _diagnosticsViewModel;
        private readonly EventManager _eventManager;

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

        private ErrorMessage _errorMessage;
        public ErrorMessage ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }


        public RelayCommand Forward { get; set; }
        public RelayCommand Back { get; set; }

        public MainWindowViewModel(DeviceSelectionViewModel deviceSelectionViewModel, 
            FrameConfigurationViewModel frameConfigurationViewModel, 
            DiagnosticsViewModel diagnosticsViewModel,
            EventManager eventManager)
        {
            #region InjectingDependencies

            _deviceSelectionViewModel = deviceSelectionViewModel;
            _frameConfigurationViewModel = frameConfigurationViewModel;
            _diagnosticsViewModel = diagnosticsViewModel;
            _eventManager = eventManager;

            #endregion

            InitializeCommands();
            InitializeNavigationStack();

            CurrentViewModel = _navigationStack.First();

            AssignEvents();
        }

        private void AssignEvents()
        {
            _eventManager.PushError += (sender, error) =>
            {
                ErrorMessage = new ErrorMessage(error);
            };

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

        public override void Unload()
        {
            CurrentViewModel.Unload();
        }
    }
}
