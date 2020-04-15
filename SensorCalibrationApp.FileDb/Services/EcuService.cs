using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SensorCalibrationApp.Domain.Models;
using SensorCalibrationApp.Domain.Services;

namespace SensorCalibrationApp.FileDb.Services
{
    public class EcuService : IEcuService
    {
        private readonly FileDatabase _db;

        public EcuService(FileDatabase db)
        {
            _db = db;
        }

        public async Task<List<EcuModel>> GetAll()
        {
            await _db.Load();
            return _db.Collection;
        }
    }
}
