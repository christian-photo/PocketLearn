using System;
using System.Globalization;
using System.Windows.Data;

namespace PocketLearn.Win.MVVM.Model.ValueConverter
{
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return $"{parameter}: {((DateTime)value).ToString("dd.MM.yy")}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
