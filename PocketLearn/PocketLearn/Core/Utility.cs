using PocketLearn.Shared.Core.Learning;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Xamarin.Forms;
using Color = Xamarin.Forms.Color;

namespace PocketLearn.Core
{
    public static class Utility
    {
        public static ImageSource ToBitmapImage(this Bitmap bitmap)
        {
            return ImageSource.FromStream(() =>
            {
                MemoryStream ms = new MemoryStream();
                bitmap.Save(ms, ImageFormat.Png);
                ms.Position = 0;
                return ms;
            });
        }

        public static SolidColorBrush GetColorFromHex(string hexaColor)
        {
            return new SolidColorBrush(Color.FromHex(hexaColor));
        }

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
                    File.Delete(Path.Combine(App.PlatformMediator.ApplicationConstants.GetDataPath(), item.Content));
                }
            }
            foreach (CardContentItem item in card.CardContent2.Items)
            {
                if (item.Type != CardContentItemType.Text)
                {
                    File.Delete(Path.Combine(App.PlatformMediator.ApplicationConstants.GetDataPath(), item.Content));
                }
            }
        }
    }
}
