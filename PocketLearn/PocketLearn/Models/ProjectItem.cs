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
