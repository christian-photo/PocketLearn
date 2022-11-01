using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace PocketLearn.Shared.Core.Learning
{
    [Serializable]
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

        public static bool operator !=(CardContent card1, CardContent card2)
        {
            return !(card1 == card2);
        }

        public static bool operator ==(CardContent card1, CardContent card2)
        {
            if (card1 == null || card2 == null) return false;
            if (card1.Items.Count != card2.Items.Count) return false;

            foreach (CardContentItem item in card1.Items)
            {
                if (card2.Items[card1.Items.IndexOf(item)] != item) return false;
            }
            return true;
        }
    }
}
