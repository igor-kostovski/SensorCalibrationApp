﻿using System.Collections.Generic;
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

        public MainWindowViewModel()
        {
            Forward = new RelayCommand(OnForward, CanGoForward);
            Back = new RelayCommand(OnBack, CanGoBack);

            CurrentViewModel = _deviceSelectionViewModel;

            _deviceSelectionViewModel.SelectionDone += (sender, args) => Forward.RaiseCanExecuteChanged();

            InitializeNavigationStack();
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
                CurrentViewModel = _navigationStack.ElementAt(backIndex);
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
                CurrentViewModel = _navigationStack.ElementAt(forwardIndex);
        }

        private bool CanGoForward()
        {
            return _deviceSelectionViewModel.SelectedFrame != null
                && CurrentViewModel != _navigationStack.Last();
        }
    }
}
