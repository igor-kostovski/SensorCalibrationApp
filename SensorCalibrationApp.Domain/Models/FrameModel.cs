using System.Collections.Generic;
using SensorCalibrationApp.Common.Enums;
using SensorCalibrationApp.Domain.Dtos;

namespace SensorCalibrationApp.Domain.Models
{
    public class FrameModel : BaseModel
    {
        public byte FrameId { get; set; }
        public Direction Direction { get; set; }
        public byte[] Bytes { get; set; }
        public byte[] LegacyCommandBytes { get; set; }
        public ChecksumType Checksum { get; set; }
        public List<SignalModel> Signals { get; set; }
        public int DeviceId { get; set; }

        private byte _length;
        public byte Length
        {
            get
            {
                return _length;
            }
            set
            {
                _length = value;
                OnPropertyChanged();
                RunValidation?.Invoke();
            }
        }

        private DeviceDto _device;
        public DeviceDto Device
        {
            get
            {
                return _device;
            }
            set
            {
                _device = value;
                OnPropertyChanged();
            }
        }

        public void Update(FrameModel model)
        {
            FrameId = model.FrameId;
            Direction = model.Direction;
            Bytes = model.Bytes;
            Length = model.Length;
            Checksum = model.Checksum;
            DeviceId = model.DeviceId;
            Device = model.Device;
            Name = model.Name;
        }
    }
}
