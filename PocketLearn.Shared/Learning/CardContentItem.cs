#region "copyright"

/*
    Copyright © 2023 Christian Palm (christian@palm-family.de)
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
*/

#endregion "copyright"

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
