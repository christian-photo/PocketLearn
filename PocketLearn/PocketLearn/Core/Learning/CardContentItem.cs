using System;

namespace PocketLearn.Core.Learning
{
    [Serializable]
    public class CardContentItem
    {
        public CardContentItemType Type { get; set; }
        public string Content { get; set; }

        public CardContentItem(string content, CardContentItemType type)
        {
            this.Content = content;
            this.Type = type;
        }
    }
}
