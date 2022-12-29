using Foundation;
using PocketLearn.Core.PlatformSpecifics.Interfaces;
using System;
using System.IO;
using UIKit;

namespace PocketLearn.iOS.Platform
{
    public class FileOperations : IFile
    {
        public void SaveBase64Image(string image, string filename)
        {
            byte[] encodedDataAsBytes = Convert.FromBase64String(image);
            NSData data = NSData.FromArray(encodedDataAsBytes);
            using (FileStream fs = new FileStream(Path.Combine(App.PlatformMediator.ApplicationConstants.GetDataPath(), filename), FileMode.OpenOrCreate))
            {
                UIImage.LoadFromData(data).AsJPEG().AsStream().CopyTo(fs);
                fs.Seek(0, SeekOrigin.Begin);
                fs.Flush(true);
            }
        }
    }
}