using RimacLINBusInterfacesLib.Enums;
using RimacLINBusInterfacesLib.Structs;
using SensorCalibrationApp.Domain.Interfaces;
using SensorCalibrationApp.Domain.Models;

namespace SensorCalibrationApp.Domain.LinVersions.MessageProviders
{
    internal class MessageProvider : BaseMessageProvider, IMessageProvider
    {
        public Message CreateReadByIdMessage(FrameModel frame)
        {
            return new Message(
                0x3C,
                Direction.Publisher,
                ChecksumType.Classic,
                new byte[] { 0x7F, 0x06, 0xB2, 0x00, 0xFF, 0x7F, 0xFF, 0xFF });
        }

        public Message CreateUpdateFrameIdMessage(FrameModel frame, byte newFrameId)
        {
            return new Message(
                0x3C,
                Direction.Publisher,
                ChecksumType.Classic,
                new byte[] { 0x7F, 0x06, 0xB7, 0x02, newFrameId, 0xFF, 0xFF, 0xFF });
        } 
    }
}
