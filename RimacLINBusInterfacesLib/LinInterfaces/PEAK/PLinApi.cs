using System.Runtime.InteropServices;
using RimacLINBusInterfacesLib.Enums;
using RimacLINBusInterfacesLib.Structs;
using ClientHandle = System.Byte;
using HardwareHandle = System.UInt16;

namespace RimacLINBusInterfacesLib.LinInterfaces.PEAK
{
    internal static class PLinApi
    {
        const string PLinDLLPath = @"Libs\PLinApi.dll";

        [DllImport(PLinDLLPath, EntryPoint = "LIN_RegisterClient")]
        public static extern LinError RegisterClient(
            string strName,
            uint hWnd,
            out ClientHandle hClient);

        [DllImport(PLinDLLPath, EntryPoint = "LIN_RemoveClient")]
        public static extern LinError RemoveClient(
            ClientHandle hClient);

        [DllImport(PLinDLLPath, EntryPoint = "LIN_ConnectClient")]
        public static extern LinError ConnectClient(
            ClientHandle hClient,
            HardwareHandle hHw);

        [DllImport(PLinDLLPath, EntryPoint = "LIN_Read")]
        public static extern LinError Read(
            ClientHandle hClient,
            out ReceivedMessage pMsg);

        [DllImport(PLinDLLPath, EntryPoint = "LIN_Write")]
        public static extern LinError Write(
            ClientHandle hClient,
            HardwareHandle hHw,
            ref Message pMsg);

        [DllImport(PLinDLLPath, EntryPoint = "LIN_InitializeHardware")]
        public static extern LinError InitializeHardware(
            ClientHandle hClient,
            HardwareHandle hHw,
            [MarshalAs(UnmanagedType.U1)]
            HardwareMode byMode,
            ushort wBaudrate);

        [DllImport(PLinDLLPath, EntryPoint = "LIN_RegisterFrameId")]
        public static extern LinError RegisterFrameId(
            ClientHandle hClient,
            HardwareHandle hHw,
            byte bFromFrameId,
            byte bToFrameId);

        [DllImport(PLinDLLPath, EntryPoint = "LIN_CalculateChecksum")]
        public static extern LinError CalculateChecksum(
            ref Message pMsg);

        [DllImport(PLinDLLPath, EntryPoint = "LIN_GetAvailableHardware")]
        public static extern LinError GetAvailableHardware(
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)]
            HardwareHandle[] pBuff,
            ushort wBuffSize,
            out ushort pCount);

        [DllImport(PLinDLLPath, EntryPoint = "LIN_GetPID")]
        public static extern LinError GetPID(
            ref byte pFrameId);

        [DllImport(PLinDLLPath, EntryPoint = "LIN_StartKeepAlive")]
        public static extern LinError StartKeepAlive(
            ClientHandle hClient,
            HardwareHandle hHw,
            byte bFrameId,
            ushort wPeriod);
    }
}
