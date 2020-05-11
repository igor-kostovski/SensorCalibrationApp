using AutoMapper;
using SensorCalibrationApp.Domain.Models;
using SensorCalibrationApp.EntityFramework.Data.Entities;

namespace SensorCalibrationApp.EntityFramework.Automapper
{
    public class FrameProfile : Profile
    {
        public FrameProfile()
        {
            CreateMap<Frame, FrameModel>()
                .ReverseMap()
                .ForMember(dest => dest.Device, opt => opt.Ignore());
        }
    }
}
