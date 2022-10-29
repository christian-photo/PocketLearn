using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace PocketLearn.Core.Learning
{
    [Serializable]
    public class LearnProject
    {
        public string ProjectName { get; set; }
        public LearnSubject LearnSubject { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime HasToBeCompleted { get; set; }
        public DateTime LastLearnedTime { get; set; }
        public static List<(TimeSpan, TimeSpan)> LearnTimes { get; set; } = new List<(TimeSpan, TimeSpan)>() { (new TimeSpan(0, 0, 0), new TimeSpan(24, 0, 0)) };

        private List<LearnCard> _cards;
        public List<LearnCard> Cards { get => _cards; set { _cards = value; } }
        public ProjectConfig ProjectConfig { get; set; }

        [JsonProperty]
        public Guid ProjectID { get; private set; }

        private List<LearnCard> hardCards = new List<LearnCard>();
        private List<LearnCard> mediumCards = new List<LearnCard>();
        private List<LearnCard> okCards = new List<LearnCard>();
        private List<LearnCard> easyCards = new List<LearnCard>();
        private List<LearnCard> notLearnedCards = new List<LearnCard>();

        private LearnCard activeCard;
        private (CardContent, CardContent) activeContents;

        public LearnProject(DateTime CreationDate, DateTime HasToBeCompletet, Guid ID)
        {
            this.CreationTime = CreationDate;
            this.HasToBeCompleted = HasToBeCompletet;
            ProjectID = ID;
        }

        public void SetCards(List<LearnCard> cards)
        {
            Cards = cards;
            InitCards();
        }

        private bool IsLastDay()
        {
            return DateTime.Now.Day == HasToBeCompleted.Day-1;
        }

        public void InitCards()
        {
            hardCards.Clear();
            mediumCards.Clear();
            okCards.Clear();
            easyCards.Clear();
            notLearnedCards.Clear();
            foreach (var card in Cards)
            {
                switch (card.Difficulty)
                {
                    case CardDifficulty.Hard:
                        hardCards.Add(card);
                        break;
                    case CardDifficulty.Medium:
                        mediumCards.Add(card);
                        break;
                    case CardDifficulty.OK:
                        okCards.Add(card);
                        break;
                    case CardDifficulty.Easy:
                        easyCards.Add(card);
                        break;
                    case CardDifficulty.NotLearned:
                        notLearnedCards.Add(card);
                        break;
                }
            }
        }


        public bool ShouldLearn()
        {
            /*
            bool learnTime = false;
            TimeSpan now = DateTime.Now.TimeOfDay;
            foreach (var i in LearnTimes)
            {
                if (i.Item1 < i.Item2)
                {
                    learnTime = i.Item1 <= now && now <= i.Item2;
                }
                else
                {
                    learnTime = !(i.Item2 < now && now < i.Item1);
                }
                if (learnTime) { break; }
            }
            if (!learnTime) { return false; }

            if (easyCards.Count == Cards.Count && !IsLastDay()) { return false; }
            */
            double learnedDays = new TimeSpan(DateTime.Now.Ticks).TotalDays-new TimeSpan(CreationTime.Ticks).TotalDays+1;
            double totoalLearnDays = new TimeSpan(HasToBeCompleted.Ticks).TotalDays- new TimeSpan(CreationTime.Ticks).TotalDays+1;
            Console.WriteLine(((Cards.Count/ totoalLearnDays)*  learnedDays));
            if ((Cards.Count / totoalLearnDays) * learnedDays <= easyCards.Count) { return false; }
            return true;
        }

        public (CardContent, CardContent) ShowNextCard()
        {
            TimeSpan lastCardSpan = new TimeSpan(0);
            LearnCard lastCard = new LearnCard();
            foreach (var card in notLearnedCards)
            {
                var span = DateTime.Now - card.LastLearnedTime;
                if (span.Ticks/ProjectConfig.NotLearnedFactor >= lastCardSpan.Ticks)
                {
                    lastCard = card;
                    lastCardSpan = span;
                }
            }
            foreach (var card in hardCards)
            {
                var span = DateTime.Now - card.LastLearnedTime;
                if (span.Ticks/ProjectConfig.HardFactor >= lastCardSpan.Ticks)
                {
                    lastCard = card;
                    lastCardSpan = span;
                }
            }
            foreach (var card in mediumCards)
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
            switch (activeCard.CardType)
            {
                case CardType.OneWay:
                    activeContents = (activeCard.CardContent1, activeCard.CardContent2);
                    break;
                case CardType.TwoWay:
                    Random random = new Random();
                    if (random.Next(0, 2) == 0)
                    {
                        activeContents = (activeCard.CardContent1, activeCard.CardContent2);
                    }
                    else
                    {
                        activeContents = (activeCard.CardContent2, activeCard.CardContent1);
                    }
                    break;
                case CardType.ReverseOneWay:
                    activeContents = (activeCard.CardContent2, activeCard.CardContent1);
                    break;
                default:
                    activeContents = (null, null);
                    break;
            }
            return activeContents;

        }

        public (CardContent, CardContent) GetActiveCardContents()
        {
            return activeContents;
        }

        public void CardInput(CardDifficulty difficulty)
        {
            Cards.Remove(activeCard);
            activeCard.Difficulty = difficulty;
            activeCard.LastLearnedTime = DateTime.Now;
            LastLearnedTime = DateTime.Now;
            Cards.Add(activeCard);
            InitCards();
        }
    }
}