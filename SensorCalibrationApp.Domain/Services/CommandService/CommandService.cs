﻿using System;
using System.Linq;
using System.Threading.Tasks;
using SensorCalibrationApp.Domain.Enums;
using SensorCalibrationApp.Domain.Factories;
using SensorCalibrationApp.Domain.Interfaces;
using SensorCalibrationApp.Domain.Models;

namespace SensorCalibrationApp.Domain.Services.CommandService
{
    public class CommandService : ICommandService
    {
        private readonly IDevice _device;
        private readonly ILinProvider _linProvider;

        public CommandService(DeviceType device, ILinProvider provider)
        {
            _device = DeviceFactory.CreateDevice(device);
            _linProvider = provider;
        }

        public Task ReadById()
        {
            return Task.Run(() =>
            {
                var message = MessageFactory.CreateReadByIdMessage();

                _linProvider.Send(message);
                _linProvider.Send(MessageFactory.CreateSubscriberMessage());
            });
        }

        public Task UpdateFrameId(byte newFrameId)
        {
            return Task.Run(() =>
            {
                _linProvider.GetPIDFor(ref newFrameId);
                var message = MessageFactory.CreateUpdateFrameIdMessage(newFrameId);

                _linProvider.Send(message);
                _linProvider.Send(MessageFactory.CreateSubscriberMessage());
            });
        }

        public Task SendDeviceSpecificFrame(FrameModel frame)
        {
            return Task.Run(() =>
            {
                var message = MessageFactory.CreateMessageFor(frame);
                _linProvider.Send(message);
            });
        }
    }
}
