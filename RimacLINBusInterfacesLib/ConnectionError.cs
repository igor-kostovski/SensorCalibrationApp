using System;

namespace RimacLINBusInterfacesLib
{
    class ConnectionError : Exception
    {
        public ConnectionError()
        { }

        public ConnectionError(string info) 
            : base(info)
        { }
    }
}
