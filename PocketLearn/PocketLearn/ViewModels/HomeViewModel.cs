using PocketLearn.Core;
using PocketLearn.Shared.Core.Learning;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Input;
using Xamarin.Forms;

namespace PocketLearn.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public static HomeViewModel Instance { get; set; }

        public ProjectListViewModel ProjectListViewModel { get; set; }
        public QuestionViewModel QuestionViewModel { get; set; }
        public AnswerViewModel AnswerViewModel { get; set; }

        private string _test = "hadsfo";
        public string Test
        {
            get => _test;
            set => SetProperty(ref _test, value);
        }


        private object _current;
        public object Current 
        { 
            get => _current;
            set => SetProperty(ref _current, value);
        }

        public ICommand Sync { get; set; }

        public HomeViewModel()
        {
            Instance = this;
            ProjectListViewModel = new ProjectListViewModel();
            Current = ProjectListViewModel;

            Sync = new Command(async () =>
            {
                ZXing.Result result = await App.PlatformMediator.QrScanner.StartScan();
                string json = await new HttpClient().GetStringAsync(result.Text);
                (LearnProject, bool) res = DesktopSync.SyncProject(json, json.Contains("images=true"), ProjectListViewModel.ProjectManager, App.PlatformMediator.ApplicationConstants);
                if (!res.Item2) await DesktopSync.SyncBack(GetRawURL(result.Text) + "/SetProject", res.Item1);
            });
        }

        private string GetRawURL(string url)
        {
            string[] split = url.Split('/');
            List<string> list = new List<string>();
            list.AddRange(split);
            list.RemoveAt(list.Count - 1);
            string res = "";
            foreach (string str in list)
            {
                res += str;
            }
            return res;
        }
    }
}
