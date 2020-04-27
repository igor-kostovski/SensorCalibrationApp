using SensorCalibrationApp.Domain;
using SensorCalibrationApp.Domain.Services.CommandService;

namespace SensorCalibrationApp.Diagnostics
{
    class DiagnosticsViewModel : ViewModelBase
    {
        private readonly ICommandService _commandService;
        private readonly EventManager _eventManager;

        public DiagnosticsViewModel(ICommandService commandService, EventManager eventManager)
        {
            _commandService = commandService;
            _eventManager = eventManager;
        }

        public void Load()
        {
            _eventManager.PushData += OnNewData;
        }

        public override void Unload()
        {
            _eventManager.PushData -= OnNewData;
        }

        private void OnNewData(object sender, string e)
        {
            throw new System.NotImplementedException();
        }
    }
}
