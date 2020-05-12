using System;
using System.Threading.Tasks;
using SensorCalibrationApp.Domain.Models;

namespace SensorCalibrationApp.Domain.Services.CommandService
{
    public interface ICommandService : IDisposable
    {
        Task ReadById(FrameModel frame);
        Task UpdateFrameId(FrameModel frame);
        Task SendDeviceSpecificFrame(FrameModel frame);
        void OpenConnection();
    }
}
