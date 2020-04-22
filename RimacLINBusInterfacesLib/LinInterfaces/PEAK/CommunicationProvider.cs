using System;
using System.Threading;
using SensorCalibrationApp.Common.Enums;
using SensorCalibrationApp.Common.Structs;
using HardwareHandle = System.UInt16;
using ClientHandle = System.Byte;

namespace RimacLINBusInterfacesLib.LinInterfaces.PEAK
{
    internal class CommunicationProvider
    {
        private volatile bool isReceiveThreadAlive;
        private Thread receiveThread;
        private ClientHandle linClientHandle;
        private HardwareHandle linHardwareHandle;

        public event EventHandler<byte[]> OnData;
        public event EventHandler<string> OnError;

        public CommunicationProvider(ClientHandle clientHandle, HardwareHandle hardwareHandle)
        {
            linClientHandle = clientHandle;
            linHardwareHandle = hardwareHandle;
        }

        public void Send(Message message)
        {
            PLinApi.CalculateChecksum(ref message);
            PLinApi.GetPID(ref message.FrameId);

            var result = PLinApi.Write(linClientHandle, linHardwareHandle, ref message);
            if (result != LinError.Ok)
                throw new CommunicationError($"Error while sending a message: {result.ToString()}");
        }

        public void SetupReceiveThread()
        {
            isReceiveThreadAlive = true;

            receiveThread = new Thread(() =>
            {
                while (isReceiveThreadAlive)
                {
                    ReadAllFromQueue();
                    Thread.Sleep(500);
                }
            });

            receiveThread.IsBackground = true;
            receiveThread.Start();
        }

        public void RemoveReceiveThread()
        {
            isReceiveThreadAlive = false;
            receiveThread = null;
        }

        private void ReadAllFromQueue()
        {
            LinError readResult;

            do
            {
                ReceivedMessage msg;

                readResult = PLinApi.Read(linClientHandle, out msg);
                var (processorResult, processorMessage) = MessageProcessor.Process(readResult, msg);

                switch (processorResult)
                {
                    case MessageProcessorResult.EmptyQueue:
                        break;
                    case MessageProcessorResult.Error:
                    case MessageProcessorResult.Info:
                        OnError?.Invoke(this, processorMessage);
                        break;
                    case MessageProcessorResult.Regular:
                        OnData?.Invoke(this, msg.Data);
                        break;
                    default:
                        throw new NotImplementedException();
                }
            } while (readResult == LinError.Ok);
        }
    }
}
