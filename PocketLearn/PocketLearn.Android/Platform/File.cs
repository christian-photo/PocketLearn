using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using PocketLearn.Core.PlatformSpecifics.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;

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