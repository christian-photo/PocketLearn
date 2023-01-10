#region "copyright"

/*
    Copyright © 2023 Christian Palm (christian@palm-family.de)
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
*/

#endregion "copyright"

using PocketLearn.Win.MVVM.ViewModel;
using System.Windows.Controls;

namespace PocketLearn.Win.MVVM.View
{
    /// <summary>
    /// Interaktionslogik für OptionsView.xaml
    /// </summary>
    public partial class OptionsView : UserControl
    {
        public OptionsView()
        {
            InitializeComponent();
        }

        private void UpdateProjectSettings(object sender, SelectionChangedEventArgs e)
        {
            MainWindowVM.Instance.OptionsVM.UpdateSettings();
        }

        private void Slider_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MainWindowVM.Instance.OptionsVM.SettingsChanged();
        }
    }
}
