using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PocketLearn.Core.PlatformSpecifics.Interfaces;
using PocketLearn.Shared.Core;
using PocketLearn.Shared.Core.Learning;
using System.Collections.Generic;
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
        public static (LearnProject, bool) SyncProject(string json, bool containsImages, ProjectManager manager, IFile fileOperation)
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

                foreach (JObject obj in objects)
                {
                    if (obj == objects[0]) continue; // Skip the first element, as it has already been saved as a LearnProject object
                    KeyValuePair<string, string> img = new KeyValuePair<string, string>(obj.Properties().ElementAt(0).Name, obj.Properties().ElementAt(0).Value.ToString());
                    fileOperation.SaveBase64Image(img.Value, img.Key); // Save the image
                }
            }

            // Search for the LearnProject in the ProjectManager based on the ProjectID
            IEnumerable<LearnProject> localProject = manager.LearnProjects.Where(x => x.ProjectID == project.ProjectID);

            // If the LearnProject was found in the ProjectManager:
            if (localProject.Count() > 0)
            {
                LearnProject local = localProject.ElementAt(0).MakeDeepCopy();
                if (containsImages)
                {
                    localProject.ElementAt(0).DeleteAssets();
                }
                manager.DeleteProject(localProject.ElementAt(0));
                // If the local LearnProject has a newer edit time than the given LearnProject:
                if (local.LastEdit > project.LastEdit)
                {
                    // If the local LearnProject also has a newer LastLearnedTime, return the local LearnProject without updating it
                    if (local.LastLearnedTime > project.LastLearnedTime)
                    {
                        return (local, false);
                    }
                    // Otherwise, update the LastLearnedTime of the local LearnProject and return it
                    local.LastLearnedTime = project.LastLearnedTime;
                    return (local, false);
                }
                if (local.LastLearnedTime > project.LastLearnedTime) 
                { 
                    project.LastLearnedTime = local.LastLearnedTime;
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
    }
}
