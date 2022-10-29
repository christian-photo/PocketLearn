using PocketLearn.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace PocketLearn.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {

        public ObservableCollection<ProjectItem> ProjectItems { get; }
        public Command<Item> ProjectItemTapped { get; }

        public HomeViewModel()
        {
            Title = "Home";
            ProjectItems = new ObservableCollection<ProjectItem>();
            ProjectItemTapped = new Command<Item>(OnItemTapped);
        }

        void OnItemTapped(ProjectItem item)
        {
            if (item == null)
                return;
            //TODO got to learn view
        }
    }
}
