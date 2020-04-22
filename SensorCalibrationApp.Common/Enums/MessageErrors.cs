namespace SensorCalibrationApp.Common.Enums
{
    public enum MessageErrors : int
    {
        Ok = 0x0,

        InconsistentSynch = 0x1,

        IdParityBit0 = 0x2,

        IdParityBit1 = 0x4,

        SlaveNotResponding = 0x8,

        Timeout = 0x10,

        Checksum = 0x20,

        GroundShort = 0x40,

        VBatShort = 0x80,

        SlotDelay = 0x100,

        OtherResponse = 0x200,
    }
}
