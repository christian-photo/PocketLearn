using PocketLearn.Shared.Core.Learning;
using PocketLearn.Win.Core;

namespace PocketLearn.Win.MVVM.ViewModel
{
    public class AnswerVM : ObservableObject
    {
        LearnProject Project { get; set; }
        private CardContent _questionContent;
        public CardContent QuestionContent { get => _questionContent; set
            {
                _questionContent = value;
                RaisePropertyChanged();
            }
        }
        private CardContent _answerContent;
        public CardContent AnswerContent { get => _answerContent; set { 
                _answerContent = value;
                RaisePropertyChanged();
            } 
        }
        public RelayCommand HardAnswer { get; set; } = new RelayCommand(_ =>
        {
            MainWindowVM.Instance.AnswerVM.Answer(CardDifficulty.Hard);
        });
        public RelayCommand MediumAnswer { get; set; } = new RelayCommand(_ =>
        {
            MainWindowVM.Instance.AnswerVM.Answer(CardDifficulty.Medium);
        });
        public RelayCommand OKAnswer { get; set; } = new RelayCommand(_ =>
        {
            MainWindowVM.Instance.AnswerVM.Answer(CardDifficulty.OK);
        });
        public RelayCommand EasyAnswer { get; set; } = new RelayCommand(_ =>
        {
            MainWindowVM.Instance.AnswerVM.Answer(CardDifficulty.Easy);
        });
        public void Answer(CardDifficulty difficulty)
        {
            Project.CardInput(difficulty);
            bool p = Project.ShouldLearn();
            if(p)
            {
                Utility.NavigateToPage(ApplicationConstants.QuestionViewURI);
                MainWindowVM.Instance.QuestionVM.NextCard();
            } else
            {
                MainWindowVM.Instance.HomeVM.UpdateView(MainWindowVM.Instance.ProjectManager);
                Utility.NavigateToPage(ApplicationConstants.HomeViewURI);
            }
        }
        public void Update()
        {
            (CardContent, CardContent) cardContents = Project.GetActiveCardContents();
            QuestionContent = cardContents.Item1;
            AnswerContent = cardContents.Item2;
        }
        
        public AnswerVM(LearnProject project)
        {
            Project = project;
        }
    }
}
