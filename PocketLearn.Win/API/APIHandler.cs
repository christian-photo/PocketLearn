using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using Newtonsoft.Json;
using PocketLearn.Shared.Core.Learning;
using PocketLearn.Win.Core;
using PocketLearn.Win.MVVM.ViewModel;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace PocketLearn.Win.API
{
    public class APIHandler : WebApiController
    {
        public static LearnProject ProjectToSync { get; set; }

        [Route(HttpVerbs.Get, "/GetProject")] // http://localhost:{WinConfig.Get().Port}/api + /GetProject
        public object GetLearnProject([QueryField] bool images = false)
        {
            if (ProjectToSync is null)
            {
                return new Hashtable() { { "Error", "NO-PROJECT" } };
            }
            if (images)
            {
                return new List<object>() { ProjectToSync, GetImages(ProjectToSync) };
            }
            return ProjectToSync;
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
            MainWindowVM.Instance.ProjectManager.LearnProjects.RemoveAll(x => x.ProjectID == ProjectToSync.ProjectID);
            MainWindowVM.Instance.ProjectManager.AddProject(JsonConvert.DeserializeObject<LearnProject>(HttpContext.Request.Headers["Project"]));
        }
    }
}
