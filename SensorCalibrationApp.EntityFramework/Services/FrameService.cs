using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using SensorCalibrationApp.Domain.Models;
using SensorCalibrationApp.Domain.Services;
using SensorCalibrationApp.EntityFramework.Data;
using SensorCalibrationApp.EntityFramework.Data.Entities;

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

            if (entity != null)
                _mapper.Map(model, entity);

            await _db.SaveChangesAsync();
        }

        public async Task<List<FrameModel>> GetAll()
        {
            return await _db.Frames
                .Include(x => x.Device)
                .ProjectTo<FrameModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<FrameModel> Create(FrameModel model)
        {
            var entity = new Frame();

            _mapper.Map(model, entity);
            _db.Frames.Add(entity);

            await _db.SaveChangesAsync();

            model.Id = entity.Id;
            return model;
        }

        public async Task Delete(int id)
        {
            var entity = await _db.Frames.SingleOrDefaultAsync(x => x.Id == id);

            if(entity != null)
                _db.Frames.Remove(entity);

            await _db.SaveChangesAsync();
        }
    }
}
