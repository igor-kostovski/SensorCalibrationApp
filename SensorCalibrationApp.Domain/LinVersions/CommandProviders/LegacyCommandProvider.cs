﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using SensorCalibrationApp.Common;
using SensorCalibrationApp.Common.Enums;
using SensorCalibrationApp.Domain.Models;

namespace SensorCalibrationApp.Domain.LinVersions.CommandProviders
{
    internal static class LegacyCommandProvider
    {
        public static ObservableCollection<Command> CreateCommands(FrameModel frame)
        {
            var assignFrameIdRange = CreateAssignFrameIdCommand(frame);
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
                Version = 0,
                Signals = new List<Signal>
                {
                    new Signal("NAD") {Value = 0x7F},
                    new Signal("Protocol Control Information") {Value = 0x06},
                    new Signal("Service Identifier") {Value = 0xB2},
                    new Signal("Identifier") {Value = 0x00, IsEnabled = true, CheckRange = true, MinValue = 0x10, MaxValue = 0x1F, AllowedBytes = new List<byte>{0x00}},
                    new Signal("Supplier ID LSB") {Value = 0xFF},
                    new Signal("Supplier ID MSB") {Value = 0x7F},
                    new Signal("Function ID LSB") {Value = 0x7F},
                    new Signal("Function ID MSB") {Value = 0x7F}
                }
            };
        }

        private static Command CreateAssignFrameIdCommand(FrameModel frame)
        {
            return new Command
            {
                Name = "Assign frame ID",
                Description = "Updates frame ID to avoid possible conflicts due to same IDs on multiple devices",
                Type = CommandType.AssignId,
                Version = 0,
                Signals = new List<Signal>
                {
                    new Signal("NAD") {Value = 0x7F},
                    new Signal("Protocol Control Information") {Value = 0x06},
                    new Signal("Service Identifier") {Value = 0xB1},
                    new Signal("Supplier ID LSB") {Value = 0xFF},
                    new Signal("Supplier ID MSB") {Value = 0x7F},
                    new Signal("Message ID LSB") {Value = 0xFF, IsEnabled = true},
                    new Signal("Message ID MSB") {Value = 0xFF, IsEnabled = true},
                    new Signal("PID") {Value = frame.FrameId, IsEnabled = true, CheckRange = true, MinValue = 0x00, MaxValue = 0x3B}
                }
            };
        }
    }
}
