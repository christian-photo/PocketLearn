using System;

namespace PocketLearn.Shared.Core.Learning
{
    public class LearnProjectConfig
    {
        public TimeSpan HardRepititionTime { get; set; }
        public TimeSpan MediumRepititionTime { get; set; }
        public TimeSpan OKRepititionTime { get; set; }
    }
}
