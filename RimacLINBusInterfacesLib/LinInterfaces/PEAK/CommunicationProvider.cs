using System;
using System.Threading;
using RimacLINBusInterfacesLib.Enums;
using RimacLINBusInterfacesLib.Extensions;
using RimacLINBusInterfacesLib.Structs;
using HardwareHandle = System.UInt16;
using ClientHandle = System.Byte;

namespace RimacLINBusInterfacesLib.LinInterfaces.PEAK
{
    internal class CommunicationProvider
    {
        private volatile bool _isReceiveThreadAlive;
        private Thread _receiveThread;
        private ClientHandle _clientHandle;
        private HardwareHandle _hardwareHandle;

        public event EventHandler<byte[]> OnData;
        public event EventHandler<string> OnError;

        public CommunicationProvider(ClientHandle clientHandle, HardwareHandle hardwareHandle)
        {
            _clientHandle = clientHandle;
            _hardwareHandle = hardwareHandle;
        }

        public void Send(Message message)
        {
            PLinApi.CalculateChecksum(ref message);
            PLinApi.GetPID(ref message.FrameId);

            var result = PLinApi.Write(_clientHandle, _hardwareHandle, ref message);
            if (result != LinError.Ok)
                throw new CommunicationError($"Error while sending a message: {result.ToString().ToSentence()}");
        }

        public void SetupReceiveThread()
        {
            _isReceiveThreadAlive = true;

            _receiveThread = new Thread(() =>
            {
                while (_isReceiveThreadAlive)
                {
                    ReadAllFromQueue();
                    Thread.Sleep(500);
                }
            });

            _receiveThread.IsBackground = true;
            _receiveThread.Start();
        }

        public void RemoveReceiveThread()
        {
            _isReceiveThreadAlive = false;
            _receiveThread = null;
        }

        private void ReadAllFromQueue()
        {
            LinError readResult;

            do
            {
                ReceivedMessage msg;

                readResult = PLinApi.Read(_clientHandle, out msg);
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
