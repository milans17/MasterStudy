using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPFProfessor.Converters
{
    public class IntToEmptyStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                return (int)value > 0 ? value.ToString() : "";
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                int intValue;
                if (!int.TryParse(value.ToString(), out intValue))
                    return 0;

                return intValue;
            }

            return 0;
        }
    }
}
