using SensorCalibrationApp.Domain.Models;

namespace SensorCalibrationApp.FrameConfiguration
{
    class FrameConfigurationViewModel : ViewModelBase
    {
        private FrameModel _frame;
        public FrameModel Frame
        {
            get { return _frame; }
            set
            {
                _frame = value;
                OnPropertyChanged();
            }
        }

    }
}
