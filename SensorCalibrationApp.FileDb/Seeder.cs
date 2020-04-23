using System.Collections.Generic;
using SensorCalibrationApp.Common.Enums;
using SensorCalibrationApp.Domain.Enums;
using SensorCalibrationApp.Domain.Models;

namespace SensorCalibrationApp.FileDb
{
    public static class Seeder
    {
        public static List<EcuModel> DataCollection
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
                                Name = "Compressor"
                            },
                            new DeviceModel
                            {
                                Id = 2,
                                Name = "Webasto heater"
                            },
                            new DeviceModel
                            {
                                Id = 3,
                                Name = "PT sensors evaporator outlet",
                                Type = DeviceType.PTSensor,
                                Frames = new List<FrameModel>
                                {
                                    new FrameModel
                                    {
                                        Id = 1,
                                        Name = "DTSs_01",
                                        Direction = Direction.Subscriber,
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
                                Frames = new List<FrameModel>
                                {
                                    new FrameModel
                                    {
                                        Id = 1,
                                        Name = "DTSs_01",
                                        Direction = Direction.Subscriber,
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
                                Frames = new List<FrameModel>
                                {
                                    new FrameModel
                                    {
                                        Id = 1,
                                        Name = "DTSs_01",
                                        Direction = Direction.Subscriber,
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
                                Name = "EXV valve"
                            },
                            new DeviceModel
                            {
                                Id = 7,
                                Name = "Recirculation air quality sensor"
                            },
                            new DeviceModel
                            {
                                Id = 8,
                                Name = "Fresh air quality sensor"
                            },
                            new DeviceModel
                            {
                                Id = 9,
                                Name = "Solar sensor"
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
                                Name = "Compressor"
                            },
                            new DeviceModel
                            {
                                Id = 2,
                                Name = "Webasto heater"
                            },
                            new DeviceModel
                            {
                                Id = 3,
                                Name = "PT sensors evaporator outlet",
                                Type = DeviceType.PTSensor,
                                Frames = new List<FrameModel>
                                {
                                    new FrameModel
                                    {
                                        Id = 1,
                                        Name = "DTSs_01",
                                        Direction = Direction.Subscriber,
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
                                Frames = new List<FrameModel>
                                {
                                    new FrameModel
                                    {
                                        Id = 1,
                                        Name = "DTSs_01",
                                        Direction = Direction.Subscriber,
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
                                Frames = new List<FrameModel>
                                {
                                    new FrameModel
                                    {
                                        Id = 1,
                                        Name = "DTSs_01",
                                        Direction = Direction.Subscriber,
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
                                Name = "Gearbox oil pump"
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
                                Name = "HVAC flaps"
                            },
                            new DeviceModel
                            {
                                Id = 12,
                                Name = "HVAC blower"
                            },
                            new DeviceModel
                            {
                                Id = 13,
                                Name = "Temperature and humidity windscreen sensor"
                            }
                        }
                    }
                };
            }
        }
    }
}
