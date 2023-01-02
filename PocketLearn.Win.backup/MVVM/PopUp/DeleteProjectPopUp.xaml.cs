using PocketLearn.Shared.Core.Learning;
using PocketLearn.Win.Core;
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
            Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
