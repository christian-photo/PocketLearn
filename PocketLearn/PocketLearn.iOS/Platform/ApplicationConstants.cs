using Foundation;
using PocketLearn.Core.PlatformSpecifics.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UIKit;

namespace PocketLearn.iOS.Platform
{
    public class ApplicationConstants : IApplicationConstants
    {
        public string GetDataPath() => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PocketLearn");
    }
}