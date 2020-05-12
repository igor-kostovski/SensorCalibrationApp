using System;
using SensorCalibrationApp.Common.Structs;
using SensorCalibrationApp.Domain.Interfaces;
using SensorCalibrationApp.Domain.Models;

namespace SensorCalibrationApp.Domain.MessageProviders
{
    internal class LegacyMessageProvider : BaseMessageProvider, IMessageProvider
    {
        public Message CreateReadByIdMessage(FrameModel frame)
        {
            throw new NotImplementedException();
        }

        public Message CreateUpdateFrameIdMessage(FrameModel frame)
        {
            throw new NotImplementedException();
        }
    }
}
