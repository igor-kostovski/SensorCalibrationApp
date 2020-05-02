using AutoMapper;
using SensorCalibrationApp.Common;
using SensorCalibrationApp.Domain.Models;

namespace SensorCalibrationApp.EntityFramework.Automapper
{
    public class SignalProfile : Profile
    {
        public SignalProfile()
        {
            CreateMap<Signal, SignalModel>()
                .ReverseMap();
        }
    }
}
