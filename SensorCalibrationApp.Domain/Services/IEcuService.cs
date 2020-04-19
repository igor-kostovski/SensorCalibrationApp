using System.Collections.Generic;
using System.Threading.Tasks;
using SensorCalibrationApp.Domain.Models;

namespace SensorCalibrationApp.Domain.Services
{
    public interface IEcuService
    {
        Task<List<EcuModel>> GetAll();
    }
}
