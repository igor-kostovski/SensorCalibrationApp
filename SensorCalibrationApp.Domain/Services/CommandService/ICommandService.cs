using System.Threading.Tasks;

namespace SensorCalibrationApp.Domain.Services.CommandService
{
    public interface ICommandService
    {
        Task ReadById();
        Task UpdateFrameId(byte newFrameId);
        Task SendDeviceSpecificFrame();
    }
}
