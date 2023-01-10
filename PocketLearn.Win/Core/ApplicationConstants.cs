#region "copyright"

/*
    Copyright © 2023 Christian Palm (christian@palm-family.de)
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
*/

#endregion "copyright"

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

        public static string GetNewTempFile()
        {
            return Path.Combine(Path.GetTempPath(), Path.GetTempFileName());
        }

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

        public static string AnswerViewURI = "PocketLearn.Win;component/MVVM/View/AnswerView.xaml";
        public static string QuestionViewURI = "PocketLearn.Win;component/MVVM/View/QuestionView.xaml";
        public static string EditViewURI = "PocketLearn.Win;component/MVVM/View/EditView.xaml";
        public static string HomeViewURI = "PocketLearn.Win;component/MVVM/View/HomeView.xaml";
        public static string OptionsViewURI = "PocketLearn.Win;component/MVVM/View/OptionsView.xaml";
    }
}
