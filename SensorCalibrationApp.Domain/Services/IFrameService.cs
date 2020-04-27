using System.Threading.Tasks;
using SensorCalibrationApp.Domain.Models;

namespace SensorCalibrationApp.Domain.Services
{
    public interface IFrameService
    {
        Task Update(FrameModel model);
    }
}
