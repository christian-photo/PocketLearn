using PocketLearn.Models;
using PocketLearn.Shared.Core;
using PocketLearn.Shared.Core.Learning;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Xamarin.Forms;

namespace PocketLearn.ViewModels
{
    public class ProjectListViewModel : BaseViewModel
    {
        private ObservableCollection<ProjectItem> _projectItems;
        public ObservableCollection<ProjectItem> ProjectItems
        {
            get => _projectItems;
            set => SetProperty(ref _projectItems, value);
        }
        public Command<ProjectItem> ProjectItemTapped { get; }

        
       // public LearnProject abc { get; set; }
        public BackgroundTask BackgroundTask { get; }
        public ProjectManager ProjectManager { get; }

        public ProjectListViewModel()
        {
            Title = "Home";
            ProjectItems = new ObservableCollection<ProjectItem>();
            ProjectItemTapped = new Command<ProjectItem>(OnItemTapped);


            ProjectManager = CreateProjectManager();
            ProjectManager.AddProject(new LearnProject(DateTime.Now, new DateTime(2023, 1, 1), Guid.NewGuid())
            {
                LastLearnedTime = DateTime.Now,
                LearnSubject = LearnSubject.Art,
                ProjectName = "asdf",
                ProjectConfig = new ProjectConfig(),
                Cards = new List<LearnCard>()
            });

            ObservableCollection<ProjectItem> items = new();
            foreach (LearnProject project in ProjectManager.LearnProjects)
            {
                project.InitCards();
                items.Add(new ProjectItem()
                {
                    Project = project,
                    ShouldLearn = project.ShouldLearn()
                });
            }
            ProjectItems = items;

            BackgroundTask = new(App.PlatformMediator.NotificationSender, ProjectManager);
            BackgroundTask.Start();
        }

        void OnItemTapped(ProjectItem item)
        {
            if (item == null)
                return;
            HomeViewModel.Instance.QuestionViewModel = new QuestionViewModel(item.Project);
            HomeViewModel.Instance.QuestionViewModel.NextCard();
            HomeViewModel.Instance.AnswerViewModel = new AnswerViewModel(item.Project);
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
