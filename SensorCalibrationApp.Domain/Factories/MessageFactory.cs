using System;
using SensorCalibrationApp.Common.Enums;
using SensorCalibrationApp.Common.Structs;
using SensorCalibrationApp.Domain.Models;

namespace SensorCalibrationApp.Domain.Factories
{
    internal static class MessageFactory
    {
        public static Message CreateMessageFor(FrameModel frame)
        {
            switch (frame.Name)
            {
                case "DTSs_01":
                    return new Message(frame.FrameId, Direction.Subscriber, ChecksumType.Enhanced, null, 5);
                default:
                    throw new NotImplementedException();
            }
        }

        public static Message CreateReadByIdMessage()
        {
            return new Message(
                0x3C, 
                Direction.Publisher, 
                ChecksumType.Classic, 
                new byte[] { 0x7F, 0x06, 0xB2, 0x00, 0xFF, 0x7F, 0xFF, 0xFF });
        }

        public static Message CreateUpdateFrameIdMessage(byte newFrameId)
        {
            return new Message(
                0x3C,
                Direction.Publisher,
                ChecksumType.Classic,
                new byte[] { 0x7F, 0x06, 0xB7, 0x02, newFrameId, 0xFF, 0xFF, 0xFF });
        }

        public static Message CreateSubscriberMessage()
        {
            return new Message(0x3D, Direction.Subscriber, ChecksumType.Classic, null);
        }
    }
}
