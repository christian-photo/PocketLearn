using PocketLearn.Core.Learning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketLearn.Win.MVVM.ViewModel
{
    public class EditVM : ObservableObject
    {
        public List<LearnCard> ViewLearnCards { get; set; }

        private LearnProject project;

        public void Init(LearnProject project)
        {
            ViewLearnCards = project.Cards;
            this.project = project;
        }
    }
}
