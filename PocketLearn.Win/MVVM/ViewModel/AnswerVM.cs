using PocketLearn.Core.Learning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketLearn.Win.MVVM.ViewModel
{
    public class AnswerVM : ObservableObject
    {
        public RelayCommand HardAnswer { get; set; } = new RelayCommand(_ =>
        {

        });
        public RelayCommand MediumAnswer { get; set; } = new RelayCommand(_ =>
        {
            
        });
        public RelayCommand OKAnswer { get; set; } = new RelayCommand(_ =>
        {
           
        });
        public RelayCommand EasyAnswer { get; set; } = new RelayCommand(_ =>
        {
            
        });
        private void Answer(CardDifficulty difficulty)
        {

        }
        public void Update()
        {

        }
        LearnProject Project { get; set; }
        public AnswerVM()
        {

        }
        public AnswerVM(LearnProject project)
        {
            Project = project;
        }
    }
}
