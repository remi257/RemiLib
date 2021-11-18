﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace RemiLib.Wpf.Converters
{
    /// https://stackoverflow.com/a/8392590
    /// <summary>Represents a chain of <see cref="IValueConverter"/>s to be executed in succession.</summary>
    [ContentProperty("Converters")]
    [ContentWrapper(typeof(ValueConverterCollection))]
    public class ConverterChain : IValueConverter
    {
        private readonly ValueConverterCollection _converters = new ValueConverterCollection();

        /// <summary>Gets the converters to execute.</summary>
        public ValueConverterCollection Converters
        {
            get { return _converters; }
        }

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Converters
                .Aggregate(value, (current, converter) => converter.Convert(current, targetType, parameter, culture));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    /// <summary>Represents a collection of <see cref="IValueConverter"/>s.</summary>
    public sealed class ValueConverterCollection : Collection<IValueConverter>
    {

    }
}
