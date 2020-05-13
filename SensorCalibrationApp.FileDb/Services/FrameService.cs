using System.Collections.Generic;
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

            var entities = _db.Collection
                .FramesById(model.Id)
                .ToList();

            entities.ForEach(x => x.Update(model));

            await _db.Save();
        }

        public async Task<List<FrameModel>> GetAll()
        {
            await _db.Load();
            return _db.Collection
                .Frames()
                .ToList();
        }

        public async Task<FrameModel> Create(FrameModel model)
        {
            await _db.Load();

            model.Id = _db.Collection.Frames().Max(x => x.Id) + 1;

            var devices = _db.Collection
                .DevicesById(model.DeviceId)
                .ToList();
            devices.ForEach(x => x.Frames.Add(model));

            await _db.Save();
            return model;
        }

        public async Task Delete(int id)
        {
            await _db.Load();

            var devices = _db.Collection
                .DevicesFromIdOfFrame(id)
                .ToList();
            devices.ForEach(device => device.Frames.Remove(x => x.Id == id));

            await _db.Save();
        }
    }
}
