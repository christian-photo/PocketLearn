using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace PocketLearn.Views.Converter
{
    public class DataTemplateToViewConverter : IValueConverter
    {
        private static IEnumerable<ContentView> views = new List<ContentView>();
        public DataTemplateToViewConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value.ToString())
            {
                case "Home": return new ProjectListView();
                case "AnswerVM": return new AnswerView();
                default:
                    break;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
