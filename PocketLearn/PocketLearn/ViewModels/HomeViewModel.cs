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
using PocketLearn.Core.Interfaces;
using PocketLearn.Droid;

namespace PocketLearn.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {

        public ObservableCollection<ProjectItem> ProjectItems { get; }
        public Command<ProjectItem> ProjectItemTapped { get; }

        public HomeViewModel Instance { get; }

        public IApplicationConstants ApplicationConstants { get; }
        public BackgroundTask BackgroundTask { get; }
        public ProjectManager ProjectManager { get; }

        public HomeViewModel()
        {
            Title = "Home";
            ProjectItems = new ObservableCollection<ProjectItem>();
            ProjectItemTapped = new Command<ProjectItem>(OnItemTapped);

            Instance = this;

            ApplicationConstants = new AndroidConstants();

            ProjectManager = CreateProjectManager();
            foreach (LearnProject project in ProjectManager.LearnProjects)
            {
                project.InitCards();
            }

            BackgroundTask = new(new WindowsNotificationSender(), ProjectManager);
            BackgroundTask.Start();
        }

        void OnItemTapped(ProjectItem item)
        {
            if (item == null)
                return;
            //TODO got to learn view
        }

        private ProjectManager CreateProjectManager()
        {
            if (File.Exists(Path.Combine(ApplicationConstants.GetDataPath(), "Projects.json")))
            {
                return ProjectManager.Create(File.ReadAllText(Path.Combine(ApplicationConstants.GetDataPath(), "Projects.json")));
            }
            return ProjectManager.Create(string.Empty);
        }
    }
}
