using System.Threading.Tasks;
using SensorCalibrationApp.Domain.Devices;
using SensorCalibrationApp.Domain.Factories;
using SensorCalibrationApp.Domain.Interfaces;
using SensorCalibrationApp.Domain.Models;

namespace SensorCalibrationApp.Domain.Services.CommandService
{
    public class CommandService : DeviceBound, ICommandService
    {
        private readonly EventManager _eventManager;
        private readonly ILinProvider _linProvider;

        public CommandService(ILinProvider provider, EventManager eventManager)
        {
            _linProvider = provider;
            _eventManager = eventManager;

            AssignEvents();
            _linProvider.OpenConnection();
        }

        private void AssignEvents()
        {
            _linProvider.OnError += _eventManager.HandleError;
            _linProvider.OnRead += _eventManager.HandleRead;
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
                var message = _device.CreateMessageFor(frame);
                _linProvider.Send(message);
            });
        }
    }
}
