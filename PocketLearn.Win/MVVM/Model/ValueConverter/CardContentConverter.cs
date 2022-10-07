using PocketLearn.Core.Learning;
using PocketLearn.Win.Core;
using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Image = System.Windows.Controls.Image;

namespace PocketLearn.Win.MVVM.Model.ValueConverter
{
    public class CardContentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            CardContent content = (CardContent)value;
            StackPanel container = new()
            {
                Orientation = Orientation.Vertical
            };
            foreach (object item in content.Items)
            {
                if (((CardContentItem<object>)item).Type == CardContentItemType.Image)
                {
                    Image image = new()
                    {
                        Source = new Bitmap(((CardContentItem<string>)item).Content).ToBitmapImage(),
                        Margin = new System.Windows.Thickness(2),
                    };
                    container.Children.Add(image);
                }
                else if (((CardContentItem<object>)item).Type == CardContentItemType.Text)
                {
                    TextBlock textBlock = new()
                    {
                        Text = ((CardContentItem<string>)item).Content,
                        FontSize = 14,
                        Margin = new System.Windows.Thickness(2),
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
