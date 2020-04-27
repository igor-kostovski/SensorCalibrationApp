using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SensorCalibrationApp.Common.Enums;
using SensorCalibrationApp.Common.Extensions;
using SensorCalibrationApp.Common.Structs;
using SensorCalibrationApp.Domain.Interfaces;
using SensorCalibrationApp.Domain.Models;

namespace SensorCalibrationApp.Domain.Devices
{
    public class PTSensor : IDevice
    {
        public bool HasError { get; set; }
        public string Message { get; set; }

        private List<PTSensorValue> Data { get; }

        private Func<int, string> GetPress = x => $"{x * 0.0088} bar";
        private Func<int, string> GetTemp = x => $"{x * 0.1 - 40} °C";

        public PTSensor()
        {
            Data = new List<PTSensorValue>();
            Message = "";
            HasError = false;
        }

        public void ProcessData(byte[] data)
        {
            const int sizeOfEachValue = 12;
            data = new[] { data[0], data[1], data[2], data[3], data[4] };

            IEnumerable bits = new BitArray(data);
            int numberOfValues = bits.OfType<bool>().Count() / sizeOfEachValue + 1;

            ParseBits(numberOfValues, bits);
            Validate();
        }

        private void Validate()
        {
            var error = Data.SingleOrDefault(x => x.Type == PTSensorData.Error);
            var binary = error.Binary.ToList();

            if (!IsValid(binary))
                return;

            Data.Remove(error);
            ProcessValues();
        }

        private bool IsValid(List<int> binary)
        {
            if (binary.Contains(1))
            {
                Message = (
                    (PTSensorError)binary.FindIndex(x => x == 1)
                ).ToString();
                HasError = true;

                return false;
            }

            return true;
        }

        private void ProcessValues()
        {
            var messages = new List<string>();

            Data.ForEach(x =>
            {
                var Get = x.Type == PTSensorData.Pressure ? GetPress : GetTemp;

                var binaryStr = String.Concat(x.Binary);
                var value = Convert.ToInt32(binaryStr, 2);

                messages.Add($"{x.Type.ToString().ToSentence()}: {Get(value)}");
            });

            Message = string.Join("\n", messages);
        }

        private void ParseBits(int numberOfValues, IEnumerable bits)
        {
            for (int i = 0; i < numberOfValues; i++)
            {
                var binaryVal = bits.OfType<bool>()
                    .Skip(i * 12)
                    .Take(12)
                    .Reverse()
                    .Select(x => x ? 1 : 0);

                Data.Add(new PTSensorValue
                {
                    Binary = binaryVal,
                    Type = (PTSensorData) i
                });
            }
        }

        public void ClearData()
        {
            Message = "";
            HasError = false;
            Data.Clear();
        }

        public Message CreateMessageFor(FrameModel frame)
        {
            switch (frame.Name)
            {
                case "DTSs_01":
                    return new Message(frame.FrameId, Direction.Subscriber, ChecksumType.Enhanced, null, 5);
                default:
                    throw new NotImplementedException();
            }
        }

        #region Members
        private class PTSensorValue
        {
            public PTSensorData Type { get; set; }
            public IEnumerable<int> Binary { get; set; }
        }

        private enum PTSensorData
        {
            Pressure,
            ExternalTemperature,
            InternalTemperature,
            Error
        }

        private enum PTSensorError
        {
            GlobalFault,
            ChecksumError,
            ResponseError,
            TNCTOC
        }
        #endregion
    }
}
