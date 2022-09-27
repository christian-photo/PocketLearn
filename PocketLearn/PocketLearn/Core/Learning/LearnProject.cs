using System;
using System.Collections.Generic;

namespace PocketLearn.Core.Learning
{
    public class LearnProject
    {
        public string ProjectName { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime HasToBeCompleted { get; set; }
        public List<LearnCard> Cards { get; set; }
    }
}
