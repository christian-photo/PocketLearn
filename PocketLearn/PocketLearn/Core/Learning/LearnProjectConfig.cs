using System;
using System.Collections.Generic;
using System.Text;

namespace PocketLearn.Core.Learning
{
    public class LearnProjectConfig
    {
        public TimeSpan HardRepititionTime { get; set; }
        public TimeSpan MediumRepititionTime { get; set; }
        public TimeSpan OKRepititionTime { get; set; }
    }
}
