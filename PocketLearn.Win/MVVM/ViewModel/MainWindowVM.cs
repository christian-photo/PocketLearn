using PocketLearn.Core.Learning;
using PocketLearn.Win.Core;
using PocketLearn.Win.API;
using System.IO;
using PocketLearn.Public.Core.Config;

namespace PocketLearn.Win.MVVM.ViewModel
{
    public class MainWindowVM : ObservableObject
    {
        public static MainWindowVM Instance { get; private set; }

        public HomeVM HomeVM { get; private set; }
        public EditVM EditVM { get; private set; }

        public ProjectManager ProjectManager { get; private set; }
        public WebAPI API { get; private set; }

        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                RaisePropertyChanged();
            }
        }

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

            API = new WebAPI(WinConfig.Get()); // Not working
            CurrentView = HomeVM;
        }

        private ProjectManager CreateProjectManager()
        {
            if (File.Exists(Path.Combine(ApplicationConstants.APPLICATION_DATA_PATH, "Projects.json")))
            {
                return ProjectManager.Create(File.ReadAllText(Path.Combine(ApplicationConstants.APPLICATION_DATA_PATH, "Projects.json")));
            }
            return ProjectManager.Create(null);
        }
    }
}
