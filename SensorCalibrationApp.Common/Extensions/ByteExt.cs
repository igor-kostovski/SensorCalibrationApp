namespace SensorCalibrationApp.Common.Extensions
{
    public static class ByteExt
    {
        public const byte Min = 0x20;
        public const byte Max = 0x3B;

        public static bool IsInRange(this byte id)
        {
            return id >= Min && id <= Max;
        }
    }
}
