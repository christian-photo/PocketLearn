#region "copyright"

/*
    Copyright © 2023 Christian Palm (christian@palm-family.de)
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
*/

#endregion "copyright"

using Microsoft.Toolkit.Uwp.Notifications;
using PocketLearn.Public.Core.Config;
using PocketLearn.Win.Core;
using PocketLearn.Win.Core.Notification;
using PocketLearn.Win.MVVM.ViewModel;
using System.IO;
using System.Windows;

namespace PocketLearn.Win
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ToastNotificationManagerCompat.OnActivated += toastArgs =>
            {
                NotificationManager.Handle(toastArgs);
            };
            base.OnStartup(e);
        }

        public void Window_Closing(object sender, ExitEventArgs e)
        {
            if (!Directory.Exists(ApplicationConstants.APPLICATION_DATA_PATH))
            {
                Directory.CreateDirectory(ApplicationConstants.APPLICATION_DATA_PATH);
            }

            File.WriteAllText(Path.Combine(ApplicationConstants.APPLICATION_DATA_PATH, "Projects.json"), MainWindowVM.Instance.ProjectManager.Serialize());
            WinConfig.Get().Save();
            MainWindowVM.Instance.API.Stop();
            MainWindowVM.Instance.BackgroundTask.Stop();
        }
    }
}
