using PocketLearn.Shared.Core.Learning;
using PocketLearn.Win.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Wpf.Ui.Controls;

namespace PocketLearn.Win.MVVM.PopUp
{
    /// <summary>
    /// Interaktionslogik für NewProjectPopUp.xaml
    /// </summary>
    public partial class NewProjectPopUp : UiWindow
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
            if( new TimeSpan(((DateTime)TargetDate.SelectedDate).Ticks).TotalDays <= new TimeSpan(DateTime.Now.Ticks).TotalDays) { return; }
            string name = ProjectName.Text;
            if(MainWindowVM.Instance.ProjectManager.ProjectNameExists(ProjectName.Text)) { name += "_"; }
            LearnProject project = new LearnProject(DateTime.Now, (DateTime)TargetDate.SelectedDate, Guid.NewGuid())
            { 
                LastLearnedTime = DateTime.Now,
                LearnSubject = (LearnSubject)Enum.GetValues(typeof(LearnSubject)).GetValue(Subject.SelectedIndex),
                ProjectName = name,
                ProjectConfig = new ProjectConfig(),
                Cards = new List<LearnCard>()
            };
            project.InitCards();
            projectManager.AddProject(project);
            Close();
        }
    }
}
