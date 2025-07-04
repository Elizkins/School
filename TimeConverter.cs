﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace School
{
    public class TimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                if ((value as string).Split(' ')[0] == "0")
                {
                    return new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                }
                return new SolidColorBrush(Color.FromArgb(255, 4, 160, 255));
            }
            return new SolidColorBrush(Color.FromArgb(255, 4, 160, 255));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
