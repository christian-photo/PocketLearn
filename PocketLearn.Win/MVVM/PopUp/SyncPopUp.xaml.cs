#region "copyright"

/*
    Copyright © 2023 Christian Palm (christian@palm-family.de)
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
*/

#endregion "copyright"

using System.Windows;
using Wpf.Ui.Controls;
using PocketLearn.Win.Core;
using PocketLearn.Public.Core.Config;
using PocketLearn.Win.API;
using PocketLearn.Shared.Core.Learning;
using PocketLearn.Shared.Core;

namespace PocketLearn.Win.MVVM.PopUp
{
    /// <summary>
    /// Interaktionslogik für SyncPopUp.xaml
    /// </summary>
    public partial class SyncPopUp : UiWindow
    {
        private LearnProject syncProject;
        public SyncPopUp(LearnProject project)
        {
            InitializeComponent();
            syncProject = project;
        }

        private void Sync(object sender, RoutedEventArgs e)
        {
            bool syncImages = SyncImages.IsChecked.Value;
            if (!syncImages)
            {
                APIHandler.ProjectToSync = syncProject;
                QrCode.Source = Utility.CreateQRCode($"http://{Utility.GetIPv4Address()}:{WinConfig.Get().Port}/api/GetProject?images=false").ToBitmapImage();
            }
            else
            {
                APIHandler.ProjectToSync = syncProject;
                QrCode.Source = Utility.CreateQRCode($"http://{Utility.GetIPv4Address()}:{WinConfig.Get().Port}/api/GetProject?images=true").ToBitmapImage();
            }
        }
    }
}
