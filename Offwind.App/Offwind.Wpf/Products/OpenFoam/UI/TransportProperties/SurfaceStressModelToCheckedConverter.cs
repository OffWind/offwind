using System;
using System.Globalization;
using System.Windows.Data;
using Offwind.Sowfa.Constant.TransportProperties;

namespace Offwind.Products.OpenFoam.UI.TransportProperties
{
    public class SurfaceStressModelToCheckedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var modelValue = (SurfaceStressModel)value;
            var txtParameter = parameter as string;
            var param = (SurfaceStressModel) Enum.Parse(typeof (SurfaceStressModel), txtParameter);
            if (modelValue == param) return true;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var txtParameter = parameter as string;
            var param = (SurfaceStressModel)Enum.Parse(typeof(SurfaceStressModel), txtParameter);
            return param;
        }
    }
}
