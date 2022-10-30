using PocketLearn.Shared.Core.Learning;
using System;

namespace PocketLearn.Models
{
    public class ProjectItem
    {
        public LearnProject Project {get;set;}
        public bool ShouldLearn { get;set;}
    }
}
