using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using PocketLearn.Core.Learning;
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
        [Route(HttpVerbs.Get, "/GetData")] // http://localhost:{WinConfig.Get().Port}4242/api + /GetData
        public List<LearnProject> GetLearnProjects()
        {
            return MainWindowVM.Instance.ProjectManager.LearnProjects;
        }

        [Route(HttpVerbs.Get, "/GetImages")]
        public Hashtable GetImages() // returns a hashtable of the image name and images base64 encoded
        {
            string directory = Path.Combine(ApplicationConstants.APPLICATION_DATA_PATH, "Images");
            Hashtable images = new();
            foreach (LearnProject project in MainWindowVM.Instance.ProjectManager.LearnProjects)
            {
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
            }
            return images;
        }
    }
}
