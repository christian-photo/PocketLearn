
using PocketLearn.Shared.Core.Learning;
using PocketLearn.Win.Core;
using PocketLearn.Win.MVVM.ViewModel;
using System;
using System.IO;
using System.Text.RegularExpressions;
using Wpf.Ui.Controls;

namespace PocketLearn.Win.MVVM.PopUp
{
    /// <summary>
    /// Interaction logic for LearnTimePopUp.xaml
    /// </summary>
    public partial class LearnTimePopUp : UiWindow
    {
        ProjectManager projectManager;
        public LearnTimePopUp(ProjectManager projectManager)
        {
            InitializeComponent();
            this.projectManager = projectManager;
        }

        public void Accept()
        {
            if (!Directory.Exists(ApplicationConstants.APPLICATION_DATA_PATH))
            {
                Directory.CreateDirectory(ApplicationConstants.APPLICATION_DATA_PATH);
            }

            File.WriteAllText(Path.Combine(ApplicationConstants.APPLICATION_DATA_PATH, "Projects.json"), MainWindowVM.Instance.ProjectManager.Serialize());
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Regex regex = new(@"^[0-9]+$");
            if(regex.IsMatch(FromHourBox.Text) &&
                regex.IsMatch(FromMinutesBox.Text) &&
                regex.IsMatch(ToHourBox.Text) &&
                regex.IsMatch(ToMinutesBox.Text))
            {
                projectManager.AddLearntime(new TimeSpan(0, int.Parse(FromHourBox.Text) , int.Parse(FromMinutesBox.Text), 0), new TimeSpan(0, int.Parse(ToHourBox.Text), int.Parse(ToMinutesBox.Text), 0));
            }

            Close();
        }
    }
}
