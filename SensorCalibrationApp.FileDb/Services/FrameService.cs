using System.Linq;
using System.Threading.Tasks;
using SensorCalibrationApp.Domain.Models;
using SensorCalibrationApp.Domain.Services;
using SensorCalibrationApp.FileDb.Extensions;

namespace SensorCalibrationApp.FileDb.Services
{
    public class FrameService : IFrameService
    {
        private readonly FileDatabase _db;

        public FrameService(FileDatabase db)
        {
            _db = db;
        }

        public async Task Update(FrameModel model)
        {
            await _db.Load();

            var entity = _db.Collection
                .Frames()
                .SingleOrDefault(x => x.Id == model.Id);
            if (entity == null)
                return;

            entity.FrameId = model.FrameId;

            await _db.Save();
        }
    }
}
