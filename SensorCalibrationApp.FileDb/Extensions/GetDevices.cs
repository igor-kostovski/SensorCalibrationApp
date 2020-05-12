using System.Collections.Generic;
using System.Linq;
using SensorCalibrationApp.Domain.Models;

namespace SensorCalibrationApp.FileDb.Extensions
{
    public static class GetDevices
    {
        public static IEnumerable<DeviceModel> DevicesFromIdOfFrame(this List<EcuModel> collection, int id)
        {
            return collection
                .SelectMany(x => x.Devices)
                .Where(x => x.Frames.Any(frame => frame.Id == id));
        }

        public static IEnumerable<DeviceModel> DevicesById(this List<EcuModel> collection, int id)
        {
            return collection
                .SelectMany(x => x.Devices)
                .Where(x => x.Id == id);
        }

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
