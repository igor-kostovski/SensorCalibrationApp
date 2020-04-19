using System.Runtime.InteropServices;
using RimacLINBusInterfacesLib.Enums;

namespace RimacLINBusInterfacesLib.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Message
    {
        public byte FrameId;

        public byte Length;

        [MarshalAs(UnmanagedType.U1)]
        public Direction Direction;

        [MarshalAs(UnmanagedType.U1)]
        public ChecksumType ChecksumType;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] Data;

        public byte Checksum;

        public Message(byte frameId, Direction direction, ChecksumType checksumType, byte[] data)
        {
            FrameId = frameId;
            Direction = direction;
            ChecksumType = checksumType;
            Data = data;
            Length = 8;
            Checksum = 0;
        }
    }
}
