using System.Linq;
using System.Threading.Tasks;
using SensorCalibrationApp.Domain.Models;
using SensorCalibrationApp.Domain.Services;

namespace SensorCalibrationApp.FileDb.Services
{
    public class FrameService : IFrameService
    {
        private readonly FileDatabase _db;

        public FrameService(FileDatabase db)
        {
            _db = db;
        }

        public async Task Update(byte newId, FrameModel model)
        {
            await _db.Load();

            var entity = _db.Collection.SelectMany(x => x.Devices)
                .SelectMany(x => x.Frames)
                .SingleOrDefault(x => x.Id == model.Id);
            if (entity == null)
                return;

            entity.FrameId = newId;

            await _db.Save();
        }
    }
}
