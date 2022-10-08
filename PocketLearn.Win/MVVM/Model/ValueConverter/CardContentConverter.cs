using PocketLearn.Core.Learning;
using PocketLearn.Win.Core;
using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using Image = System.Windows.Controls.Image;

namespace PocketLearn.Win.MVVM.Model.ValueConverter
{
    public class CardContentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string directory = System.IO.Path.Combine(ApplicationConstants.APPLICATION_DATA_PATH, "Images");
            CardContent content = (CardContent)value;
            StackPanel container = new()
            {
                Orientation = Orientation.Vertical
            };
            foreach (CardContentItem item in content.Items)
            {
                if ((item).Type == CardContentItemType.Image)
                {
                    Bitmap bmp = new(System.IO.Path.Combine(directory, item.Content));
                    Image image = new()
                    {
                        Source = bmp.ToBitmapImage(),
                        Margin = new System.Windows.Thickness(2)
                    };
                    container.Children.Add(image);
                }
                else if ((item).Type == CardContentItemType.Text)
                {
                    TextBlock textBlock = new()
                    {
                        Text = item.Content,
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
