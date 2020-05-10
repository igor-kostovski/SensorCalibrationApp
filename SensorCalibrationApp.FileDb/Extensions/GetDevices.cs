using System.Collections.Generic;
using System.Linq;
using SensorCalibrationApp.Domain.Models;

namespace SensorCalibrationApp.FileDb.Extensions
{
    public static class GetDevices
    {
        public static IEnumerable<DeviceModel> Devices(this List<EcuModel> collection)
        {
            return collection
                .SelectMany(x => x.Devices)
                .GroupBy(x => x.Id)
                .Select(x => x.First())
                .OrderBy(x => x.Id);
        }
    }
}
