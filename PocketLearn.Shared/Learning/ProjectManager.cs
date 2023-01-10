#region "copyright"

/*
    Copyright © 2023 Christian Palm (christian@palm-family.de)
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
*/

#endregion "copyright"

using Newtonsoft.Json;
using PocketLearn.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketLearn.Shared.Core.Learning
{
    public class ProjectManager
    {
        public List<LearnProject> LearnProjects { get; private set; }


        public delegate void EventHandler(object sender);
        public event EventHandler ProjectsChanged;

        private static ProjectManager instance;
        private IConfig config;

        public void DeleteProject(LearnProject project) // Delete from List and json file
        {
            LearnProjects.Remove(project);
            ProjectsChanged?.Invoke(this);
        }

        public void AddProject(LearnProject project) // Add to List and serialize/save to json
        {
            LearnProjects.Add(project);
            ProjectsChanged?.Invoke(this);
        }

        private ProjectManager(IConfig config)
        {
            this.config = config;
            if (LearnProjects is null)
            {
                LearnProjects = new List<LearnProject>();
            }
        }

        public static ProjectManager Create(string JsonContent, IConfig config)
        {
            if (JsonContent == string.Empty)
            {
                instance = new ProjectManager(config);
                return instance;
            }
            instance = new ProjectManager(config) { LearnProjects = JsonConvert.DeserializeObject<List<LearnProject>>(JsonContent) };
            return instance;
        }

        public void AddLearntime(TimeSpan from, TimeSpan to)
        {
            config.LearnTimes.Add((from,to));
            ProjectsChanged?.Invoke(this);
        }
        
        public void RemoveLearnTime(TimeSpan from, TimeSpan to)
        {
            
            foreach(var s in config.LearnTimes)
            {
                if(s.Item1 == from && s.Item2 == to)
                {
                    config.LearnTimes.Remove(s);
                    break;
                }
            }
            ProjectsChanged?.Invoke(this);
        }

        public static ProjectManager Create(List<LearnProject> projects, IConfig config)
        {
            instance = new ProjectManager(config) { LearnProjects = projects };
            return instance;
        }

        public bool ProjectNameExists(string text)
        {
            foreach(var p in LearnProjects)
            {
                if(p.ProjectName == text) { return true; }
            }
            return false;
        }

        public static LearnProject GetProjectByID(Guid projectID) => instance.LearnProjects.Where(x => x.ProjectID == projectID).FirstOrDefault();

        public string Serialize() => JsonConvert.SerializeObject(LearnProjects, Formatting.Indented);
    }
}