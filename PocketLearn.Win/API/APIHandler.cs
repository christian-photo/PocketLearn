using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using Newtonsoft.Json;
using PocketLearn.Public.Core.Config;
using PocketLearn.Shared.Core.Learning;
using PocketLearn.Win.Core;
using PocketLearn.Win.MVVM.ViewModel;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace PocketLearn.Win.API
{
    public class APIHandler : WebApiController
    {
        public static LearnProject ProjectToSync { get; set; }

        [Route(HttpVerbs.Get, "/GetProject")] // http://localhost:{WinConfig.Get().Port}/api + /GetProject
        public void GetLearnProject([QueryField] bool images = false)
        {
            if (ProjectToSync is null)
            {
                HttpContext.WriteToResponse(JsonConvert.SerializeObject(new Hashtable() { { "Error", "NO-PROJECT" } }));
            }
            if (images)
            {
                HttpContext.WriteToResponse(JsonConvert.SerializeObject(new List<object>() { ProjectToSync, GetImages(ProjectToSync) }));
            }
            HttpContext.WriteToResponse(JsonConvert.SerializeObject(ProjectToSync));
        }

        public Hashtable GetImages(LearnProject project) // returns a hashtable of the image name and images base64 encoded
        {
            string directory = Path.Combine(ApplicationConstants.APPLICATION_DATA_PATH, "Images");
            Hashtable images = new();
            foreach (LearnCard card in project.Cards)
            {
                foreach (CardContentItem item in card.CardContent1.Items.Where(x => x.Type == CardContentItemType.Image))
                {
                    images.Add(item.Content, new Bitmap(Path.Combine(directory, item.Content)).BitmapToBase64());
                }
                foreach (CardContentItem item in card.CardContent2.Items.Where(x => x.Type == CardContentItemType.Image))
                {
                    images.Add(item.Content, new Bitmap(Path.Combine(directory, item.Content)).BitmapToBase64());
                }
            }
            return images;
        }

        [Route(HttpVerbs.Get, "/SetProject")]
        public void RecieveProject()
        {
            // Remove any existing projects with the same ProjectID as ProjectToSync
            MainWindowVM.Instance.ProjectManager.LearnProjects.RemoveAll(x => x.ProjectID == ProjectToSync.ProjectID);

            // Deserialize a new project from the "Project" HTTP request header
            LearnProject newProject = JsonConvert.DeserializeObject<LearnProject>(HttpContext.Request.Headers["Project"]);

            // Loop through each card in ProjectToSync
            foreach (LearnCard card in ProjectToSync.Cards)
            {
                // Loop through each CardContentItem in card.CardContent1.Items
                foreach (CardContentItem item in card.CardContent1.Items)
                {
                    // If the CardContentItem is an image, skip it
                    if (item.Type == CardContentItemType.Image)
                    {
                        continue;
                    }
                    // Replace the content of the CardContentItem with the corresponding
                    // text content from newProject
                    item.Content = newProject.Cards.Where(x => x.CardID == card.CardID)
                        .First().CardContent1.Items.Where(i => i.Type == CardContentItemType.Text)
                        .FirstOrDefault(new CardContentItem("", CardContentItemType.Text)).Content;
                }

                // Repeat the process for card.CardContent2.Items
                foreach (CardContentItem item in card.CardContent2.Items)
                {
                    if (item.Type == CardContentItemType.Image)
                    {
                        continue;
                    }
                    item.Content = newProject.Cards.Where(x => x.CardID == card.CardID)
                        .First().CardContent2.Items.Where(i => i.Type == CardContentItemType.Text)
                        .FirstOrDefault(new CardContentItem("", CardContentItemType.Text)).Content;
                }
            }

            // Add the updated ProjectToSync back to the LearnProjects list
            MainWindowVM.Instance.ProjectManager.AddProject(ProjectToSync);
        }

        [Route(HttpVerbs.Get, "/LearnTimes")]
        public void GetLearnTimes()
        {
            HttpContext.WriteToResponse(JsonConvert.SerializeObject(WinConfig.Get().LearnTimes));
        }
    }

    public static class WebUtility
    {
        public static void WriteToResponse(this IHttpContext context, string json)
        {
            context.SendStringAsync(json, MimeType.Json, Encoding.UTF8).Wait();
        }
    }
}