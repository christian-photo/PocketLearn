using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace PocketLearn.Win.Core
{
    public class ApplicationConstants
    {
        public static string APPLICATION_NAME = "PocketLearn";
        public static DateTime APPLICATION_START = DateTime.Now;
        public static string APPLICATIONDIRECTORY = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static string APPLICATION_DATA_PATH = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PocketLearn");

        public static string Version
        {
            get
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                string version = fvi.FileVersion;
                return version;
            }
        }
    }
}
