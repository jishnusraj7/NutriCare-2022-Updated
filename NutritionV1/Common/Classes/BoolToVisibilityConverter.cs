using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;

namespace NutritionV1
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,object parameter, CultureInfo culture)
        {
            bool param = bool.Parse(parameter as string);
            bool val = (bool)value;

            return val == param ?
              Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType,object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
