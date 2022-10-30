using PocketLearn.Models;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.IO;
using PocketLearn.Shared.Core;
using PocketLearn.Shared.Core.Learning;

namespace PocketLearn.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public ProjectListViewModel ProjectListViewModel { get; set; }
        public QuestionViewModel QuestionViewModel { get; set; }
        public AnswerViewModel AnswerViewModel { get; set; }


        private object _current;
        public object Current { 
            get => _current;
            set => SetProperty(ref _current, value);
        }

        public HomeViewModel()
        {

            _current = new ProjectListViewModel();
        }
    }
}
