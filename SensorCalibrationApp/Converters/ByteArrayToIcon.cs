using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using MaterialDesignThemes.Wpf;
using SensorCalibrationApp.Common.Enums;
using Label = System.Windows.Controls.Label;

namespace SensorCalibrationApp.Converters
{
    public class ByteArrayToIcon : IValueConverter
    {
        private readonly List<byte[]> _validAssignIdResponses; //without NAD
        private readonly List<byte[]> _nonValidReadByIdResponses; //without NAD

        public ByteArrayToIcon()
        {
            _validAssignIdResponses = new List<byte[]>
            {
                new byte[]{ 0x01, 0xF1, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, //Lin 2.0 version
                new byte[] { 0x01, 0xF6, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF } //Lin 2.1 version
            };

            _nonValidReadByIdResponses = new List<byte[]>
            {
                new byte[]{ 0x03, 0x7F, 0xB2, 0x12, 0xFF, 0xFF, 0xFF } //Both versions have the same res
            };
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            CommandType? type = GetCommandType(parameter);

            if (type == null)
                return null;

            var responseWithoutNAD = new byte[7];

            if (value is byte[] response)
                Array.Copy(response, 1, responseWithoutNAD, 0, responseWithoutNAD.Length);
            else
                return null;
            
            if (IsValid(type, responseWithoutNAD))
                return Application.Current.FindResource("Success") as PackIcon;

            return Application.Current.FindResource("Error") as PackIcon;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static CommandType? GetCommandType(object parameter)
        {
            var commandTypeLabel = parameter as Label;
            return (CommandType?)commandTypeLabel?.Content;
        }

        private bool IsValid(CommandType? type, byte[] responseWithoutNad)
        {
            if (type == CommandType.ReadById)
                return !_nonValidReadByIdResponses.Any(responseWithoutNad.SequenceEqual);

            return _validAssignIdResponses.Any(responseWithoutNad.SequenceEqual);
        }
    }
}
