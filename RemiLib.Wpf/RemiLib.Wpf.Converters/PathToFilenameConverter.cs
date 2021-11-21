using System;
using System.Globalization;
using System.Windows.Data;

namespace RemiLib.Wpf.Converters
{
    public class PathToFilenameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is string s && !string.IsNullOrWhiteSpace(s))
            {
                return s.Split(@"\")[^1];
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
