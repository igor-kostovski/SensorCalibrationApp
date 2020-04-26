using System.Collections.Generic;
using System.Linq;
using SensorCalibrationApp.Domain.Models;

namespace SensorCalibrationApp.FileDb.Extensions
{
    public static class GetFrames
    {
        public static IEnumerable<FrameModel> Frames(this List<EcuModel> collection)
        {
            return collection
                .SelectMany(x => x.Devices)
                .SelectMany(x => x.Frames)
                .GroupBy(x => x.Id)
                .Select(x => x.First())
                .OrderBy(x => x.Id);
        }
    }
}
