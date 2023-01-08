using System;
using System.Collections.Generic;

namespace PocketLearn.Shared.Interfaces
{
    public interface IConfig
    {
        public List<(TimeSpan, TimeSpan)> LearnTimes { get; set; }
    }
}
