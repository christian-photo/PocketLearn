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
        public CardContent QuestionContent { get => QuestionContent; set
            {
                QuestionContent = value;
                RaisePropertyChanged();
            }
        }
        public CardContent AnswerContent { get => AnswerContent; set { 
                AnswerContent = value;
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
