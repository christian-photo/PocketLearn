using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PocketLearn.Core.Learning
{
    public class CardContent
    {
        public List<CardContentItem> Items { get; private set; }
        public CardContent(List<CardContentItem> items)
        {
            Items = items;
        }

        public void ClearItems(string path)
        {
            foreach (CardContentItem item in Items.Where(x => x.Type == CardContentItemType.Image))
            {
                try
                {
                    File.Delete(Path.Combine(path, item.Content));
                }
                catch { }
            }
            Items.Clear();
        }
    }
}
