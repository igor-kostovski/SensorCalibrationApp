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
    public class FrameService : IFrameService
    {
        private readonly DataContext _db;
        private readonly IMapper _mapper;

        public FrameService(DataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task Update(FrameModel model)
        {
            var entity = await _db.Frames
                .SingleOrDefaultAsync(x => x.Id == model.Id);

            if(entity != null)
                entity.FrameId = model.FrameId;

            await _db.SaveChangesAsync();
        }

        public async Task<List<FrameModel>> GetAll()
        {
            return await _db.Frames
                .Include(x => x.Device)
                .ProjectTo<FrameModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
