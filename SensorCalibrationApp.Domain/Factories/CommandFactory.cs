using System.Collections.Generic;
using System.Collections.ObjectModel;
using SensorCalibrationApp.Common;
using SensorCalibrationApp.Domain.Services.CommandService;

namespace SensorCalibrationApp.Domain.Factories
{
    public static class CommandFactory
    {
        public static ObservableCollection<Command> CreateCommands(byte frameId, ICommandService service)
        {
            var assignFrameId = CreateAssignFrameIdCommand(frameId, service.UpdateFrameId);
            var readById = CreateReadByIdCommand(service.ReadById);

            return new ObservableCollection<Command>
            {
                readById,
                assignFrameId
            };
        }

        private static Command CreateReadByIdCommand(CommandWithoutParams command)
        {
            return new Command(command)
            {
                Name = "Read by identifier",
                Description = "Generic command to check if communication with LIN device is enabled",
                Signals = new List<Signal>
                {
                    new Signal("NAD") {Value = 0x7F},
                    new Signal("Protocol Control Information") {Value = 0x06},
                    new Signal("Service Identifier") {Value = 0xB2},
                    new Signal("Identifier") {Value = 0x00},
                    new Signal("Supplier ID LSB") {Value = 0xFF},
                    new Signal("Supplier ID MSB") {Value = 0x7F},
                    new Signal("Function ID LSB") {Value = 0x7F},
                    new Signal("Function ID MSB") {Value = 0x7F}
                }
            };
        }

        private static Command CreateAssignFrameIdCommand(byte frameId, CommandWithParams<byte> command)
        {
            return new Command(command)
            {
                Name = "Assign frame ID",
                Description = "Updates frame ID to avoid possible conflicts due to same IDs on multiple devices",
                Signals = new List<Signal>
                {
                    new Signal("NAD") {Value = 0x7F},
                    new Signal("Protocol Control Information") {Value = 0x06},
                    new Signal("Service Identifier") {Value = 0xB7},
                    new Signal("Start Index") {Value = 0x02},
                    new Signal("PID (index)") {Value = frameId, IsEnabled = true},
                    new Signal("PID (index) + 1") {Value = 0xFF},
                    new Signal("PID (index) + 2") {Value = 0xFF},
                    new Signal("PID (index) + 3") {Value = 0xFF}
                }
            };
        }
    }
}
