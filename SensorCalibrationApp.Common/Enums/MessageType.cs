namespace SensorCalibrationApp.Common.Enums
{
    public enum MessageType : byte
    {
        Standard = 0,
        BusSleep = 1,
        BusWakeUp = 2,
        AutobaudrateTimeOut = 3,
        AutobaudrateReply = 4,
        Overrun = 5,
        QueueOverrun = 6,
    }
}
