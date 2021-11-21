using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RemiLib.Wpf.Converters
{
    public class EnumExistenceToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is Enum e1 && parameter is Enum e2 && e1.GetType().Equals(e2.GetType()))
            {
                uint u1 = System.Convert.ToUInt32(e1);
                uint u2 = System.Convert.ToUInt32(e2);
                return (u1 & u2) != 0; 
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
