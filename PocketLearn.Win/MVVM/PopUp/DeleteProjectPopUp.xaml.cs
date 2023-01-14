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
using System.Windows;
using Wpf.Ui.Controls;

namespace PocketLearn.Win.MVVM.PopUp
{
    /// <summary>
    /// Interaktionslogik für DeleteProjectPopUp.xaml
    /// </summary>
    public partial class DeleteProjectPopUp : UiWindow
    {
        LearnProject project;
        ProjectManager manager;
        public DeleteProjectPopUp(LearnProject project, ProjectManager manager)
        {
            this.project = project;
            this.manager = manager;
            InitializeComponent();
            Project.Text = project.ProjectName;
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            project.DeleteAssets();
            manager.DeleteProject(project);
            Log.Information($"Deleted {project.ProjectName}, {project.ProjectID}");
            Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
