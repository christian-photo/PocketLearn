#region "copyright"

/*
    Copyright © 2023 Christian Palm (christian@palm-family.de)
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
*/

#endregion "copyright"

using PocketLearn.Shared.Core.Learning;
using PocketLearn.Win.Core;
using PocketLearn.Win.MVVM.ViewModel;
using Serilog;
using System;
using System.IO;
using System.Text.RegularExpressions;
using Wpf.Ui.Controls;

namespace PocketLearn.Win.MVVM.PopUp
{
    /// <summary>
    /// Interaction logic for LearnTimePopUp.xaml
    /// </summary>
    public partial class LearnTimePopUp : UiWindow
    {
        ProjectManager projectManager;
        public LearnTimePopUp(ProjectManager projectManager)
        {
            InitializeComponent();
            this.projectManager = projectManager;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Regex regex = new(@"^[0-9]+$");
            if(regex.IsMatch(FromHourBox.Text) &&
                regex.IsMatch(FromMinutesBox.Text) &&
                regex.IsMatch(ToHourBox.Text) &&
                regex.IsMatch(ToMinutesBox.Text))
            {
                projectManager.AddLearntime(new TimeSpan(0, 
                    int.Parse(FromHourBox.Text) , 
                    int.Parse(FromMinutesBox.Text), 0), new TimeSpan(0, 
                    int.Parse(ToHourBox.Text), 
                    int.Parse(ToMinutesBox.Text), 0));
            }
            Log.Information("Created new Learntimes");

            Close();
        }
    }
}
