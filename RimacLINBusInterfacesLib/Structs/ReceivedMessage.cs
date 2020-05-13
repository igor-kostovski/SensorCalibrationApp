using System;
using System.Runtime.InteropServices;
using RimacLINBusInterfacesLib.Enums;
using HardwareHandle = System.UInt16;

namespace RimacLINBusInterfacesLib.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ReceivedMessage
    {
        [MarshalAs(UnmanagedType.U1)]
        public MessageType Type;

        public byte FrameId;

        public byte Length;

        [MarshalAs(UnmanagedType.U1)]
        public Direction Direction;

        [MarshalAs(UnmanagedType.U1)]
        public ChecksumType ChecksumType;
        
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] Data;

        public byte Checksum;

        [MarshalAs(UnmanagedType.I4)]
        public MessageErrors ErrorFlags;

        public UInt64 TimeStamp;

        public HardwareHandle hHw;
    }
}
