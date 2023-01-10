#region "copyright"

/*
    Copyright © 2023 Christian Palm (christian@palm-family.de)
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
*/

#endregion "copyright"

using PocketLearn.Core.Interfaces.Classes;
using PocketLearn.Public.Core.Config;
using PocketLearn.Shared.Core;
using PocketLearn.Shared.Core.Learning;
using PocketLearn.Win.API;
using PocketLearn.Win.Core;
using System.IO;
using System.Threading.Tasks;

namespace PocketLearn.Win.MVVM.ViewModel
{
    public class MainWindowVM : ObservableObject
    {
        public static MainWindowVM Instance { get; private set; }

        public HomeVM HomeVM { get; private set; }
        public EditVM EditVM { get; private set; }
        public QuestionVM QuestionVM { get; set; }
        public AnswerVM AnswerVM { get; set; }
        public OptionsVM OptionsVM { get; set; }

        public ProjectManager ProjectManager { get; private set; }
        public WebAPI API { get; private set; }
        public BackgroundTask BackgroundTask { get; set; }

        public static TaskScheduler UIContext;

        public RelayCommand SetHomeVM { get; set; }

        public MainWindowVM()
        {
            Instance = this;
            UIContext = TaskScheduler.FromCurrentSynchronizationContext();

            ProjectManager = CreateProjectManager();
            foreach (LearnProject project in ProjectManager.LearnProjects)
            {
                project.InitCards();
            }
            HomeVM = new HomeVM(ProjectManager);
            EditVM = new EditVM();
            OptionsVM = new OptionsVM(ProjectManager);

            API = new WebAPI(WinConfig.Get());

            BackgroundTask = new(new WindowsNotificationSender(), ProjectManager);
            BackgroundTask.Start();

            SetHomeVM = new RelayCommand(_ =>
            {
                Utility.NavigateToPage(ApplicationConstants.HomeViewURI);
            });
        }

        private static ProjectManager CreateProjectManager()
        {
            if (File.Exists(Path.Combine(ApplicationConstants.APPLICATION_DATA_PATH, "Projects.json")))
            {
                return ProjectManager.Create(File.ReadAllText(Path.Combine(ApplicationConstants.APPLICATION_DATA_PATH, "Projects.json")), WinConfig.Get());
            }
            return ProjectManager.Create(string.Empty, WinConfig.Get());
        }
    }
}
