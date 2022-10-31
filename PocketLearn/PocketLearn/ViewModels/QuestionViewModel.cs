using PocketLearn.Shared.Core.Learning;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PocketLearn.ViewModels
{
    public class QuestionViewModel : BaseViewModel
    {
        public Command ShowAnswer { get; }
        private CardContent _questionContent;
        public CardContent QuestionContent { get => _questionContent; set => SetProperty(ref _questionContent, value); }

        public QuestionViewModel()
        {
            ShowAnswer = new Command(OnShowAnswerClicked);
        }

        private void OnShowAnswerClicked(object obj)
        {

        }
    }
}
