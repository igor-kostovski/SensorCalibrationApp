using System;
using System.Globalization;
using System.Windows.Controls;

namespace SensorCalibrationApp.Validations
{
    public class FrameIdRangeRule : ValidationRule
    {
        public byte Max { get; set; }
        public byte Min { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            byte frameId = 0x00;

            try
            {
                var strVal = value as string;
                if (!string.IsNullOrEmpty(strVal))
                    frameId = byte.Parse(strVal);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, $"Illegal characters or {e.Message}");
            }

            if ((frameId < Min) || (frameId > Max))
            {
                return new ValidationResult(false,
                    $"Please enter an frameId in the range: {Min}-{Max}.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
