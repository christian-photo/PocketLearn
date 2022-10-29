using PocketLearn.Core.Learning;
using PocketLearn.Win.Core;

namespace PocketLearn.Win.MVVM.ViewModel
{
    public class QuestionVM : ObservableObject
    {
        public LearnProject Project { get; set; }
        public RelayCommand ShowAnswer { get; set; }

        private CardContent _questionContent;
        public CardContent QuestionContent { get => _questionContent;
            set
            {
                _questionContent = value;
                RaisePropertyChanged();
            }
        }
        public void NextCard()
        {
            QuestionContent = Project.ShowNextCard().Item1;
        }
        public QuestionVM(LearnProject project)
        {
            ShowAnswer = new RelayCommand(_ =>
            {
                MainWindowVM.Instance.AnswerVM.Update();
                Utility.NavigateToPage(ApplicationConstants.AnswerViewURI);
            });
            Project = project;
            NextCard();
        }
    }
}
