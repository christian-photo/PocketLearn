using PocketLearn.Models;
using PocketLearn.Shared.Core;
using PocketLearn.Shared.Core.Learning;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace PocketLearn.ViewModels
{
    public class ProjectListViewModel : BaseViewModel
    {
        public ObservableCollection<ProjectItem> ProjectItems { get; }
        public Command<ProjectItem> ProjectItemTapped { get; }

        
        public LearnProject abc { get; set; }
        public BackgroundTask BackgroundTask { get; }
        public ProjectManager ProjectManager { get; }

        public ProjectListViewModel()
        {
            Title = "Home";
            ProjectItems = new ObservableCollection<ProjectItem>();
            ProjectItemTapped = new Command<ProjectItem>(OnItemTapped);


            ProjectManager = CreateProjectManager();
            foreach (LearnProject project in ProjectManager.LearnProjects)
            {
                project.InitCards();
                ProjectItems.Add(new ProjectItem()
                {
                    Project = project,
                    ShouldLearn = project.ShouldLearn()
                });
            }

            BackgroundTask = new(App.PlatformMediator.NotificationSender, ProjectManager);
            BackgroundTask.Start();
        }

        void OnItemTapped(ProjectItem item)
        {
            if (item == null)
                return;
            //TODO go to learn view
        }

        private ProjectManager CreateProjectManager()
        {
            if (File.Exists(Path.Combine(App.PlatformMediator.ApplicationConstants.GetDataPath(), "Projects.json")))
            {
                return ProjectManager.Create(File.ReadAllText(Path.Combine(App.PlatformMediator.ApplicationConstants.GetDataPath(), "Projects.json")));
            }
            return ProjectManager.Create(string.Empty);
        }
    }
}
