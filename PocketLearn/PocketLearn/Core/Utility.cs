using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Xamarin.Forms;

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
    }
}
