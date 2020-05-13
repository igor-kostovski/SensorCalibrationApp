using System;
using RimacLINBusInterfacesLib.Structs;

namespace RimacLINBusInterfacesLib.Interfaces
{
    public interface ILinProvider : IDisposable
    {
        void OpenConnection();

        void CloseConnection();

        void Send(Message message);

        void GetPIDFor(ref byte frameId);

        event EventHandler<byte[]> OnRead;

        event EventHandler<string> OnError;
    }
}
