using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using PocketLearn.Core.Learning;
using PocketLearn.Win.MVVM.ViewModel;
using System.Collections.Generic;

namespace PocketLearn.Win.API
{
    public class APIHandler : WebApiController
    {
        [Route(HttpVerbs.Get, "/GetData")] // http://localhost:{WinConfig.Get().Port}4242/api + /GetData
        public List<LearnProject> GetLearnProjects()
        {
            return MainWindowVM.Instance.ProjectManager.LearnProjects;
        }
    }
}
