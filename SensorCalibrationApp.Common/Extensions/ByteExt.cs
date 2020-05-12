namespace SensorCalibrationApp.Common.Extensions
{
    public static class ByteExt
    {
        public static bool IsInRange(this byte id, byte min, byte max)
        {
            return id >= min && id <= max;
        }
    }
}
