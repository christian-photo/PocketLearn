using System;
using System.Collections.Generic;

namespace PocketLearn.Core.Learning
{
    public class ProjectManager
    {
        public List<LearnProject> LearnProjects { get; private set; }

        public void DeleteProject(LearnProject project) // Delete from List and json file
        {
            throw new NotImplementedException();
        }

        public void AddProject(LearnProject project) // Add to List and serialize/save to json
        {
            throw new NotImplementedException();
        }
    }
}
