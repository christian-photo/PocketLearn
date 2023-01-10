#region "copyright"

/*
    Copyright © 2023 Christian Palm (christian@palm-family.de)
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
*/

#endregion "copyright"

using Android.Graphics;
using Android.Util;
using PocketLearn.Core.PlatformSpecifics.Interfaces;
using System.IO;

namespace PocketLearn.Droid.Platform
{
    public class File : IFile
    {

        public void SaveBase64Image(string image, string filename)
        {
            byte[] decodedString = Base64.Decode(image, Base64Flags.Default);

            Bitmap decodedBitmap = BitmapFactory.DecodeByteArray(decodedString, 0, decodedString.Length);

            using (FileStream fs = new FileStream(System.IO.Path.Combine(App.PlatformMediator.ApplicationConstants.GetDataPath(), filename), FileMode.OpenOrCreate))
            {
                decodedBitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, fs);
            }
        }
    }
}