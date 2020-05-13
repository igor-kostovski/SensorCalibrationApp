using SensorCalibrationApp.Common.Structs;
using SensorCalibrationApp.Domain.Models;

namespace SensorCalibrationApp.Domain.Interfaces
{
    public interface IMessageProvider
    {
        Message CreateReadByIdMessage(FrameModel frame);
        Message CreateUpdateFrameIdMessage(FrameModel frame, byte newFrameId);
        Message CreateMessageFor(FrameModel frame);
        Message CreateSubscriberMessage();
        Message CreateSaveConfigMessage();
    }
}
