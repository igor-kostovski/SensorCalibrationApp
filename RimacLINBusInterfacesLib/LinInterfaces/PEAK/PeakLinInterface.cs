using System;
using SensorCalibrationApp.Common;
using SensorCalibrationApp.Common.Structs;
using SensorCalibrationApp.Domain.Interfaces;

namespace RimacLINBusInterfacesLib.LinInterfaces.PEAK
{
    public class PeakLinInterface : ILinProvider
    {
        public event EventHandler<byte[]> OnRead;
        public event EventHandler<string> OnError;

        private readonly LinConfiguration _config;
        private readonly ConnectionProvider _connectionProvider;
        private CommunicationProvider _communicationProvider;

        public PeakLinInterface(LinConfiguration configuration)
        {
            _config = configuration;
            _connectionProvider = new ConnectionProvider(_config);
        }

        public void OpenConnection()
        {
            try
            {
                var (clientHandle, hardwareHandle) = _connectionProvider.Open();
                _communicationProvider = new CommunicationProvider(clientHandle, hardwareHandle);

                AssignEvents();

                _communicationProvider.SetupReceiveThread();
            }
            catch (ConnectionError err)
            {
                CloseConnection();

                OnError?.Invoke(this, err.Message);
            }
        }

        private void AssignEvents()
        {
            _communicationProvider.OnData += OnRead;
            _communicationProvider.OnError += OnError;
        }

        public void CloseConnection()
        {
            try
            {
                _connectionProvider.Close();
            }
            catch (ConnectionError err)
            {
                OnError?.Invoke(this, err.Message);
            }
        }

        public void Send(Message message)
        {
            try
            {
                _communicationProvider?.Send(message);
            }
            catch (CommunicationError err)
            {
                OnError?.Invoke(this, err.Message);
            }
        }

        public void Dispose()
        {
            _communicationProvider?.RemoveReceiveThread();
            CloseConnection();
        }

        public void GetPIDFor(ref byte frameId) => PLinApi.GetPID(ref frameId);
    }
}
