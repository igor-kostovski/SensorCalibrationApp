using System.Data.Entity;
using System.Threading.Tasks;
using SensorCalibrationApp.Domain.Models;
using SensorCalibrationApp.Domain.Services;
using SensorCalibrationApp.EntityFramework.Data;

namespace SensorCalibrationApp.EntityFramework.Services
{
    public class FrameService : IFrameService
    {
        private readonly DataContext _db;

        public FrameService(DataContext db)
        {
            _db = db;
        }

        public async Task Update(FrameModel model)
        {
            var entity = await _db.Frames
                .SingleOrDefaultAsync(x => x.Id == model.Id);

            if(entity != null)
                entity.FrameId = model.FrameId;

            await _db.SaveChangesAsync();
        }
    }
}
