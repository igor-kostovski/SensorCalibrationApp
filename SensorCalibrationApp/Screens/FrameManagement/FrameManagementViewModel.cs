using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Media;
using SensorCalibrationApp.Common.Enums;
using SensorCalibrationApp.Domain.Models;
using SensorCalibrationApp.Domain.Services;

namespace SensorCalibrationApp.Screens.FrameManagement
{
    class FrameManagementViewModel : ViewModelBase
    {
        private readonly IFrameService _frameService;

        private ObservableCollection<FrameModel> _frames;
        public ObservableCollection<FrameModel> Frames
        {
            get
            {
                return _frames;
            }
            set
            {
                _frames = value;
                OnPropertyChanged();
            }
        }

        private FrameModel _selectedFrame;
        public FrameModel SelectedFrame
        {
            get
            {
                return _selectedFrame;
            }
            set
            {
                _selectedFrame = value;
                OnPropertyChanged();

                Delete.RaiseCanExecuteChanged();
                Clear.RaiseCanExecuteChanged();
            }
        }

        public List<Direction> Directions { get; set; }

        public RelayCommand Save { get; set; }
        public RelayCommand Delete { get; set; }
        public RelayCommand Clear { get; set; }

        public FrameManagementViewModel(IFrameService frameService)
        {
            _frameService = frameService;
            InitializeCommands();

            Directions = new List<Direction>
            {
                Direction.Disabled,
                Direction.Publisher,
                Direction.Subscriber,
                Direction.SubscriberAutoLength
            };
        }

        private void InitializeCommands()
        {
            Save = new RelayCommand(OnSave, CanSave);
            Delete = new RelayCommand(OnDelete, CanDelete);
            Clear = new RelayCommand(OnClear, CanDelete);
        }

        public async void Load()
        {
            Frames = new ObservableCollection<FrameModel>(await _frameService.GetAll());
            SelectedFrame = new FrameModel();
        }

        private bool CanDelete()
        {
            return SelectedFrame?.Id != 0;
        }

        private void OnDelete()
        {
            Debug.WriteLine("Deleted");
        }

        private bool CanSave()
        {
            return true;
        }

        private void OnSave()
        {
            if(SelectedFrame.Id != 0)
                Debug.WriteLine("Updated");
            else
                Debug.WriteLine("Added");
        }

        private void OnClear()
        {
            SelectedFrame = new FrameModel();
        }
    }
}
