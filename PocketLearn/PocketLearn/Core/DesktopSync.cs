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
        public static async Task SyncProject(string url, ProjectManager manager, IApplicationConstants constants, bool syncBack)
        {
            string json = await new HttpClient().GetStringAsync(url);

            if (!url.Contains("images=true")) // JSON doesn't contain images, only entry is the project
            {
                LearnProject project = JsonConvert.DeserializeObject<LearnProject>(json);

                IEnumerable<LearnProject> localProject = manager.LearnProjects.Where(x => x.ProjectID == project.ProjectID); // Find matching project on the local device
                if (localProject.Count() > 0) // Project is found
                {
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
                            
                            if (match.CardContent1 != card.CardContent1) // take remote CardContent if changed
                                match.CardContent1 = card.CardContent1;
                            if (match.CardContent2 != card.CardContent2) 
                                match.CardContent2 = card.CardContent2;
                        }
                        else // Card doesn't exist, add remote card to the local project
                        {
                            localProject.First().Cards.Add(card);
                        }
                    }
                    if (syncBack)
                        new HttpClient().DefaultRequestHeaders.Add("Project", JsonConvert.SerializeObject(localProject)); // Send the updated project back to the computer
                }

                else
                {
                    manager.AddProject(project);
                }
            }

            else
            {
                List<object> objects = JsonConvert.DeserializeObject<List<object>>(json);
                LearnProject project = JsonConvert.DeserializeObject<LearnProject>(objects[0].ToString());

                IEnumerable<LearnProject> localProject = manager.LearnProjects.Where(x => x.ProjectID == project.ProjectID);
                if (localProject.Count() > 0) // Project is found
                {
                    localProject.First().LastLearnedTime = localProject.First().LastLearnedTime > project.LastLearnedTime ? localProject.First().LastLearnedTime : project.LastLearnedTime;
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

                            if (match.CardContent1 != card.CardContent1) // take remote CardContent if changed
                                match.CardContent1 = card.CardContent1;
                            if (match.CardContent2 != card.CardContent2)
                                match.CardContent2 = card.CardContent2;
                        }
                        else // Card doesn't exist, add remote card to the local project
                        {
                            localProject.First().Cards.Add(card);
                        }
                    }
                    if (syncBack)
                        new HttpClient().DefaultRequestHeaders.Add("Project", JsonConvert.SerializeObject(localProject)); // Send the updated project back to the computer
                }
                else
                {
                    manager.AddProject(project);
                }

                foreach (object obj in objects)
                {
                    if (obj == objects[0]) continue;
                    KeyValuePair<string, string> img = JsonConvert.DeserializeObject<KeyValuePair<string, string>>(obj.ToString());
                    SaveImage(constants, img.Key, img.Value);
                }
            }
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
