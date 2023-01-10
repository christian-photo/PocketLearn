#region "copyright"

/*
    Copyright © 2023 Christian Palm (christian@palm-family.de)
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
*/

#endregion "copyright"

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
