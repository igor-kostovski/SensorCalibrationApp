using System;

namespace RimacLINBusInterfacesLib
{
    class CommunicationError : Exception
    {
        public CommunicationError() { }

        public CommunicationError(string info)
            : base(info)
        { }
    }
}
