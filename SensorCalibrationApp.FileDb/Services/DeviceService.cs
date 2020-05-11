using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SensorCalibrationApp.Domain.Models;
using SensorCalibrationApp.Domain.Services;
using SensorCalibrationApp.FileDb.Extensions;

namespace SensorCalibrationApp.FileDb.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly FileDatabase _db;

        public DeviceService(FileDatabase db)
        {
            _db = db;
        }

        public async Task<List<DeviceModel>> GetAll()
        {
            await _db.Load();
            return _db.Collection
                .Devices()
                .ToList();
        }

        public async Task Update(DeviceModel model)
        {
            await _db.Load();

            var device = _db.Collection
                .Devices()
                .SingleOrDefault(x => x.Id == model.Id);

            if(device != null)
                device.IncludeSaveConfig = model.IncludeSaveConfig;

            await _db.Save();
        }

        public DeviceModel GetDeviceFromIdOfFrame(int id)
        {
            return _db.Collection
                .Devices()
                .SingleOrDefault(x => x.Frames.Any(frame => frame.Id == id));
        }

        public DeviceModel GetDevice(int id)
        {
            return _db.Collection
                .Devices()
                .SingleOrDefault(x => x.Id == id);
        }
    }
}
