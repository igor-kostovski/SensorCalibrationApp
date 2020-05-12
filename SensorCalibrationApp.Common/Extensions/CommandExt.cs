using System.Collections.ObjectModel;
using System.Linq;
using SensorCalibrationApp.Common.Enums;

namespace SensorCalibrationApp.Common.Extensions
{
    public static class CommandExt
    {
        public static byte? GetFrameId(this Command command)
        {
            if (command == null || command.Type == CommandType.ReadById)
                return null;

            return command.Version == 0 ? 
                command.Signals.Last(x => x.IsEnabled).Value : 
                command.Signals.First(x => x.IsEnabled).Value;
        }

        public static Command SelectByName(this ObservableCollection<Command> commands, string name)
        {
            commands.Where(x => x.Name != name && x.IsSelected)
                .ToList()
                .ForEach(x => x.IsSelected = false);

            return commands.SingleOrDefault(x => x.IsSelected);
        }

        public static bool AreSignalsInRange(this Command command)
        {
            var forCheck = command.Signals
                .Where(x => x.CheckRange)
                .ToList();

            var inRange = forCheck
                .Where(x => x.Value.IsInRange(x.MinValue, x.MaxValue))
                .ToList();

            var allowed = forCheck
                .Where(x => !inRange.Contains(x))
                .Where(x => x.AllowedBytes?.Contains(x.Value) ?? false);

            return forCheck.Count == (inRange.Count + allowed.Count());
        }
    }
}
