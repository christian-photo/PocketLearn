using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using System.Windows.Forms;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;
using System;
using System.Windows.Navigation;
using Application = System.Windows.Application;
using System.Runtime.Serialization.Formatters.Binary;
using QRCoder;
using System.Net.Sockets;
using System.Net;
using PocketLearn.Shared.Core.Learning;

namespace PocketLearn.Win.Core
{
    public static class Utility
    {

        public static void DeleteAssets(this LearnProject proj)
        {
            proj.Cards.ForEach(x => x.DeleteAssets());
        }

        public static void DeleteAssets(this LearnCard card)
        {
            foreach (CardContentItem item in card.CardContent1.Items)
            {
                if (item.Type != CardContentItemType.Text)
                {
                    File.Delete(Path.Combine(ApplicationConstants.APPLICATION_DATA_PATH, "Images", item.Content));
                }
            }
            foreach (CardContentItem item in card.CardContent2.Items)
            {
                if (item.Type != CardContentItemType.Text)
                {
                    File.Delete(Path.Combine(ApplicationConstants.APPLICATION_DATA_PATH, "Images", item.Content));
                }
            }
        }


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

        /// <summary>
        /// 1L = Lowest Quality
        /// 25L = Low Quality
        /// 50L = Medium Quality
        /// 75L = High Quality
        /// 100L = Highest Quality
        /// </summary>
        /// <returns></returns>
        public static EncoderParameters GetCompression(long quality = 50L)
        {
            var encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, quality);
            return encoderParameters;
        }

        public static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            var codecs = ImageCodecInfo.GetImageDecoders();
            foreach (var codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
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

        /// <summary>
        /// Sets Active Page
        /// </summary>
        /// <param name="pageUri">The URI to the page, e.g. "PocketLearn.Win;component/MVVM/View/HomeView.xaml"</param>
        public static void NavigateToPage(string pageUri)
        {
            NavigationService nav = (Application.Current.MainWindow as MainWindow).RootFrame.NavigationService;
            nav.Navigate(new Uri(pageUri, UriKind.RelativeOrAbsolute));
        }

        public static void NavigateToPage(Uri pageUri)
        {
            NavigationService nav = (Application.Current.MainWindow as MainWindow).RootFrame.NavigationService;
            nav.Navigate(pageUri);
        }

        public static T MakeDeepCopy<T>(this T obj)
        {
            T o;
            using (MemoryStream ms = new())
            {
                new BinaryFormatter().Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                o = (T)new BinaryFormatter().Deserialize(ms);
            }
            return o;
        }

        public static Bitmap CreateQRCode(string content)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            return qrCode.GetGraphic(20);
        }

        public static string GetIPv4Address()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return null;
        }
    }
}
