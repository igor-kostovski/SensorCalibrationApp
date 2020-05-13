using System.Collections.ObjectModel;
using SensorCalibrationApp.Common;
using SensorCalibrationApp.Domain.LinVersions.CommandProviders;
using SensorCalibrationApp.Domain.Models;

namespace SensorCalibrationApp.Domain.Factories
{
    public static class CommandFactory
    {
        public static ObservableCollection<Command> CreateCommands(FrameModel frame)
        {
            if (frame.Device.IncludeSaveConfig)
                return CommandProvider.CreateCommands(frame);

            return LegacyCommandProvider.CreateCommands(frame);
        }
    }
}
