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
using PocketLearn.Win.MVVM.Model;
using PocketLearn.Win.MVVM.PopUp;
using System.Collections.Generic;
using System.IO;

namespace PocketLearn.Win.MVVM.ViewModel
{
    public class HomeVM : ObservableObject
    {
        private List<object> _learningProjectsView;
        public List<object> LearningProjectsView
        {
            get => _learningProjectsView;
            set
            {
                _learningProjectsView = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand AddProject { get; set; }

        public HomeVM(ProjectManager projectManager)
        {
            UpdateView(projectManager);
            projectManager.ProjectsChanged += ProjectsChanged;

            AddProject = new RelayCommand(_ =>
            {
                new NewProjectPopUp(projectManager).ShowDialog();
            });
        }

        private void ProjectsChanged(object sender)
        {
            UpdateView((ProjectManager)sender);
            if (!Directory.Exists(ApplicationConstants.APPLICATION_DATA_PATH))
            {
                Directory.CreateDirectory(ApplicationConstants.APPLICATION_DATA_PATH);
            }

            File.WriteAllText(Path.Combine(ApplicationConstants.APPLICATION_DATA_PATH, "Projects.json"), MainWindowVM.Instance.ProjectManager.Serialize());
        }

        public void UpdateView(ProjectManager projectManager)
        {
            List<object> view = new List<object>();
            foreach (LearnProject project in projectManager.LearnProjects)
            {
                view.Add(new LearningProjectControl(project, projectManager));
            }
            LearningProjectsView = view;
        }
    }
}
