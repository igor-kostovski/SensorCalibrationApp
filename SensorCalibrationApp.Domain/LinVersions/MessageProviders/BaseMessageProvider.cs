using SensorCalibrationApp.Common.Enums;
using SensorCalibrationApp.Common.Structs;
using SensorCalibrationApp.Domain.Models;

namespace SensorCalibrationApp.Domain.LinVersions.MessageProviders
{
    internal abstract class BaseMessageProvider
    {
        public Message CreateSubscriberMessage()
        {
            return new Message(0x3D, Direction.Subscriber, ChecksumType.Classic, null);
        }

        public Message CreateSaveConfigMessage()
        {
            return new Message(0x3C, Direction.Publisher, ChecksumType.Classic, new byte[] { 0x7F, 0x01, 0xB6, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF });
        }

        public Message CreateMessageFor(FrameModel frame)
        {
            return new Message(frame.FrameId, frame.Direction, frame.Checksum, frame.Bytes, frame.Length);
        }
    }
}
