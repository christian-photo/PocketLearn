using PocketLearn.Core.Learning;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketLearn.Models
{
    public class ProjectItem
    {
        public LearnProject Project {get;set;}
        public bool ShouldLearn { get;set;}
    }
}
