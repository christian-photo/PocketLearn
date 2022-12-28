using Android.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketLearn.Core.PlatformSpecifics.Interfaces
{
    public interface IFile
    {
        public void SaveBase64Image(string image, string filename);
    }
}
