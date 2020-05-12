using System.Collections.ObjectModel;
using System.Linq;
using SensorCalibrationApp.Common.Enums;

namespace SensorCalibrationApp.Common.Extensions
{
    public static class CommandExt
    {
        public static byte GetFrameId(this Command command)
        {
            if (command == null || command.Type == CommandType.ReadById)
                return 0x00;

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
    }
}
