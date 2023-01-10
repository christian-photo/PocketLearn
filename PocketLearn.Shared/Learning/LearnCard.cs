#region "copyright"

/*
    Copyright © 2023 Christian Palm (christian@palm-family.de)
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
*/

#endregion "copyright"

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace PocketLearn.Shared.Core.Learning
{
    [Serializable]
    public class LearnCard
    {
        public CardContent CardContent1 { get; set; }
        public CardContent CardContent2 { get; set; }
        public CardType CardType { get; set; }
        public DateTime LastLearnedTime { get; set; }
        public DateTime LastEdit { get; set; }
        public CardDifficulty Difficulty { get; set; }

        [JsonProperty]
        public Guid CardID { get; private set; } = Guid.NewGuid();

        public LearnCard()
        {
            CardContent1 = new CardContent(new List<CardContentItem>());
            CardContent2 = new CardContent(new List<CardContentItem>());
        }
    }
}
