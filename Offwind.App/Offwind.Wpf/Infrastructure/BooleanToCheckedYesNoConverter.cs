using System;
using System.Globalization;
using System.Windows.Data;

namespace Offwind.Infrastructure
{
    public class BooleanToCheckedYesNoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var modelValue = (bool)value;
            var txtParameter = parameter as string;
            if (modelValue && txtParameter == "Yes") return true;
            if (!modelValue && txtParameter == "No") return true;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var txtParameter = parameter as string;
            if (txtParameter == "Yes") return true;
            return false;
        }
    }
}
