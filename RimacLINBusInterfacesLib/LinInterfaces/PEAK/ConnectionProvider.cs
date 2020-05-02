using System.Linq;
using SensorCalibrationApp.Common;
using SensorCalibrationApp.Common.Enums;
using SensorCalibrationApp.Common.Extensions;
using HardwareHandle = System.UInt16;
using ClientHandle = System.Byte;

namespace RimacLINBusInterfacesLib.LinInterfaces.PEAK
{
    internal class ConnectionProvider
    {
        private ClientHandle linClientHandle;
        private HardwareHandle linHardwareHandle;
        private LinConfiguration _config;

        public ConnectionProvider(LinConfiguration config)
        {
            _config = config;
        }

        public (ClientHandle, HardwareHandle) Open()
        {
            CheckIfConnectionExists();
            Register();
            DetermineHardware();
            Connect();
            Initialize(_config.BaudRate, _config.HardwareMode);
            SetClientFilter();
            KeepAlive();

            return (linClientHandle, linHardwareHandle);
        }

        public void Close()
        {
            Disconnect();
            linClientHandle = 0;
        }

        private void CheckIfConnectionExists()
        {
            if (linClientHandle != 0)
                throw new ConnectionError("Connection with this client already exists");
        }

        private void Register()
        {
            var result = PLinApi.RegisterClient("Master", 0, out linClientHandle);
            if (result != LinError.Ok)
                throw new ConnectionError($"Error while registering a client: {result.ToString().ToSentence()}");
        }

        protected void DetermineHardware()
        {
            var hardwareHandles = new HardwareHandle[5];
            ushort hardwareHandlesCount;

            var result = PLinApi.GetAvailableHardware(hardwareHandles,
                (ushort)(hardwareHandles.Length * sizeof(HardwareHandle)), out hardwareHandlesCount);
            if (result != LinError.Ok)
                throw new ConnectionError($"Error while getting available hardware: {result.ToString().ToSentence()}");

            linHardwareHandle = hardwareHandles.First();
        }

        private void Connect()
        {
            var result = PLinApi.ConnectClient(linClientHandle, linHardwareHandle);
            if (result != LinError.Ok)
                throw new ConnectionError($"Error while connecting a client: {result.ToString().ToSentence()}");
        }

        private void Initialize(BaudRate baudRate, HardwareMode hardwareMode)
        {
            var result = PLinApi.InitializeHardware(linClientHandle, linHardwareHandle, hardwareMode, (ushort)baudRate);
            if (result != LinError.Ok)
                throw new ConnectionError($"Error while initializing hardware: {result.ToString().ToSentence()}");
        }

        private void SetClientFilter()
        {
            var result = PLinApi.RegisterFrameId(linClientHandle, linHardwareHandle, 0x01, 0x3D);
            if (result != LinError.Ok)
                throw new ConnectionError($"Error while setting client filter: {result.ToString().ToSentence()}");
        }

        protected void KeepAlive()
        {
            var result = PLinApi.StartKeepAlive(linClientHandle, linHardwareHandle, 0x00, 1000);
            if (result != LinError.Ok)
                throw new ConnectionError($"Error while starting a keep alive session: {result.ToString().ToSentence()}");
        }

        private void Disconnect()
        {
            var result = PLinApi.RemoveClient(linClientHandle);
            if (result != LinError.Ok)
                throw new ConnectionError($"Error while removing a client: {result.ToString()}");
        }
    }
}
