using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace AvaloniaUIHelp.Converters
{
    public class EnumToBooleanConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return AvaloniaProperty.UnsetValue; ;
            }

            if (parameter == null)
            {
                return AvaloniaProperty.UnsetValue;
            }

            if (!value.Equals(parameter))
            {
                return false;
            }

            return true;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value == null)
            {
                return BindingOperations.DoNothing;
            }

            if (value.Equals(false))
            {
                return BindingOperations.DoNothing;
            }

            if (parameter == null)
            {
                return BindingOperations.DoNothing;
            }
            return parameter;
        }
    }
}
