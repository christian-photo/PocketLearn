using PocketLearn.Core.Learning;
using PocketLearn.Win.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Brushes = System.Windows.Media.Brushes;

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

        public HomeVM(ProjectManager projectManager)
        {
            UpdateView(projectManager);
            projectManager.ProjectsChanged += ProjectsChanged;
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
