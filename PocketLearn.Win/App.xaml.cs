using PocketLearn.Public.Core.Config;
using PocketLearn.Win.Core;
using PocketLearn.Win.MVVM.ViewModel;
using System.IO;
using System.Windows;

namespace PocketLearn.Win
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        private void Window_Closing(object sender, ExitEventArgs e)
        {
            if (!Directory.Exists(ApplicationConstants.APPLICATION_DATA_PATH))
            {
                Directory.CreateDirectory(ApplicationConstants.APPLICATION_DATA_PATH);
            }

            File.WriteAllText(Path.Combine(ApplicationConstants.APPLICATION_DATA_PATH, "Projects.json"), MainWindowVM.Instance.ProjectManager.Serialize());
            WinConfig.Get().Save();
            MainWindowVM.Instance.API.Stop();
        }
    }
}
