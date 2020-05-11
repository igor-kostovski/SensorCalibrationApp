using System.Collections.Generic;
using System.Threading.Tasks;
using SensorCalibrationApp.Domain.Models;

namespace SensorCalibrationApp.Domain.Services
{
    public interface IFrameService
    {
        Task Update(FrameModel model);
        Task<List<FrameModel>> GetAll();
        Task<FrameModel> Create(FrameModel model);
        Task Delete(int id);
    }
}
