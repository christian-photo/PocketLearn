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

        public HomeViewModel()
        {
            Instance = this;
            Current = new ProjectListViewModel();
        }
    }
}
