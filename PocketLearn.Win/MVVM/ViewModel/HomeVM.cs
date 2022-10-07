using PocketLearn.Core.Learning;
using PocketLearn.Win.MVVM.Model;
using PocketLearn.Win.MVVM.PopUp;
using System;
using System.Collections.Generic;

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

        public RelayCommand Learn { get; set; } = new RelayCommand(_ =>
        {
            throw new NotImplementedException();
        });

        public RelayCommand Edit { get; set; } = new RelayCommand(_ =>
        {
            throw new NotImplementedException();
        });

        public RelayCommand AddProject { get; set; }

        public HomeVM(ProjectManager projectManager)
        {
            UpdateView(projectManager);
            projectManager.ProjectsChanged += ProjectsChanged;

            AddProject = new RelayCommand(_ =>
            {
                new NewProjectPopUp(projectManager).Show();
            });
        }

        private void ProjectsChanged(object sender)
        {
            UpdateView((ProjectManager)sender);
        }

        public void UpdateView(ProjectManager projectManager)
        {
            List<object> view = new List<object>();
            foreach (LearnProject project in projectManager.LearnProjects)
            {
                view.Add(new LearningProjectControl(project));
            }
            LearningProjectsView = view;
        }
    }
}
