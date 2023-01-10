#region "copyright"

/*
    Copyright © 2023 Christian Palm (christian@palm-family.de)
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
*/

#endregion "copyright"

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
        public bool ContainsLaTeX { get; set; } = false;
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
