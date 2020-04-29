﻿using System.Windows;

namespace SensorCalibrationApp.Validations
{
    public class BindingProxy : Freezable
    {
        public object Data
        {
            get { return GetValue(DataProperty); }
            set
            {
                SetValue(DataProperty, value);
            }
        }

        public static readonly DependencyProperty DataProperty = DependencyProperty.Register(nameof(Data),
            typeof(object), typeof(BindingProxy), new PropertyMetadata(null));

        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }
    }
}
