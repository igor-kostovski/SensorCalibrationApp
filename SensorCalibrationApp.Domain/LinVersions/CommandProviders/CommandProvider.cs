using System.Collections.Generic;
using System.Collections.ObjectModel;
using SensorCalibrationApp.Common;
using SensorCalibrationApp.Common.Enums;
using SensorCalibrationApp.Domain.Models;

namespace SensorCalibrationApp.Domain.LinVersions.CommandProviders
{
    internal static class CommandProvider
    {
        public static ObservableCollection<Command> CreateCommands(FrameModel frame)
        {
            var assignFrameIdRange = CreateAssignFrameIdRangeCommand(frame);
            var readById = CreateReadByIdCommand();

            return new ObservableCollection<Command>
            {
                readById,
                assignFrameIdRange
            };
        }

        private static Command CreateReadByIdCommand()
        {
            return new Command
            {
                Name = "Read by identifier",
                Description = "Generic command to check if communication with LIN device is enabled",
                Type = CommandType.ReadById,
                Version = 1,
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

        private static Command CreateAssignFrameIdRangeCommand(FrameModel frame)
        {
            return new Command
            {
                Name = "Assign frame ID range",
                Description = "Updates frame ID to avoid possible conflicts due to same IDs on multiple devices",
                Type = CommandType.AssignId,
                Version = 1,
                Signals = new List<Signal>
                {
                    new Signal("NAD") {Value = 0x7F},
                    new Signal("Protocol Control Information") {Value = 0x06},
                    new Signal("Service Identifier") {Value = 0xB7},
                    new Signal("Start Index") {Value = 0x02},
                    new Signal("PID (index)") {Value = frame.FrameId, IsEnabled = true, CheckRange = true, MinValue = 0x20, MaxValue = 0x3B},
                    new Signal("PID (index) + 1") {Value = 0xFF},
                    new Signal("PID (index) + 2") {Value = 0xFF},
                    new Signal("PID (index) + 3") {Value = 0xFF}
                }
            };
        }
    }
}
