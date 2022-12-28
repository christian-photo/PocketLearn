using Android.OS;
using Newtonsoft.Json;
using PocketLearn.Core.PlatformSpecifics.Interfaces;
using PocketLearn.Shared.Core.Learning;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PocketLearn.Core
{
    public static class DesktopSync
    {
        /// <summary>
        /// Intelligent syncing algorithm to merge the remote project and the local project
        /// </summary>
        /// <param name="json">The json from the api request</param>
        /// <param name="containsImages">Wheter the url contains images=true</param>
        /// <param name="manager">The ProjectManager</param>
        /// <param name="constants">Platform dependend Application Constats</param>
        /// <returns>The updated LearnProject (not added to the Manager) and a bool if it was a new unknown project or not. If this is false, the Project is already in the Manager, so don't add it</returns>
        public static (LearnProject, bool) SyncProject(string json, bool containsImages, ProjectManager manager, IApplicationConstants constants)
        {
            // Create a LearnProject object deserialized from the given JSON code of the project
            LearnProject project;

            // If the project does not contain images, deserialize it directly
            if (!containsImages)
            {
                project = JsonConvert.DeserializeObject<LearnProject>(json);
            }

            // If the project contains images, deserialize it first as a list of objects and save the first element as a LearnProject object.
            // Then iterate over the remaining elements and save them as KeyValuePair<string, string> objects. For each of these KeyValuePair objects,
            // call the SaveImage method to save the image.
            else
            {
                List<object> objects = JsonConvert.DeserializeObject<List<object>>(json);
                project = JsonConvert.DeserializeObject<LearnProject>(objects[0].ToString());

                foreach (object obj in objects)
                {
                    if (obj == objects[0]) continue; // Skip the first element, as it has already been saved as a LearnProject object
                    KeyValuePair<string, string> img = JsonConvert.DeserializeObject<KeyValuePair<string, string>>(obj.ToString());
                    SaveImage(constants, img.Key, img.Value); // Save the image
                }
            }

            // Search for the LearnProject in the ProjectManager based on the ProjectID
            IEnumerable<LearnProject> localProject = manager.LearnProjects.Where(x => x.ProjectID == project.ProjectID);

            // If the LearnProject was found in the ProjectManager:
            if (localProject.Count() > 0)
            {
                // If the local LearnProject has a newer edit time than the given LearnProject:
                if (localProject.ElementAt(0).LastEdit > project.LastEdit)
                {
                    // If the local LearnProject also has a newer LastLearnedTime, return the local LearnProject without updating it
                    if (localProject.ElementAt(0).LastLearnedTime > project.LastLearnedTime)
                    {
                        return (localProject.ElementAt(0), false);
                    }
                    // Otherwise, update the LastLearnedTime of the local LearnProject and return it
                    localProject.ElementAt(0).LastLearnedTime = project.LastLearnedTime;
                    return (localProject.ElementAt(0), false);
                }
                if (localProject.ElementAt(0).LastLearnedTime > project.LastLearnedTime) 
                { 
                    project.LastLearnedTime = localProject.ElementAt(0).LastLearnedTime;
                    return (project, false);
                }
                return (project, false);
            }
            // If the LearnProject was not found in the ProjectManager, return the given LearnProject and indicate that it is new
            else
            {
                return (project, true);
            }
        }

        public static async Task SyncBack(string url, LearnProject project) // Send the updated project back to the computer
        { 
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Project", JsonConvert.SerializeObject(project));
            await client.GetAsync(url);
        }

        private static void SaveImage(IApplicationConstants constants, string filename, string image)
        {
            byte[] imgBytes = Convert.FromBase64String(image);
            using (var ms = new MemoryStream(imgBytes, 0, imgBytes.Length))
            {
                Image.FromStream(ms, true).Save(Path.Combine(constants.GetDataPath(), "Images", filename));
            }
        }
    }
}
