using System.Threading.Tasks;
using SensorCalibrationApp.Domain.Enums;
using SensorCalibrationApp.Domain.Factories;
using SensorCalibrationApp.Domain.Interfaces;
using SensorCalibrationApp.Domain.Models;

namespace SensorCalibrationApp.Domain.Services.CommandService
{
    public class CommandService : ICommandService
    {
        private IDevice _device;
        private EventManager _eventManager;

        private readonly ILinProvider _linProvider;

        public CommandService(ILinProvider provider)
        {
            _linProvider = provider;

            AssignEvents();
        }

        public void Load(IDevice device)
        {
            _device = device;
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
