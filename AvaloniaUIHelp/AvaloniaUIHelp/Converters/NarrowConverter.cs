using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Avalonia.Data.Converters;

namespace AvaloniaUIHelp.Converters
{
    public class NarrowConverter : IValueConverter
    {
        private double m_MinWidth;

        private double m_MinHeight;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double douValue = System.Convert.ToDouble(value);
            if (douValue<40)
            {
                return douValue*2;
            }
            else if (douValue > 400)
            {
                return douValue / 2;
            }
            else if(douValue>800)
            {
                return douValue / 3;
            }
            else if (douValue > 1200)
            {
                return douValue / 4;
            }
            return douValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
