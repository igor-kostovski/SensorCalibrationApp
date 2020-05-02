using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SensorCalibrationApp.Domain.Models;
using SensorCalibrationApp.Domain.Services;
using SensorCalibrationApp.EntityFramework.Data;

namespace SensorCalibrationApp.EntityFramework.Services
{
    public class EcuService : IEcuService
    {
        private readonly DataContext _db;
        private readonly IMapper _mapper;

        public EcuService(DataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<EcuModel>> GetAll()
        {
            var entities = await _db.Ecus
                .Include(x => x.Devices)
                .Include(x => x.Devices.Select(y => y.Frames))
                .Include(x => x.Devices.SelectMany(y => y.Frames).Select(z => z.Signals))
                .ToListAsync();

            return _mapper.Map<List<EcuModel>>(entities);
        }
    }
}
