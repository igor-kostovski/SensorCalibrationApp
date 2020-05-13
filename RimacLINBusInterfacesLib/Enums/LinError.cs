namespace RimacLINBusInterfacesLib.Enums
{
    public enum LinError
    {
        Ok = 0,
        TransmitQueueFull = 1,
        IllegalPeriod = 2,
        ReceiveQueueEmpty = 3,
        IllegalChecksumType = 4,
        IllegalHardware = 5,
        IllegalClient = 6,
        WrongParameterType = 7,
        WrongParameterValue = 8,
        IllegalDirection = 9,
        IllegalLength = 10,
        IllegalBaudrate = 11,
        IllegalFrameID = 12,
        BufferInsufficient = 13,
        IllegalScheduleNo = 14,
        IllegalSlotCount = 15,
        IllegalIndex = 16,
        IllegalRange = 17,
        OutOfResource = 1001,
        ManagerNotLoaded = 1002,
        ManagerNotResponding = 1003,
        MemoryAccess = 1004,
        NotImplemented = 0xFFFE,
        Unknown = 0xFFFF
    }
}
