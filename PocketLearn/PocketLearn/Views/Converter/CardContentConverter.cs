using Android.Graphics;
using PocketLearn.Core;
using PocketLearn.Shared.Core;
using PocketLearn.Shared.Core.Learning;
using System;
using System.Globalization;
using System.IO;
using System.Text;
using Xamarin.Forms;
using Brush = Xamarin.Forms.Brush;

namespace PocketLearn.Views.Converter
{
    public class CardContentConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int font = 14;
            int width = 400;
            int height = 200;
            if (parameter != null)
            {
                string[] split = parameter.ToString().Split('-'); // 20&400&200 (font, width, height)
                font = int.Parse(split[0]);
                width = int.Parse(split[1]);
                height = int.Parse(split[2]);
            }
            string directory = App.PlatformMediator.ApplicationConstants.GetDataPath();
            CardContent content = (CardContent)value;
            StackLayout container = new()
            {
                Orientation = StackOrientation.Vertical,
            };
            foreach (CardContentItem item in content.Items)
            {
                if (item.Type == CardContentItemType.Image)
                {
                    if (!File.Exists(System.IO.Path.Combine(directory, item.Content)))
                    {
                        continue;
                    }
                    Image image = new()
                    {
                        Source = System.IO.Path.Combine(directory, item.Content),
                        Margin = new Thickness(2),
                        HorizontalOptions = LayoutOptions.Center
                    };
                    container.Children.Add(image);
                }
                else if (item.Type == CardContentItemType.Text)
                {
                    Label textBlock = new()
                    {
                        Text = item.Content,
                        FontSize = font,
                        Margin = new Thickness(2),
                        TextColor = Utility.GetColorFromHex("#FFF").Color,
                        HorizontalOptions = LayoutOptions.Center
                    };
                    container.Children.Add(textBlock);
                }
            }
            return container;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
