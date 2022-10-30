using PocketLearn.Win.Core;
using PocketLearn.Win.API;
using System.IO;
using PocketLearn.Public.Core.Config;
using PocketLearn.Core.Interfaces.Classes;
using PocketLearn.Shared.Core.Learning;
using PocketLearn.Shared.Core;

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

        public MainWindowVM()
        {
            Instance = this;

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
        }

        private ProjectManager CreateProjectManager()
        {
            if (File.Exists(Path.Combine(ApplicationConstants.APPLICATION_DATA_PATH, "Projects.json")))
            {
                return ProjectManager.Create(File.ReadAllText(Path.Combine(ApplicationConstants.APPLICATION_DATA_PATH, "Projects.json")));
            }
            return ProjectManager.Create(string.Empty);
        }
    }
}
