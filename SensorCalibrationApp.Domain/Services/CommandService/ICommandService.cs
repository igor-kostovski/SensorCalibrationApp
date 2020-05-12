using System;
using System.Threading.Tasks;
using SensorCalibrationApp.Domain.Models;

namespace SensorCalibrationApp.Domain.Services.CommandService
{
    public interface ICommandService : IDisposable
    {
        Task ReadById();
        Task UpdateFrameId(FrameModel frame);
        Task SendDeviceSpecificFrame(FrameModel frame);
        void OpenConnection();
    }
}
