using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PocketLearn.Core.PlatformSpecifics.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Environment = System.Environment;

namespace PocketLearn.Droid.Platform
{
    public class AndroidConstants : IApplicationConstants
    {
        public string GetDataPath() => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    }
}