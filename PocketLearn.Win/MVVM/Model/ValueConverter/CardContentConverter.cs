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
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfMath.Controls;
using Image = System.Windows.Controls.Image;

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
                string[] split = parameter.ToString().Split('-'); // 20&400&200 (font, width, height)
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
                    using MemoryStream ms = new MemoryStream(File.ReadAllBytes(Path.Combine(directory, item.Content)));

                    var decoder = BitmapDecoder.Create(ms,
                                                       BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                    BitmapSource source = decoder.Frames[0];
                    double factor = source.Height / height;
                    double targetwidth = source.Width / factor;
                    if (targetwidth > width)
                    {
                        factor = source.Width / width;
                    }
                    source.Freeze();
                    Image image = new()
                    {
                        Source = source,
                        Margin = new Thickness(2),
                        MaxHeight = source.Height / factor,
                        MaxWidth = source.Width / factor
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
                            Foreground = System.Windows.Media.Brushes.White,
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
                            Foreground = System.Windows.Media.Brushes.White,
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
