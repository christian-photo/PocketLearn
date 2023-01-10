#region "copyright"

/*
    Copyright © 2023 Christian Palm (christian@palm-family.de)
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
*/

#endregion "copyright"

using PocketLearn.ViewModels;
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
            switch (((BaseViewModel)value).Title)
            {
                case "Home": return new ProjectListView();
                case "AnswerVM": return new AnswerView();
                case "QuestionVM": return new QuestionView();
               // case "ScanVM": return new ScannQRPage();
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
