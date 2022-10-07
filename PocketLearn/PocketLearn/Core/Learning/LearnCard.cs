using System;
using System.Collections.Generic;

namespace PocketLearn.Core.Learning
{
    public class LearnCard
    {
        public CardContent CardContent1 { get; set; }
        public CardContent CardContent2 { get; set; }
        public CardType CardType { get; set; }
        public DateTime LastLearnedTime { get; set; }
        public CardDifficulty Difficulty { get; set; }

        public LearnCard()
        {
            CardContent1 = new CardContent(new List<CardContentItem>());
            CardContent2 = new CardContent(new List<CardContentItem>());
        }
    }
}
