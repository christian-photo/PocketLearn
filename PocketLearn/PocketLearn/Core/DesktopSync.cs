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
            LearnProject project;

            if (!containsImages) // JSON doesn't contain images, only entry is the project
            {
                project = JsonConvert.DeserializeObject<LearnProject>(json);
            }

            else // Images exist
            {
                List<object> objects = JsonConvert.DeserializeObject<List<object>>(json);
                project = JsonConvert.DeserializeObject<LearnProject>(objects[0].ToString()); // Project is first entry, later entries are images

                foreach (object obj in objects)
                {
                    if (obj == objects[0]) continue; // skip project
                    KeyValuePair<string, string> img = JsonConvert.DeserializeObject<KeyValuePair<string, string>>(obj.ToString());
                    SaveImage(constants, img.Key, img.Value); // Key = filename, Value = image as Base64 (jpg)
                }
            }

            IEnumerable<LearnProject>  localProject = manager.LearnProjects.Where(x => x.ProjectID == project.ProjectID); // Look for existing project
            if (localProject.Count() > 0) // Project is found
            {
                localProject.First().ProjectConfig = localProject.First().ProjectConfig.LastEdit > project.ProjectConfig.LastEdit ? localProject.First().ProjectConfig : project.ProjectConfig;
                localProject.First().LastLearnedTime = localProject.First().LastLearnedTime > project.LastLearnedTime ? localProject.First().LastLearnedTime : project.LastLearnedTime; // Take the later LastLearnedTime from both projects
                foreach (LearnCard card in project.Cards) // Look through all remote cards
                {
                    LearnCard match = localProject.First().Cards.Find(x => x.CardID == card.CardID);
                    if (match is not null) // Card exists, pick more up to date
                    {
                        if (match.LastLearnedTime < card.LastLearnedTime)
                        {
                            match.LastLearnedTime = card.LastLearnedTime; // Take the later LastLearnedTime from both Cards
                            match.Difficulty = card.Difficulty;
                        }

                        match.CardContent1 = match.LastEdit > card.LastEdit ? match.CardContent1 : card.CardContent1;
                        match.CardContent2 = match.LastEdit > card.LastEdit ? match.CardContent2 : card.CardContent2;
                    }
                    else // Card doesn't exist, add remote card to the local project
                    {
                        localProject.First().Cards.Add(card);
                    }
                }
                return (localProject.First(), false);
            }
            else
            {
                return (project, true); // If the project doesn't exist, add it to the projects on the local device
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
