using Android.Icu.Text;
using Android.Net.Vcn;
using Android.Net.Wifi.Hotspot2.Pps;
using PocketLearn.Core.Learning;
using PocketLearn.Core;
using PocketLearn.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using static Android.Telephony.CarrierConfigManager;
using System.IO;
using PocketLearn.Core.PlatformSpecifics.Interfaces;
using PocketLearn.Core.PlatformSpecifics;

namespace PocketLearn.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {

        public ObservableCollection<ProjectItem> ProjectItems { get; }
        public Command<ProjectItem> ProjectItemTapped { get; }

        public HomeViewModel Instance { get; }
        public LearnProject abc { get; set; }
        public BackgroundTask BackgroundTask { get; }
        public ProjectManager ProjectManager { get; }

        public HomeViewModel()
        {
            Title = "Home";
            ProjectItems = new ObservableCollection<ProjectItem>();
            ProjectItemTapped = new Command<ProjectItem>(OnItemTapped);

            Instance = this;

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
