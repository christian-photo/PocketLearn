using PocketLearn.Core.Learning;
using PocketLearn.Win.Core;
using PocketLearn.Win.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PocketLearn.Win.MVVM.PopUp
{
    /// <summary>
    /// Interaktionslogik für NewProjectPopUp.xaml
    /// </summary>
    public partial class NewProjectPopUp : Window
    {
        ProjectManager projectManager;
        public NewProjectPopUp(ProjectManager manager)
        {
            InitializeComponent();
            projectManager = manager;

            foreach (LearnSubject subject in Enum.GetValues(typeof(LearnSubject)))
            {
                Subject.Items.Add(subject.ToString());
            }
            Subject.SelectedIndex = 0;
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Accept(object sender, RoutedEventArgs e)
        {
            LearnProject project = new LearnProject()
            { 
                CreationTime = DateTime.Now,
                HasToBeCompleted = TargetDate.DisplayDate,
                LastLearnedTime = DateTime.Now,
                LearnSubject = (LearnSubject)Enum.GetValues(typeof(LearnSubject)).GetValue(Subject.SelectedIndex),
                ProjectName = ProjectName.Text,
                ProjectConfig = new ProjectConfig(),
                Cards = new List<LearnCard>()
            };
            project.InitCards();
            projectManager.AddProject(project);
            Close();
        }
    }
}
