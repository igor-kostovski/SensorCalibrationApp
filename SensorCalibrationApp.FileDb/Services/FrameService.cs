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
        private readonly DeviceService _deviceService;

        public FrameService(FileDatabase db, DeviceService deviceService)
        {
            _db = db;
            _deviceService = deviceService;
        }

        public async Task Update(FrameModel model)
        {
            await _db.Load();

            var entity = _db.Collection
                .Frames()
                .SingleOrDefault(x => x.Id == model.Id);

            entity?.Update(model);

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

            var device = _deviceService.GetDevice(model.DeviceId);
            device?.Frames.Add(model);

            await _db.Save();
            return model;
        }

        public async Task Delete(int id)
        {
            await _db.Load();

            var device = _deviceService.GetDeviceFromIdOfFrame(id);
            device?.Frames.Remove(x => x.Id == id);

            await _db.Save();
        }
    }
}
