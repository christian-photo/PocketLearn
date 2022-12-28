using System;

namespace PocketLearn.Shared.Core.Learning
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

        public static bool operator !=(CardContentItem item1, CardContentItem item2)
        {
            return !(item1 == item2);
        }

        public static bool operator ==(CardContentItem item1, CardContentItem item2)
        {
            if (item1 is null || item2 is null) return false;

            if ((!item1.Content.Equals(item2.Content)) || item1.Type != item2.Type) return false;
            return true;
        }
    }
}
