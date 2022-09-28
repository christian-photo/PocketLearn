using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace PocketLearn.Core.Learning
{
    public class ProjectManager
    {
        public List<LearnProject> LearnProjects { get; private set; }


        public delegate void EventHandler(object sender);
        public event EventHandler ProjectsChanged;

        public void DeleteProject(LearnProject project) // Delete from List and json file
        {
            throw new NotImplementedException();
        }

        public void AddProject(LearnProject project) // Add to List and serialize/save to json
        {
            LearnProjects.Add(project);
            ProjectsChanged.Invoke(this);
        }

        private ProjectManager()
        {
            if (LearnProjects is null)
            {
                LearnProjects = new List<LearnProject>();
            }
        }

        public static ProjectManager Create(string JsonContent)
        {
            if (JsonContent == null)
            {
                return new ProjectManager();
            }

            return new ProjectManager() { LearnProjects = JsonConvert.DeserializeObject<List<LearnProject>>(JsonContent) };
        }

        public string Serialize() => JsonConvert.SerializeObject(LearnProjects, Formatting.Indented);
    }
}
