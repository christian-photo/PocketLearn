using System;
using System.Collections.Generic;
using System.Text;

namespace PocketLearn.Core.Learning
{
    public class CardContentItem<T>
    {
        public CardContentItemType Type { get; set; }
        public T Content { get; set; } 

    }
}
