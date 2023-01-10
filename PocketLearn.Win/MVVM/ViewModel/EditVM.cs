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
using PocketLearn.Win.MVVM.Model;
using PocketLearn.Win.MVVM.PopUp;
using System;
using System.Collections.Generic;
using System.IO;

namespace PocketLearn.Win.MVVM.ViewModel
{
    public class EditVM : ObservableObject
    {
        private List<object> _learningCardsView;
        public List<object> LearningCardsView
        {
            get => _learningCardsView;
            set
            {
                _learningCardsView = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand AddCard { get; set; }

        private LearnProject project;

        public EditVM()
        {
            AddCard = new RelayCommand(_ =>
            {
                LearnCard card = new()
                {
                    CardType = CardType.OneWay,
                    Difficulty = CardDifficulty.NotLearned,
                    LastLearnedTime = DateTime.Now
                };
                new PopUpEdit(project, card).ShowDialog();
                project.Cards.Add(card);
                project.InitCards();
                File.WriteAllText(Path.Combine(ApplicationConstants.APPLICATION_DATA_PATH, "Projects.json"), MainWindowVM.Instance.ProjectManager.Serialize());
                UpdateView(project);
            });
        }

        /// <summary>
        /// Call every time when openening the edit view
        /// </summary>
        /// <param name="project">The project to be edited</param>
        public void UpdateView(LearnProject project)
        {
            List<object> view = new();
            foreach (LearnCard card in project.Cards)
            {
                view.Add(new CardControl(project, card));
            }
            LearningCardsView = view;
            this.project = project;
        }
    }
}
