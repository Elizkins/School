using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace School
{
    public class CostConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if(values != null && values[0] != null && values[1] != null && values[2] != null)
            {
                if((double)values[2] != 0)
                {
                    return $" {Math.Round(Double.Parse(values[0].ToString()) - Double.Parse(values[0].ToString()) * (double)values[2] / 100)} рублей за {(int)values[1]} минут";
                }
                return $"{Math.Round((decimal)values[0])} рублей за {(int)values[1]} минут";
            }
            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
