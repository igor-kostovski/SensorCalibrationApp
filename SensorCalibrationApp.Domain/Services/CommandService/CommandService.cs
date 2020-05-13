using System.Threading.Tasks;
using SensorCalibrationApp.Domain.Factories;
using SensorCalibrationApp.Domain.Interfaces;
using SensorCalibrationApp.Domain.Models;

namespace SensorCalibrationApp.Domain.Services.CommandService
{
    public class CommandService : ICommandService
    {
        private readonly EventManager _eventManager;
        private readonly ILinProvider _linProvider;

        public CommandService(ILinProvider provider, EventManager eventManager)
        {
            _linProvider = provider;
            _eventManager = eventManager;

            AssignEvents();
        }

        private void AssignEvents()
        {
            _linProvider.OnError += _eventManager.HandleError;
            _linProvider.OnRead += _eventManager.HandleRead;
        }

        public void OpenConnection()
        {
            _linProvider.OpenConnection();
        }

        public Task ReadById(FrameModel frame)
        {
            return Task.Run(() =>
            {
                var messageProvider = MessageProviderFactory.Create(frame.Device.IncludeSaveConfig);
                var message = messageProvider.CreateReadByIdMessage(frame);

                _linProvider.Send(message);
                _linProvider.Send(messageProvider.CreateSubscriberMessage());
            });
        }

        public Task UpdateFrameId(FrameModel frame)
        {
            return Task.Run(() =>
            {
                var newFrameId = frame.FrameId;
                _linProvider.GetPIDFor(ref newFrameId);

                var messageProvider = MessageProviderFactory.Create(frame.Device.IncludeSaveConfig);
                var message = messageProvider.CreateUpdateFrameIdMessage(frame, newFrameId);

                _linProvider.Send(message);

                if(frame.Device.IncludeSaveConfig)
                    _linProvider.Send(messageProvider.CreateSaveConfigMessage());

                _linProvider.Send(messageProvider.CreateSubscriberMessage());
            });
        }

        public Task SendDeviceSpecificFrame(FrameModel frame)
        {
            return Task.Run(() =>
            {
                var messageProvider = MessageProviderFactory.Create(frame.Device.IncludeSaveConfig);
                var message = messageProvider.CreateMessageFor(frame);
                _linProvider.Send(message);
            });
        }

        public void Dispose()
        {
            _linProvider.OnRead -= _eventManager.HandleRead;
            _linProvider.OnError -= _eventManager.HandleError;
        }
    }
}
