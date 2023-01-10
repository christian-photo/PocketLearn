#region "copyright"

/*
    Copyright © 2023 Christian Palm (christian@palm-family.de)
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
*/

#endregion "copyright"

using PocketLearn.Shared.Core.Learning;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PocketLearn.ViewModels
{
    public class QuestionViewModel : BaseViewModel
    {
        public LearnProject Project { get; set; }
        public Command ShowAnswer { get; private set; }
        private CardContent _questionContent;
        public CardContent QuestionContent { get => _questionContent; set => SetProperty(ref _questionContent, value); }

        public QuestionViewModel()
        {
            Init();
        }
        public QuestionViewModel(LearnProject project)
        {
            Project = project;
            Init();
        }
        void Init()
        {
            Title = "QuestionVM";
            ShowAnswer = new Command(OnShowAnswerClicked);
        }

        public void NextCard()
        {
            
            QuestionContent = Project.ShowNextCard().Item1;
        }

        private void OnShowAnswerClicked(object obj)
        {
            HomeViewModel.Instance.AnswerViewModel.Update();
            HomeViewModel.Instance.Current = HomeViewModel.Instance.AnswerViewModel;
        }
    }
}
