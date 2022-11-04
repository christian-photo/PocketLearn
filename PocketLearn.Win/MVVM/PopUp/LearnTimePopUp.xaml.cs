
using PocketLearn.Win.Core;
using PocketLearn.Win.MVVM.ViewModel;
using System.IO;
using Wpf.Ui.Controls;

namespace PocketLearn.Win.MVVM.PopUp
{
    /// <summary>
    /// Interaction logic for LearnTimePopUp.xaml
    /// </summary>
    public partial class LearnTimePopUp : UiWindow
    {
        public LearnTimePopUp()
        {
            InitializeComponent();
        }

        public void Accept()
        {
            if (!Directory.Exists(ApplicationConstants.APPLICATION_DATA_PATH))
            {
                Directory.CreateDirectory(ApplicationConstants.APPLICATION_DATA_PATH);
            }

            File.WriteAllText(Path.Combine(ApplicationConstants.APPLICATION_DATA_PATH, "Projects.json"), MainWindowVM.Instance.ProjectManager.Serialize());
        }
    }
}
