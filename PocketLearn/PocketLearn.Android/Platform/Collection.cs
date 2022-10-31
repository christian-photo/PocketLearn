using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
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
    public class GetFileStream : IGetFileStream
    {
        public MemoryStream GetStream()
        {
            var bitmap = ...;

            var stream = new MemoryStream();
            bitmap.Compress(Bitmap.CompressFormat.Png, 100, stream);
            bitmap.Recycle();
            return stream;
        }
    }
}