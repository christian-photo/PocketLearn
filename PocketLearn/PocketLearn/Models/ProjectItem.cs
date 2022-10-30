using PocketLearn.Core.Learning;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketLearn.Models
{
    public class ProjectItem
    {
        public LearnSubject Subject { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime HasToBeCompletesDate { get; set; }
        public bool ShouldLearn { get; set; }
        public string ProjectName { get; set; }

        public LearnProject Project { get; set; }
    }
}
