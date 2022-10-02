using System;
using System.Collections.Generic;
using System.Text;

namespace PocketLearn.Core.Learning
{
    public class CardContent
    {
        public List<dynamic> Items { get; private set; }
        public CardContent(List<dynamic> items)
        {
            Items = items;
        }
    }
}
