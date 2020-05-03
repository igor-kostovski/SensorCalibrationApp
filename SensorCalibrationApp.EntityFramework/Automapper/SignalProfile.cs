using AutoMapper;
using SensorCalibrationApp.Domain.Models;
using SensorCalibrationApp.EntityFramework.Data.Entities;

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
