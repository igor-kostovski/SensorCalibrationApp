using AutoMapper;
using SensorCalibrationApp.Domain.Models;
using SensorCalibrationApp.EntityFramework.Data.Entities;

namespace SensorCalibrationApp.EntityFramework.Automapper
{
    public class DeviceProfile : Profile
    {
        public DeviceProfile()
        {
            CreateMap<Device, DeviceModel>()
                .ReverseMap();
        }
    }
}
