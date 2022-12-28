using PocketLearn.Core.PlatformSpecifics;
using PocketLearn.ViewModels;
using System.IO;
using Xamarin.Forms;

namespace PocketLearn
{
    public partial class App : Application
    {
        public static PlatformMediator PlatformMediator { get; private set; }

        public App(PlatformMediator platform)
        {
            InitializeComponent();
            PlatformMediator = platform;
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            HomeViewModel.Instance.ProjectListViewModel.BackgroundTask.Start();
        }

        protected override void OnSleep()
        {
            File.WriteAllText(Path.Combine(PlatformMediator.ApplicationConstants.GetDataPath(), "Projects.json"), HomeViewModel.Instance.ProjectListViewModel.ProjectManager.Serialize());
            HomeViewModel.Instance.ProjectListViewModel.BackgroundTask.Stop();
        }

        protected override void OnResume()
        {
            HomeViewModel.Instance.ProjectListViewModel.BackgroundTask.Start();
        }
    }
}
