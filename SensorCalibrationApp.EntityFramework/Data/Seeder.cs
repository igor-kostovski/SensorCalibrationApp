﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using RimacLINBusInterfacesLib.Enums;
using SensorCalibrationApp.Domain.Enums;
using SensorCalibrationApp.Domain.Services;
using SensorCalibrationApp.EntityFramework.Data.Entities;

namespace SensorCalibrationApp.EntityFramework.Data
{
    public class Seeder : ISeeder
    {
        private readonly DataContext _db;

        public Seeder(DataContext db)
        {
            _db = db;
        }

        public Task Seed()
        {
            if (_db.Ecus.FirstOrDefault() != null)
                return Task.CompletedTask;

            return Task.Run(() =>
            {
                _db.Ecus.AddOrUpdate(e => e.Id,
                    new Ecu
                    {
                        Id = 1,
                        Name = "Front cooling ECU"
                    },
                    new Ecu
                    {
                        Id = 2,
                        Name = "Rear cooling ECU"
                    },
                    new Ecu
                    {
                        Id = 3,
                        Name = "PCU ECU"
                    }
                );

                _db.Devices.AddOrUpdate(d => d.Id,
                    new Device
                    {
                        Id = 1,
                        Name = "Compressor",
                        Type = DeviceType.Other
                    },
                    new Device
                    {
                        Id = 2,
                        Name = "Webasto heater",
                        Type = DeviceType.Other
                    },
                    new Device
                    {
                        Id = 3,
                        Name = "PT sensors evaporator outlet",
                        Type = DeviceType.PTSensor,
                        IncludeSaveConfig = true,
                        Frames = new List<Frame>
                        {
                            new Frame
                            {
                                Name = "DTSs_01",
                                FrameId = 0x27,
                                Direction = Direction.Subscriber,
                                DeviceId = 3,
                                Length = 5,
                                Checksum = ChecksumType.Enhanced,
                            }
                        }
                    },
                    new Device
                    {
                        Id = 4,
                        Name = "PT sensors compressor outlet",
                        Type = DeviceType.PTSensor,
                        IncludeSaveConfig = true,
                        Frames = new List<Frame>
                        {
                            new Frame
                            {
                                Name = "DTSs_01",
                                FrameId = 0x27,
                                Direction = Direction.Subscriber,
                                DeviceId = 4,
                                Length = 5,
                                Checksum = ChecksumType.Enhanced,
                            }
                        }
                    },
                    new Device
                    {
                        Id = 5,
                        Name = "PT sensors compressor inlet",
                        Type = DeviceType.PTSensor,
                        IncludeSaveConfig = true,
                        Frames = new List<Frame>
                        {
                            new Frame
                            {
                                Name = "DTSs_01",
                                FrameId = 0x27,
                                Direction = Direction.Subscriber,
                                DeviceId = 5,
                                Length = 5,
                                Checksum = ChecksumType.Enhanced,
                            }
                        }
                    },
                    new Device
                    {
                        Id = 6,
                        Name = "EXV valve",
                        Type = DeviceType.Other
                    },
                    new Device
                    {
                        Id = 7,
                        Name = "Recirculation air quality sensor",
                        Type = DeviceType.Other
                    },
                    new Device
                    {
                        Id = 8,
                        Name = "Fresh air quality sensor",
                        Type = DeviceType.Other
                    },
                    new Device
                    {
                        Id = 9,
                        Name = "Solar sensor",
                        Type = DeviceType.Other
                    },
                    new Device
                    {
                        Id = 10,
                        Name = "Gearbox oil pump",
                        Type = DeviceType.Other
                    },
                    new Device
                    {
                        Id = 11,
                        Name = "HVAC flaps",
                        Type = DeviceType.Other
                    },
                    new Device
                    {
                        Id = 12,
                        Name = "HVAC blower",
                        Type = DeviceType.Other
                    },
                    new Device
                    {
                        Id = 13,
                        Name = "Temperature and humidity windscreen sensor",
                        Type = DeviceType.Other
                    }
                );

                _db.Signals.AddOrUpdate(s => s.Id,
                    new Signal
                    {
                        Id = 1,
                        Name = "Pressure"
                    },
                    new Signal
                    {
                        Id = 2,
                        Name = "Internal temperature"
                    },
                    new Signal
                    {
                        Id = 3,
                        Name = "External temperature"
                    }
                );

                _db.SaveChanges();

                var frame = _db.Frames
                    .Include(x => x.Signals)
                    .SingleOrDefault(x => x.Id == 1);
                if (frame != null && frame.Signals.Count == 0)
                    frame.Signals = new List<Signal>(_db.Signals.ToList());

                var ecu = _db.Ecus
                    .Include(x => x.Devices)
                    .SingleOrDefault(x => x.Id == 1);
                if (ecu != null && ecu.Devices.Count == 0)
                    ecu.Devices = new List<Device>(_db.Devices.Where(x => x.Id < 10).ToList());

                ecu = _db.Ecus
                    .Include(x => x.Devices)
                    .SingleOrDefault(x => x.Id == 2);
                if (ecu != null && ecu.Devices.Count == 0)
                    ecu.Devices = new List<Device>(_db.Devices.Where(x => x.Id < 6 || x.Id == 10).ToList());

                ecu = _db.Ecus
                    .Include(x => x.Devices)
                    .SingleOrDefault(x => x.Id == 3);
                if (ecu != null && ecu.Devices.Count == 0)
                    ecu.Devices = new List<Device>(_db.Devices.Where(x => x.Id > 10).ToList());

                _db.SaveChanges();
            });
        }
    }
}
