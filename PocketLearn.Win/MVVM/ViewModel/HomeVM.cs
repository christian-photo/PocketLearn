using PocketLearn.Core.Learning;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Brushes = System.Windows.Media.Brushes;

namespace PocketLearn.Win.MVVM.ViewModel
{
    public class HomeVM : ObservableObject
    {
        private List<object> _learningProjectsView;
        public List<object> LearningProjectsView
        {
            get => _learningProjectsView;
            set
            {
                _learningProjectsView = value;
                RaisePropertyChanged();
            }
        }

        public HomeVM(ProjectManager projectManager)
        {
            UpdateView(projectManager);
            projectManager.ProjectsChanged += ProjectsChanged;
        }

        private void ProjectsChanged(object sender)
        {
            UpdateView((ProjectManager)sender);
        }

        public void UpdateView(ProjectManager projectManager)
        {
            List<object> view = new List<object>();
            foreach (LearnProject project in projectManager.LearnProjects)
            {
                Border container = new Border()
                {
                    Height = 50,
                    CornerRadius = new CornerRadius(10),
                    Margin = new Thickness(5, 15, 5, 5),
                    BorderThickness = new Thickness(0),
                    Focusable = false,
                    Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#6E6E6E")
                };

                StackPanel panel = new StackPanel()
                {
                    Orientation = Orientation.Vertical,
                    Focusable = false
                };

                StackPanel Row1 = new StackPanel()
                {
                    Orientation = Orientation.Horizontal,
                    Focusable = false,
                    Margin = new Thickness(5, 5, 5, 2)
                };

                TextBlock block = new TextBlock()
                {
                    Text = project.ProjectName,
                    Foreground = Brushes.White,
                    FontSize = 16,
                    Background = Brushes.Transparent,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Focusable = false,
                    TextWrapping = TextWrapping.Wrap
                };

                StackPanel Row2 = new StackPanel()
                {
                    Orientation = Orientation.Horizontal,
                    Focusable = false,
                    Margin = new Thickness(5, 2, 5, 5),
                };

                TextBlock Creation = new TextBlock()
                {
                    Text = $"Creation date: {project.CreationTime:dd.MM.yy}",
                    Foreground = Brushes.White,
                    FontSize = 10,
                    Background = Brushes.Transparent,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Focusable = false,
                    TextWrapping = TextWrapping.Wrap
                };

                TextBlock TargetDate = new TextBlock()
                {
                    Text = $"Learn until: {project.HasToBeCompleted:dd.MM.yy}",
                    Foreground = Brushes.White,
                    FontSize = 10,
                    Background = Brushes.Transparent,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Focusable = false,
                    TextWrapping = TextWrapping.Wrap
                };

                Row1.Children.Add(block);

                Row2.Children.Add(Creation);
                Row2.Children.Add(TargetDate);

                panel.Children.Add(Row1);
                panel.Children.Add(Row2);
                container.Child = panel;
                view.Add(container);
            }
            LearningProjectsView = view;
        }
    }
}
