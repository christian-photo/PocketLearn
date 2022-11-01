using PocketLearn.Shared.Core.Learning;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace PocketLearn.ViewModels
{
    public class AnswerViewModel : BaseViewModel
    {
        private CardContent _answerContent;
        public CardContent AnswerContent { get => _answerContent; set => SetProperty(ref _answerContent, value); }

        private CardContent _questionContent;
        public CardContent QuestionContent { get => _questionContent; set => SetProperty(ref _questionContent, value); }


        public Command HardAnswer { get; private set; }
        public Command MediumAnswer { get; private set; }
        public Command OKAnswer { get; private set; }
        public Command EasyAnswer { get; private set; }


        public LearnProject Project { get; set; }
        public AnswerViewModel()
        {
            Init();
        }
        public AnswerViewModel(LearnProject project)
        {
            
            Project = project;
            Init();
        }
        private void Init()
        {
            Title = "AnswerVM";
            HardAnswer = new Command(OnHardAnswerClicked);
            MediumAnswer = new Command(OnMediumAnswerClicked);
            OKAnswer = new Command(OnOKAnswerClicked);
            EasyAnswer = new Command(OnEasyAnswerClicked);
        }

        private void OnHardAnswerClicked(object obj)
        {
           Answer(CardDifficulty.Hard);
        }
        private void OnMediumAnswerClicked(object obj)
        {
            Answer(CardDifficulty.Medium);
        }
        private void OnOKAnswerClicked(object obj)
        {
            Answer(CardDifficulty.OK);
        }
        private void OnEasyAnswerClicked(object obj)
        {
            Answer(CardDifficulty.Easy);
        }

        public void Update()
        {
            (CardContent, CardContent) cardContents = Project.GetActiveCardContents();
            QuestionContent = cardContents.Item1;
            AnswerContent = cardContents.Item2;
        }

        private void Answer(CardDifficulty difficulty)
        {
            Project.CardInput(difficulty);
            if(Project.ShouldLearn())
            {
                HomeViewModel.Instance.QuestionViewModel.NextCard();
                HomeViewModel.Instance.Current = HomeViewModel.Instance.QuestionViewModel;
            } else
            {
                HomeViewModel.Instance.Current = HomeViewModel.Instance.ProjectListViewModel;
            }
        }
    }
}
