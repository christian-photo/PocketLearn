using PocketLearn.Shared.Core.Learning;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace PocketLearn.Views.Converter
{
    public class LearnSubjectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string subject = Enum.GetName(typeof(LearnSubject), value);
            return $"{parameter}: {subject}"; //TODO implementation
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
