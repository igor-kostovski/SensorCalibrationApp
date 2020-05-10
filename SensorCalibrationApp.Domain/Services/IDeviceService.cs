using System.Collections.Generic;
using System.Threading.Tasks;
using SensorCalibrationApp.Domain.Models;

namespace SensorCalibrationApp.Domain.Services
{
    public interface IDeviceService
    {
        Task<List<DeviceModel>> GetAll();
    }
}
