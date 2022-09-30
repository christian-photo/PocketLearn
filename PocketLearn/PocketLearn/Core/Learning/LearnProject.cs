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
        public List<LearnCard> Cards { get => Cards; set { Cards = value; InitCards(); } }
        public ProjectConfig ProjectConfig { get; set; }

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
            if((Cards.Count - easyCards.Count) / ((HasToBeCompleted.Day-CreationTime.Day-1)/Cards.Count) > 1) { return false; }
            return true;
        }

        public (string, string) ShowNextCard()
        {
           TimeSpan lastCardSpan = new TimeSpan(0);
           LearnCard lastCard = new LearnCard();
           foreach(var card in hardCards)
            {
                var span = DateTime.Now - card.LastLearnedTime;
                if (span.Ticks/ProjectConfig.HardFactor >= lastCardSpan.Ticks)
                {
                    lastCard = card;
                    lastCardSpan = span;
                }
            }
           foreach(var card in mediumCards)
            {
                var span = DateTime.Now - card.LastLearnedTime;
                if (span.Ticks/ProjectConfig.MediumFactor >= lastCardSpan.Ticks)
                {
                    lastCard = card;
                    lastCardSpan = span;
                }
            }
            foreach (var card in okCards)
            {
                var span = DateTime.Now - card.LastLearnedTime;
                if (span.Ticks/ProjectConfig.OKFactor >= lastCardSpan.Ticks)
                {
                    lastCard = card;
                    lastCardSpan = span;
                }
            }
            foreach (var card in easyCards)
            {
                var span = DateTime.Now - card.LastLearnedTime;
                if (span.Ticks/ProjectConfig.EasyFactor >= lastCardSpan.Ticks)
                {
                    lastCard = card;
                    lastCardSpan = span;
                }
            }
            activeCard = lastCard;
            switch(activeCard.CardType)
            {
                case CardType.OneWay:
                    return((activeCard.Text1, activeCard.Text2));
                case CardType.TwoWay:
                    Random random = new Random();
                    if(random.Next(0, 2) == 0)
                    {
                        return((activeCard.Text1, activeCard.Text2));
                    } else
                    {
                        return ((activeCard.Text2, activeCard.Text1));
                    }
                case CardType.ReverseOneWay:
                    return((activeCard.Text2,activeCard.Text1));
                default:
                    return (("Placeholder", "Placeholder"));
            }

        }

        public void CardInput(Difficulty difficulty)
        {
            switch(activeCard.Difficulty)
            {
                case Difficulty.Hard:
                    hardCards.Remove(activeCard);
                    break;
                case Difficulty.Medium:
                    mediumCards.Remove(activeCard);
                    break;
                case Difficulty.OK:
                    okCards.Remove(activeCard);
                    break;
                case Difficulty.Easy:
                    easyCards.Remove(activeCard);
                    break;
            }
            switch(difficulty)
            {
                case Difficulty.Hard:
                    hardCards.Add(activeCard);
                    break;
                case Difficulty.Medium:
                    mediumCards.Add(activeCard);
                    break;
                case Difficulty.OK:
                    okCards.Add(activeCard);
                    break;
                case Difficulty.Easy:
                    easyCards.Add(activeCard);
                    break;
            }
            activeCard.Difficulty = difficulty;
            activeCard.LastLearnedTime = DateTime.Now;
            LastLearnedTime = DateTime.Now;
        }
    }
}
