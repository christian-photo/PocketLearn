using PocketLearn.Core.Learning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketLearn.Win.MVVM.ViewModel
{
    public class QuestionVM : ObservableObject
    {
        public LearnProject Project { get; set; }
        public RelayCommand ShowAnswer { get; set; } = new RelayCommand(_ =>
        {
            MainWindowVM.Instance.AnswerVM.Update();
            MainWindowVM.Instance.CurrentView = MainWindowVM.Instance.AnswerVM;
        });

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
            Project = project;
            NextCard();
        }
    }
}
