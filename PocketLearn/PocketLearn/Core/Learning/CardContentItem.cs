namespace PocketLearn.Core.Learning
{
    public class CardContentItem<T>
    {
        public CardContentItemType Type { get; set; }
        public T Content { get; set; }

        public CardContentItem(T content, CardContentItemType type)
        {
            this.Content = content;
            this.Type = type;
        }
    }
}
