using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace SensorCalibrationApp.Common.Extensions
{
    public static class CommandExt
    {
        public static bool IsReadById(this Command command)
        {
            if (command == null)
                return false;

            return !command.Signals.Any(x => x.IsEnabled);
        }

        public static bool IsAssignId(this Command command)
        {
            if (command == null)
                return false;

            return command.Signals.Any(x => x.IsEnabled);
        }

        public static byte GetFrameId(this Command command)
        {
            if (command == null)
                return 0x00;

            if(command.IsAssignId())
                return command.Signals.Single(x => x.IsEnabled).Value;

            Debug.WriteLine("Cannot get Id on this type of command.");
            return 0x00;
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
