using PocketLearn.Core.Learning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketLearn.Win.MVVM.ViewModel
{
    public class AnswerVM : ObservableObject
    {
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
            if(Project.ShouldLearn())
            {
                MainWindowVM.Instance.CurrentView = MainWindowVM.Instance.QuestionVM;
                MainWindowVM.Instance.QuestionVM.NextCard();
            }
        }
        public void Update()
        {
            var cardContents = Project.GetActiveCardContents();
            QuestionContent = cardContents.Item1;
            AnswerContent = cardContents.Item2;
        }
        LearnProject Project { get; set; }
        public AnswerVM()
        {

        }
        public AnswerVM(LearnProject project)
        {
            Project = project;
        }
    }
}
