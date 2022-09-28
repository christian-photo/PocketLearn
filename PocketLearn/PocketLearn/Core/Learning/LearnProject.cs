using System;
using System.Collections.Generic;

namespace PocketLearn.Core.Learning
{
    public class LearnProject
    {
        public string ProjectName { get; set; }
        public LearnSubject LearnSubject { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime HasToBeCompleted { get; set; }
        public DateTime LastLearnedTime { get; set; }
        public List<(TimeSpan, TimeSpan)> LearnTimes { get; set; }
        public List<LearnCard> Cards { get => Cards; set => { Cards = value; InitCards(); } }

        private List<LearnCard> hardCards = new List<LearnCard>();
        private List<LearnCard> mediumCards = new List<LearnCard>();
        private List<LearnCard> okCards = new List<LearnCard>();
        private List<LearnCard> easyCards = new List<LearnCard>();

        private LearnCard activeCard;

        private bool IsLastDay()
        {
            return DateTime.Now.Day == HasToBeCompleted.Day-1;
        }

        private void InitCards()
        {
            foreach (var card in Cards)
            {
                switch (card.Difficulty)
                {
                    case Difficulty.Hard:
                        hardCards.Add(card);
                        break;
                    case Difficulty.Medium:
                        mediumCards.Add(card);
                        break;
                    case Difficulty.OK:
                        okCards.Add(card);
                        break;
                    case Difficulty.Easy:
                        easyCards.Add(card);
                        break;
                }
            }
        }
            

        public bool ShouldLearn()
        {
            bool learnTime = false;
            TimeSpan now = DateTime.Now.TimeOfDay;
            foreach(var i in LearnTimes)
            {
                if (i.Item1 < i.Item2)
                {
                    learnTime = i.Item1 <= now && now <= i.Item2;
                }
                else
                {
                    learnTime = !(i.Item2 < now && now < i.Item1);
                }
                if(learnTime) { break; }
            }
            if(!learnTime) { return false; }

            if(easyCards.Count == Cards.Count && !IsLastDay()) { return false; }
            if((easyCards.Count - Cards.Count) / ((HasToBeCompleted.Day-CreationTime.Day-1)/Cards.Count) > 1) { return false; }
            return true;
        }

        public (string, string) ShowNextCard()
        {

           for(var card in hardCards)
            {

            }

        }

        public void CardInput(Difficulty difficulty)
        {
            activeCard.Difficulty = difficulty;
            activeCard.LastLearnedTime = DateTime.Now;
            LastLearnedTime = DateTime.Now;
        }
    }
}
