using PocketLearn.Core.Learning;
using PocketLearn.Win.MVVM.Model;
using System;
using System.Collections.Generic;

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
                    CardContent1 = new CardContent(new List<dynamic>() { "test" }),
                    CardContent2 = new CardContent(new List<dynamic>() { "test2" }),
                    CardType = CardType.OneWay,
                    Difficulty = CardDifficulty.NotLearned,
                    LastLearnedTime = DateTime.Now
                };
                project.Cards.Add(card);
                project.InitCards();
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
