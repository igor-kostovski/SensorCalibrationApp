using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using SensorCalibrationApp.Common.Enums;
using SensorCalibrationApp.Domain.Enums;
using SensorCalibrationApp.Domain.Services;
using SensorCalibrationApp.EntityFramework.Data.Entities;

namespace SensorCalibrationApp.EntityFramework
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
                        Name = "Compressor"
                    },
                    new Device
                    {
                        Id = 2,
                        Name = "Webasto heater"
                    },
                    new Device
                    {
                        Id = 3,
                        Name = "PT sensors evaporator outlet",
                        Type = DeviceType.PTSensor
                    },
                    new Device
                    {
                        Id = 4,
                        Name = "PT sensors compressor outlet",
                        Type = DeviceType.PTSensor
                    },
                    new Device
                    {
                        Id = 5,
                        Name = "PT sensors compressor inlet",
                        Type = DeviceType.PTSensor
                    },
                    new Device
                    {
                        Id = 6,
                        Name = "EXV valve"
                    },
                    new Device
                    {
                        Id = 7,
                        Name = "Recirculation air quality sensor"
                    },
                    new Device
                    {
                        Id = 8,
                        Name = "Fresh air quality sensor"
                    },
                    new Device
                    {
                        Id = 9,
                        Name = "Solar sensor"
                    },
                    new Device
                    {
                        Id = 10,
                        Name = "Gearbox oil pump"
                    },
                    new Device
                    {
                        Id = 11,
                        Name = "HVAC flaps"
                    },
                    new Device
                    {
                        Id = 12,
                        Name = "HVAC blower"
                    },
                    new Device
                    {
                        Id = 13,
                        Name = "Temperature and humidity windscreen sensor"
                    }
                );

                _db.Frames.AddOrUpdate(f => f.Id,
                    new Frame
                    {
                        Id = 1,
                        Name = "DTSs_01",
                        FrameId = 0x27,
                        Direction = Direction.Subscriber
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

                var frame = _db.Frames.SingleOrDefault(x => x.Id == 1);
                if (frame != null && frame.Signals == null)
                    frame.Signals = new List<Signal>(_db.Signals.ToList());

                var deviceIds = new List<int>(new int[] {3, 4, 5});
                var devices = _db.Devices.Where(x => deviceIds.Any(y => x.Id == y)).ToList();
                devices.ForEach(x =>
                {
                    if (x.Frames == null)
                        x.Frames = new List<Frame> {frame};
                });

                var ecu = _db.Ecus.SingleOrDefault(x => x.Id == 1);
                if (ecu != null && ecu.Devices == null)
                    ecu.Devices = new List<Device>(_db.Devices.Where(x => x.Id < 10).ToList());

                ecu = _db.Ecus.SingleOrDefault(x => x.Id == 2);
                if (ecu != null && ecu.Devices == null)
                    ecu.Devices = new List<Device>(_db.Devices.Where(x => x.Id < 6 || x.Id == 10).ToList());

                ecu = _db.Ecus.SingleOrDefault(x => x.Id == 3);
                if (ecu != null && ecu.Devices == null)
                    ecu.Devices = new List<Device>(_db.Devices.Where(x => x.Id > 10).ToList());

                _db.SaveChanges();
            });
        }
    }
}
