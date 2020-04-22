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
        private readonly EventManager _eventManager;

        public CommandService(DeviceType device, ILinProvider provider)
        {
            _device = DeviceFactory.CreateDevice(device);
            _linProvider = provider;
            _eventManager = new EventManager(_device);

            AssignEvents();
        }

        private void AssignEvents()
        {
            _linProvider.OnError += _eventManager.HandleError;
            _linProvider.OnRead += _eventManager.HandleRead;
        }

        public Task ReadById()
        {
            _eventManager.UseDeviceParser = false;

            return Task.Run(() =>
            {
                var message = MessageFactory.CreateReadByIdMessage();

                _linProvider.Send(message);
                _linProvider.Send(MessageFactory.CreateSubscriberMessage());
            });
        }

        public Task UpdateFrameId(byte newFrameId)
        {
            _eventManager.UseDeviceParser = false;

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
            _eventManager.UseDeviceParser = true;

            return Task.Run(() =>
            {
                var message = _device.CreateMessageFor(frame);
                _linProvider.Send(message);
            });
        }
    }
}
