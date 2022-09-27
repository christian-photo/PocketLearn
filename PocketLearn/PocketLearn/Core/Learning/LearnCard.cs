using System;
using System.Collections.Generic;
using System.Text;

namespace PocketLearn.Core.Learning
{
    public class LearnCard
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public Difficulty Difficulty { get; set; } = Difficulty.Medium;
    }
}
