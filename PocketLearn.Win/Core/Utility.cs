using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using Microsoft.Win32;
using System.Windows.Forms;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;
using System;
using System.Windows.Navigation;
using System.Windows;
using PocketLearn.Win.MVVM.View;
using Application = System.Windows.Application;

namespace PocketLearn.Win.Core
{
    public static class Utility
    {
        public static BitmapImage ToBitmapImage(this Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }

        public static Bitmap ToBitmap(this BitmapImage bitmapImage)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                Bitmap bitmap = new Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }

        public static List<string> FileDialog(string filter, string title)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = filter;
            dialog.Title = title;
            dialog.Multiselect = true;
            DialogResult res = dialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                return dialog.FileNames.ToList();
            }
            return null;
        }

        public static List<string> ToList(this string[] str)
        {
            List<string> list = new List<string>();
            list.AddRange(str);
            return list;
        }

        public static string BitmapToBase64(this Bitmap bmp)
        {
            Bitmap map = new(bmp);
            using (MemoryStream memory = new())
            {
                map.Save(memory, ImageFormat.Jpeg);
                return Convert.ToBase64String(memory.ToArray());
            }
        }

        public static int GetSizeFactor(int dimension, int targetDimension)
        {
            return dimension / targetDimension;
        }

        /// <summary>
        /// Sets Active Page
        /// </summary>
        /// <param name="pageUri">The URI to the page, e.g. "PocketLearn.Win;component/MVVM/View/HomeView.xaml"</param>
        public static void NavigateToPage(string pageUri)
        {
            NavigationService nav = (Application.Current.MainWindow as MainWindow).RootFrame.NavigationService;
            nav.Navigate(new Uri(pageUri, UriKind.RelativeOrAbsolute));
        }
    }
}
