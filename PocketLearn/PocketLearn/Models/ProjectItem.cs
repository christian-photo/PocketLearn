#region "copyright"

/*
    Copyright © 2023 Christian Palm (christian@palm-family.de)
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
*/

#endregion "copyright"

using PocketLearn.Core;
using PocketLearn.Shared.Core.Learning;
using System.Windows.Input;
using Xamarin.Forms;

namespace PocketLearn.Models
{
    public class ProjectItem
    {
        public LearnProject Project { get; set; }
        public bool ShouldLearn { get; set; }
        public ICommand DeleteProject { get; set; }


        public ProjectItem(ProjectManager manager) 
        {
            DeleteProject = new Command(() =>
            {
                Project.DeleteAssets();
                manager.DeleteProject(Project);
            });
        }
    }
}
