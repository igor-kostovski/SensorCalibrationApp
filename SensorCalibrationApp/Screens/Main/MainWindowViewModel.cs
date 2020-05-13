using System.Collections.Generic;
using System.Linq;
using SensorCalibrationApp.Common;
using SensorCalibrationApp.Domain;
using SensorCalibrationApp.Domain.Services.CommandService;
using SensorCalibrationApp.Screens.DeviceSelection;
using SensorCalibrationApp.Screens.Diagnostics;
using SensorCalibrationApp.Screens.FrameConfiguration;
using SensorCalibrationApp.Screens.FrameManagement;

namespace SensorCalibrationApp.Screens.Main
{
    class MainWindowViewModel : ViewModelBase
    {
        private readonly DeviceSelectionViewModel _deviceSelectionViewModel;
        private readonly FrameConfigurationViewModel _frameConfigurationViewModel;
        private readonly DiagnosticsViewModel _diagnosticsViewModel;
        private readonly FrameManagementViewModel _frameManagementViewModel;
        private readonly EventManager _eventManager;
        private readonly ICommandService _commandService;

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

        private int _currentViewModelIndex;
        public int CurrentViewModelIndex
        {
            get { return _currentViewModelIndex; }
            set
            {
                _currentViewModelIndex = value; 
                OnPropertyChanged();
            }
        }

        public RelayCommand Forward { get; set; }
        public RelayCommand Back { get; set; }
        public RelayCommand Add { get; set; }

        public MainWindowViewModel(DeviceSelectionViewModel deviceSelectionViewModel, 
            FrameConfigurationViewModel frameConfigurationViewModel, 
            DiagnosticsViewModel diagnosticsViewModel,
            FrameManagementViewModel frameManagementViewModel,
            EventManager eventManager,
            ICommandService commandService)
        {
            #region InjectingDependencies

            _deviceSelectionViewModel = deviceSelectionViewModel;
            _frameConfigurationViewModel = frameConfigurationViewModel;
            _diagnosticsViewModel = diagnosticsViewModel;
            _frameManagementViewModel = frameManagementViewModel;
            _eventManager = eventManager;
            _commandService = commandService;

            #endregion

            InitializeCommands();
            InitializeNavigationStack();
            AssignEvents();

            CurrentViewModel = _navigationStack.First();
            _commandService.OpenConnection();
        }

        private void InitializeCommands()
        {
            Forward = new RelayCommand(OnForward, CanGoForward);
            Back = new RelayCommand(OnBack, CanGoBack);
            Add = new RelayCommand(OnAdd);
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

        private void AssignEvents()
        {
            _eventManager.PushError += HandleError;
            _deviceSelectionViewModel.SelectionChanged += HandleSelectionChanged;
        }

        private void HandleError(object sender, string error)
        {
            ErrorMessage = new ErrorMessage(error);
        }

        private void HandleSelectionChanged(object sender, bool isFrameSelected)
        {
            if (isFrameSelected)
                SetFrameOnDependentViewModels();

            Forward.RaiseCanExecuteChanged();
        }

        private void SetFrameOnDependentViewModels()
        {
            _frameConfigurationViewModel.Set(_deviceSelectionViewModel.SelectedFrame);
            _diagnosticsViewModel.Set(_deviceSelectionViewModel.SelectedFrame);
        }

        private void OnBack()
        {
            if (CurrentViewModel == _frameManagementViewModel)
            {
                CurrentViewModel.Unload();
                CurrentViewModel = _navigationStack.First();
                CurrentViewModelIndex = 0;
                return;
            }

            var currentIndex = _navigationStack.IndexOf(CurrentViewModel);
            var backIndex = --currentIndex;

            if(backIndex > -1)
            {
                CurrentViewModel.Unload();
                CurrentViewModel = _navigationStack.ElementAt(backIndex);
                CurrentViewModelIndex = backIndex;
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
                CurrentViewModelIndex = forwardIndex;
            }
        }

        private bool CanGoForward()
        {
            return DeviceSelectionConditions()
                && CurrentViewModel != _navigationStack.Last()
                && CurrentViewModel != _frameManagementViewModel;
        }

        private void OnAdd()
        {
            CurrentViewModel.Unload();
            CurrentViewModel = _frameManagementViewModel;
            CurrentViewModelIndex = -1;
        }

        private bool DeviceSelectionConditions()
        {
            if (CurrentViewModel != _deviceSelectionViewModel)
                return true;

            return _deviceSelectionViewModel.SelectedFrame != null;
        }

        public override void Unload()
        {
            CurrentViewModel.Unload();
            UnassignEvents();
        }

        private void UnassignEvents()
        {
            _eventManager.PushError -= HandleError;
            _deviceSelectionViewModel.SelectionChanged -= HandleSelectionChanged;
        }
    }
}
