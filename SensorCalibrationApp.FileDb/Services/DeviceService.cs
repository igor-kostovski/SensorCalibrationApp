using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SensorCalibrationApp.Domain.Dtos;
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

            var devices = _db.Collection
                .DevicesById(model.Id)
                .ToList();

            devices.ForEach(x =>
            {
                x.IncludeSaveConfig = model.IncludeSaveConfig;
                x.Frames.ForEach(frame => frame.Device = DeviceDto.MapFrom(model));
            });
            await _db.Save();
        }
    }
}
