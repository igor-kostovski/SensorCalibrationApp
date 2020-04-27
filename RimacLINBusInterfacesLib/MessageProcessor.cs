﻿using System;
using System.Linq;
using SensorCalibrationApp.Common.Enums;
using SensorCalibrationApp.Common.Extensions;
using SensorCalibrationApp.Common.Structs;

namespace RimacLINBusInterfacesLib
{
    internal static class MessageProcessor
    {
        public static (MessageProcessorResult, string) Process(LinError result, ReceivedMessage message)
        {
            switch (result)
            {
                case LinError.Ok:
                    return ProcessSuccessfulRead(message);
                case LinError.ReceiveQueueEmpty:
                    return (MessageProcessorResult.EmptyQueue, null);
                default:
                    return (MessageProcessorResult.Error,$"Error while reading message: {result.ToString().ToSentence()}");
            }
        }

        private static (MessageProcessorResult, string) ProcessSuccessfulRead(ReceivedMessage message)
        {
            switch (message.Type)
            {
                case MessageType.Standard:
                    return ProcessStandardMessage(message);
                default:
                    return (MessageProcessorResult.Info, $"Error while reading message: {message.Type.ToString().ToSentence()}");
            }
        }

        private static (MessageProcessorResult, string) ProcessStandardMessage(ReceivedMessage message)
        {
            if (message.Direction == Direction.Publisher)
                return (MessageProcessorResult.EmptyQueue, null);

            var error = ProcessErrorFlags(message.ErrorFlags);

            switch (error)
            {
                case MessageErrors.Ok:
                    return (MessageProcessorResult.Regular, null);
                default:
                    return (MessageProcessorResult.Error, $"Error while reading message: {error.ToString().ToSentence()}");
            }
        }

        private static MessageErrors ProcessErrorFlags(MessageErrors errorFlags)
        {
            if (errorFlags == 0)
                return MessageErrors.Ok;

            var errorOffset = 24;
            var result = (int)errorFlags - errorOffset;

            if (result == 0)
                return MessageErrors.SlaveNotResponding;

            return (MessageErrors)BitConverter.GetBytes(result).First();
        }
    }
}
