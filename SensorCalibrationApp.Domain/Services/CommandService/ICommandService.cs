using System.Threading.Tasks;
using SensorCalibrationApp.Domain.Interfaces;
using SensorCalibrationApp.Domain.Models;

namespace SensorCalibrationApp.Domain.Services.CommandService
{
    public interface ICommandService
    {
        Task ReadById();
        Task UpdateFrameId(byte newFrameId);
        Task SendDeviceSpecificFrame(FrameModel frame);
        void Load(IDevice device);
    }
}
