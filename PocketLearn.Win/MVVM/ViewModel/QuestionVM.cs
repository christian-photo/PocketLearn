#region "copyright"

/*
    Copyright © 2023 Christian Palm (christian@palm-family.de)
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
*/

#endregion "copyright"

using PocketLearn.Shared.Core.Learning;
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
