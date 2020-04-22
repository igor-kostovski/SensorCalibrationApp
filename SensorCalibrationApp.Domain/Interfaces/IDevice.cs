namespace SensorCalibrationApp.Domain.Interfaces
{
    public interface IDevice
    {
        bool HasError { get; set; }
        string Message { get; set; }

        void ProcessData(byte[] data);
        void ClearData();
    }
}
