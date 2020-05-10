using System.Collections.Generic;
using System.Threading.Tasks;
using SensorCalibrationApp.Common.Enums;
using SensorCalibrationApp.Domain.Dtos;
using SensorCalibrationApp.Domain.Enums;
using SensorCalibrationApp.Domain.Models;
using SensorCalibrationApp.Domain.Services;

namespace SensorCalibrationApp.FileDb
{
    public partial class FileDatabase
    {
        public class Seeder : ISeeder
        {
            public static List<EcuModel> Data
            {
                get
                {
                    return new List<EcuModel>
                    {
                        new EcuModel
                        {
                            Id = 1,
                            Name = "Front cooling ECU",
                            Devices = new List<DeviceModel>
                            {
                                new DeviceModel
                                {
                                    Id = 1,
                                    Name = "Compressor",
                                    Type = DeviceType.Other
                                },
                                new DeviceModel
                                {
                                    Id = 2,
                                    Name = "Webasto heater",
                                    Type = DeviceType.Other
                                },
                                new DeviceModel
                                {
                                    Id = 3,
                                    Name = "PT sensors evaporator outlet",
                                    Type = DeviceType.PTSensor,
                                    IncludeSaveConfig = true,
                                    Frames = new List<FrameModel>
                                    {
                                        new FrameModel
                                        {
                                            Id = 1,
                                            Name = "DTSs_01",
                                            FrameId = 0x27,
                                            Direction = Direction.Subscriber,
                                            DeviceId = 3,
                                            Device = new DeviceDto
                                            {
                                                Id = 3,
                                                Name = "PT sensors evaporator outlet",
                                                Type = DeviceType.PTSensor,
                                                IncludeSaveConfig = true
                                            },
                                            Signals = new List<SignalModel>
                                            {
                                                new SignalModel
                                                {
                                                    Id = 1,
                                                    Name = "Pressure"
                                                },
                                                new SignalModel
                                                {
                                                    Id = 2,
                                                    Name = "Internal temperature"
                                                },
                                                new SignalModel
                                                {
                                                    Id = 3,
                                                    Name = "External temperature"
                                                }
                                            }
                                        }
                                    }
                                },
                                new DeviceModel
                                {
                                    Id = 4,
                                    Name = "PT sensors compressor outlet",
                                    Type = DeviceType.PTSensor,
                                    IncludeSaveConfig = true,
                                    Frames = new List<FrameModel>
                                    {
                                        new FrameModel
                                        {
                                            Id = 2,
                                            Name = "DTSs_01",
                                            FrameId = 0x27,
                                            Direction = Direction.Subscriber,
                                            DeviceId = 4,
                                            Device = new DeviceDto
                                            {
                                                Id = 4,
                                                Name = "PT sensors compressor outlet",
                                                Type = DeviceType.PTSensor,
                                                IncludeSaveConfig = true
                                            },
                                            Signals = new List<SignalModel>
                                            {
                                                new SignalModel
                                                {
                                                    Id = 1,
                                                    Name = "Pressure"
                                                },
                                                new SignalModel
                                                {
                                                    Id = 2,
                                                    Name = "Internal temperature"
                                                },
                                                new SignalModel
                                                {
                                                    Id = 3,
                                                    Name = "External temperature"
                                                }
                                            }
                                        }
                                    }
                                },
                                new DeviceModel
                                {
                                    Id = 5,
                                    Name = "PT sensors compressor inlet",
                                    Type = DeviceType.PTSensor,
                                    IncludeSaveConfig = true,
                                    Frames = new List<FrameModel>
                                    {
                                        new FrameModel
                                        {
                                            Id = 3,
                                            Name = "DTSs_01",
                                            FrameId = 0x27,
                                            Direction = Direction.Subscriber,
                                            DeviceId = 5,
                                            Device = new DeviceDto
                                            {
                                                Id = 5,
                                                Name = "PT sensors compressor inlet",
                                                Type = DeviceType.PTSensor,
                                                IncludeSaveConfig = true
                                            },
                                            Signals = new List<SignalModel>
                                            {
                                                new SignalModel
                                                {
                                                    Id = 1,
                                                    Name = "Pressure"
                                                },
                                                new SignalModel
                                                {
                                                    Id = 2,
                                                    Name = "Internal temperature"
                                                },
                                                new SignalModel
                                                {
                                                    Id = 3,
                                                    Name = "External temperature"
                                                }
                                            }
                                        }
                                    }
                                },
                                new DeviceModel
                                {
                                    Id = 6,
                                    Name = "EXV valve",
                                    Type = DeviceType.Other
                                },
                                new DeviceModel
                                {
                                    Id = 7,
                                    Name = "Recirculation air quality sensor",
                                    Type = DeviceType.Other
                                },
                                new DeviceModel
                                {
                                    Id = 8,
                                    Name = "Fresh air quality sensor",
                                    Type = DeviceType.Other
                                },
                                new DeviceModel
                                {
                                    Id = 9,
                                    Name = "Solar sensor",
                                    Type = DeviceType.Other
                                }
                            }
                        },
                        new EcuModel
                        {
                            Id = 2,
                            Name = "Rear cooling ECU",
                            Devices = new List<DeviceModel>
                            {
                                new DeviceModel
                                {
                                    Id = 1,
                                    Name = "Compressor",
                                    Type = DeviceType.Other
                                },
                                new DeviceModel
                                {
                                    Id = 2,
                                    Name = "Webasto heater",
                                    Type = DeviceType.Other
                                },
                                new DeviceModel
                                {
                                    Id = 3,
                                    Name = "PT sensors evaporator outlet",
                                    Type = DeviceType.PTSensor,
                                    IncludeSaveConfig = true,
                                    Frames = new List<FrameModel>
                                    {
                                        new FrameModel
                                        {
                                            Id = 1,
                                            Name = "DTSs_01",
                                            FrameId = 0x27,
                                            Direction = Direction.Subscriber,
                                            DeviceId = 3,
                                            Device = new DeviceDto
                                            {
                                                Id = 3,
                                                Name = "PT sensors evaporator outlet",
                                                Type = DeviceType.PTSensor,
                                                IncludeSaveConfig = true
                                            },
                                            Signals = new List<SignalModel>
                                            {
                                                new SignalModel
                                                {
                                                    Id = 1,
                                                    Name = "Pressure"
                                                },
                                                new SignalModel
                                                {
                                                    Id = 2,
                                                    Name = "Internal temperature"
                                                },
                                                new SignalModel
                                                {
                                                    Id = 3,
                                                    Name = "External temperature"
                                                }
                                            }
                                        }
                                    }
                                },
                                new DeviceModel
                                {
                                    Id = 4,
                                    Name = "PT sensors compressor outlet",
                                    Type = DeviceType.PTSensor,
                                    IncludeSaveConfig = true,
                                    Frames = new List<FrameModel>
                                    {
                                        new FrameModel
                                        {
                                            Id = 2,
                                            Name = "DTSs_01",
                                            FrameId = 0x27,
                                            Direction = Direction.Subscriber,
                                            DeviceId = 4,
                                            Device = new DeviceDto
                                            {
                                                Id = 4,
                                                Name = "PT sensors compressor outlet",
                                                Type = DeviceType.PTSensor,
                                                IncludeSaveConfig = true
                                            },
                                            Signals = new List<SignalModel>
                                            {
                                                new SignalModel
                                                {
                                                    Id = 1,
                                                    Name = "Pressure"
                                                },
                                                new SignalModel
                                                {
                                                    Id = 2,
                                                    Name = "Internal temperature"
                                                },
                                                new SignalModel
                                                {
                                                    Id = 3,
                                                    Name = "External temperature"
                                                }
                                            }
                                        }
                                    }
                                },
                                new DeviceModel
                                {
                                    Id = 5,
                                    Name = "PT sensors compressor inlet",
                                    Type = DeviceType.PTSensor,
                                    IncludeSaveConfig = true,
                                    Frames = new List<FrameModel>
                                    {
                                        new FrameModel
                                        {
                                            Id = 3,
                                            Name = "DTSs_01",
                                            FrameId = 0x27,
                                            Direction = Direction.Subscriber,
                                            DeviceId = 5,
                                            Device = new DeviceDto
                                            {
                                                Id = 5,
                                                Name = "PT sensors compressor inlet",
                                                Type = DeviceType.PTSensor,
                                                IncludeSaveConfig = true
                                            },
                                            Signals = new List<SignalModel>
                                            {
                                                new SignalModel
                                                {
                                                    Id = 1,
                                                    Name = "Pressure"
                                                },
                                                new SignalModel
                                                {
                                                    Id = 2,
                                                    Name = "Internal temperature"
                                                },
                                                new SignalModel
                                                {
                                                    Id = 3,
                                                    Name = "External temperature"
                                                }
                                            }
                                        }
                                    }
                                },
                                new DeviceModel
                                {
                                    Id = 10,
                                    Name = "Gearbox oil pump",
                                    Type = DeviceType.Other
                                }
                            }
                        },
                        new EcuModel
                        {
                            Id = 3,
                            Name = "PCU ECU",
                            Devices = new List<DeviceModel>
                            {
                                new DeviceModel
                                {
                                    Id = 11,
                                    Name = "HVAC flaps",
                                    Type = DeviceType.Other
                                },
                                new DeviceModel
                                {
                                    Id = 12,
                                    Name = "HVAC blower",
                                    Type = DeviceType.Other
                                },
                                new DeviceModel
                                {
                                    Id = 13,
                                    Name = "Temperature and humidity windscreen sensor",
                                    Type = DeviceType.Other
                                }
                            }
                        }
                    };
                }
            }

            private readonly FileDatabase _db;

            public Seeder(FileDatabase db)
            {
                _db = db;
            }

            public async Task Seed()
            {
                await _db.SeedDb(Data);
            }
        }
    }
}
