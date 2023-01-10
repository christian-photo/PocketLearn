#region "copyright"

/*
    Copyright © 2023 Christian Palm (christian@palm-family.de)
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
*/

#endregion "copyright"

using PocketLearn.Core.Config;
using PocketLearn.Core.PlatformSpecifics;
using PocketLearn.ViewModels;
using System.IO;
using Xamarin.Forms;

namespace PocketLearn
{
    public partial class App : Application
    {
        public static PlatformMediator PlatformMediator { get; private set; }

        public App(PlatformMediator platform)
        {
            InitializeComponent();
            PlatformMediator = platform;
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            HomeViewModel.Instance.ProjectListViewModel.BackgroundTask.Start();
        }

        protected override void OnSleep()
        {
            File.WriteAllText(Path.Combine(PlatformMediator.ApplicationConstants.GetDataPath(), "Projects.json"), HomeViewModel.Instance.ProjectListViewModel.ProjectManager.Serialize());
            MobileConfig.Get().Save();
        }

        protected override void OnResume()
        {
            
        }
    }
}
