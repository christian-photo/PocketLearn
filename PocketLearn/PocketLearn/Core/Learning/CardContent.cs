using System.Collections.Generic;

namespace PocketLearn.Core.Learning
{
    public class CardContent
    {
        public List<CardContentItem> Items { get; private set; }
        public CardContent(List<CardContentItem> items)
        {
            Items = items;
        }
    }
}
