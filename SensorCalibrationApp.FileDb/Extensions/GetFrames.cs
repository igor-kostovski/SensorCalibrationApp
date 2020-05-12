using System;
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

        public static void Remove(this List<FrameModel> collection, Func<FrameModel, bool> query)
        {
            var frames = collection
                .Where(query)
                .ToList();

            frames.ForEach(x => collection.Remove(x));
        }

        public static IEnumerable<FrameModel> FramesById(this List<EcuModel> collection, int id)
        {
            return collection
                .SelectMany(x => x.Devices)
                .SelectMany(x => x.Frames)
                .Where(x => x.Id == id);
        }
    }
}
