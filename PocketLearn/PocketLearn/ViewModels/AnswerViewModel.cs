using PocketLearn.Shared.Core.Learning;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace PocketLearn.ViewModels
{
    public class AnswerViewModel : BaseViewModel
    {
        private CardContent _answerContent;
        public CardContent AnswerContent { get => _answerContent; set => SetProperty(ref _answerContent, value); }

        private CardContent _questionContent;
        public CardContent QuestionContent { get => _questionContent; set => SetProperty(ref _questionContent, value); }


        public LearnProject Project { get; set; }
        public AnswerViewModel()
        {
            Title = "AnswerVM";
        }
        public AnswerViewModel(LearnProject project)
        {
            Project = project;
        }
    }
}
