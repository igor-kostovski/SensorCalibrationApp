﻿using AutoMapper;
using SensorCalibrationApp.Domain.Models;
using SensorCalibrationApp.EntityFramework.Data.Entities;

namespace SensorCalibrationApp.EntityFramework.Automapper
{
    public class EcuProfile : Profile
    {
        public EcuProfile()
        {
            CreateMap<Ecu, EcuModel>()
                .ReverseMap();
        }
    }
}
