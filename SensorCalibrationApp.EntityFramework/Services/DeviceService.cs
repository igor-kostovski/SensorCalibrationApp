using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using SensorCalibrationApp.Domain.Models;
using SensorCalibrationApp.Domain.Services;
using SensorCalibrationApp.EntityFramework.Data;

namespace SensorCalibrationApp.EntityFramework.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly DataContext _db;
        private readonly IMapper _mapper;

        public DeviceService(DataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<DeviceModel>> GetAll()
        {
            return await _db.Devices
                .ProjectTo<DeviceModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task Update(DeviceModel model)
        {
            var entity = await _db.Devices
                .SingleOrDefaultAsync(x => x.Id == model.Id);

            if(entity != null)
                _mapper.Map(model, entity);

            await _db.SaveChangesAsync();
        }
    }
}
