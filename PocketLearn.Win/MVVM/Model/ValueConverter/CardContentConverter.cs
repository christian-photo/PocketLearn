#region "copyright"

/*
    Copyright © 2023 Christian Palm (christian@palm-family.de)
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
*/

#endregion "copyright"

using PocketLearn.Shared.Core.Learning;
using PocketLearn.Win.Core;
using Serilog;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using WpfMath.Controls;
using Image = System.Windows.Controls.Image;
using Brushes = System.Windows.Media.Brushes;

namespace PocketLearn.Win.MVVM.Model.ValueConverter
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
                string[] split = parameter.ToString().Split('-'); // 20-400-200 (font, width, height)
                font = int.Parse(split[0]);
                width = int.Parse(split[1]);
                height = int.Parse(split[2]);
            }
            else
            {
                Log.Verbose("No parameters passed, using default parameters");
            }
            string directory = Path.Combine(ApplicationConstants.APPLICATION_DATA_PATH, "Images");
            CardContent content = (CardContent)value;
            StackPanel container = new()
            {
                Orientation = Orientation.Vertical
            };
            foreach (CardContentItem item in content.Items)
            {
                if (item.Type == CardContentItemType.Image)
                {
                    Log.Verbose("Found ItemType Image");
                    if (!File.Exists(Path.Combine(directory, item.Content)))
                    {
                        continue;
                    }
                    Stream s = File.OpenRead(Path.Combine(directory, item.Content));
                    System.Drawing.Image img = Bitmap.FromStream(s, false, false); // Read only the metadata
                    double factor = img.Height / height;
                    double targetwidth = img.Width / factor;
                    if (targetwidth > width)
                    {
                        factor = img.Width / width;
                    }
                    int resHeight = (int)(img.Height / factor);
                    int resWidth = (int)(img.Width / factor);
                    BitmapImage bmp = new BitmapImage();
                    bmp.BeginInit();
                    bmp.CacheOption = BitmapCacheOption.OnLoad;
                    bmp.UriSource = new Uri(Path.Combine(directory, item.Content));
                    bmp.DecodePixelHeight = resHeight;
                    bmp.DecodePixelWidth = resWidth;
                    bmp.EndInit();
                    if (!bmp.IsFrozen) bmp.Freeze();
                    s.Dispose();
                    img.Dispose();

                    Image image = new()
                    {
                        Source = bmp,
                        Margin = new Thickness(2),
                        MaxHeight = resHeight,
                        MaxWidth = resWidth
                    };
                    container.Children.Add(image);
                }
                else if (item.Type == CardContentItemType.Text)
                {
                    if (content.ContainsLaTeX)
                    {
                        Log.Verbose("Found Text with LaTeX");
                        FormulaControl latex = new FormulaControl()
                        {
                            Formula = item.Content,
                            FontSize = font,
                            Margin = new Thickness(2),
                            Foreground = Brushes.White,
                            HorizontalAlignment = HorizontalAlignment.Center
                        };
                        container.Children.Add(latex);
                    }
                    else
                    {
                        Log.Verbose("Found Text");
                        TextBlock textBlock = new()
                        {
                            Text = item.Content,
                            FontSize = font,
                            Margin = new Thickness(2),
                            Foreground = Brushes.White,
                            HorizontalAlignment = HorizontalAlignment.Center
                        };
                        container.Children.Add(textBlock);
                    }
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
