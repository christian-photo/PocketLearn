using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace PocketLearn.Core.Learning
{
    public class ProjectManager
    {
        public List<LearnProject> LearnProjects { get; private set; }


        public delegate void EventHandler(object sender);
        public event EventHandler ProjectsChanged;

        private static ProjectManager instance;

        public void DeleteProject(LearnProject project) // Delete from List and json file
        {
            ProjectsChanged.Invoke(this);
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
                instance = new ProjectManager();
                return instance;
            }
            instance = new ProjectManager() { LearnProjects = JsonConvert.DeserializeObject<List<LearnProject>>(JsonContent) };
            return instance;
        }
        public static ProjectManager Create(List<LearnProject> projects)
        {
            instance = new ProjectManager() { LearnProjects = projects };
            return instance;
        }

        public static LearnProject GetProjectByID(Guid projectID) => instance.LearnProjects.Where(x => x.ProjectID == projectID).FirstOrDefault();

        public string Serialize() => JsonConvert.SerializeObject(LearnProjects, Formatting.Indented);
    }
}