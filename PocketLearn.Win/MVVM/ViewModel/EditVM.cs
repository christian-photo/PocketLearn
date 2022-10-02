using PocketLearn.Core.Learning;
using PocketLearn.Win.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private LearnProject project;

        public EditVM(LearnProject project)
        {
            this.project = project;
            UpdateView(project);
        }

        void UpdateView(LearnProject project)
        {
            List<object> view = new List<object>();
            foreach (LearnCard card in project.Cards)
            {
                view.Add(new EditProjectControl(card));
            }
        }
    }
}
