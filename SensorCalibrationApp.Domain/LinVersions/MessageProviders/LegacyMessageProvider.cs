using System.Linq;
using SensorCalibrationApp.Common.Enums;
using SensorCalibrationApp.Common.Structs;
using SensorCalibrationApp.Domain.Interfaces;
using SensorCalibrationApp.Domain.Models;

namespace SensorCalibrationApp.Domain.LinVersions.MessageProviders
{
    internal class LegacyMessageProvider : BaseMessageProvider, IMessageProvider
    {
        public Message CreateReadByIdMessage(FrameModel frame)
        {
            return new Message(
                0x3C,
                Direction.Publisher,
                ChecksumType.Classic,
                frame.LegacyCommandBytes);
        }

        public Message CreateUpdateFrameIdMessage(FrameModel frame, byte newFrameId)
        {
            return new Message(
                0x3C,
                Direction.Publisher,
                ChecksumType.Classic,
                InsertFrameId(frame, newFrameId));
        }

        private static byte[] InsertFrameId(FrameModel frame, byte newFrameId)
        {
            var bytes = frame.LegacyCommandBytes.ToList();

            bytes.RemoveAt(frame.LegacyCommandBytes.Length - 1);
            bytes.Add(newFrameId);

            return bytes.ToArray();
        }
    }
}
