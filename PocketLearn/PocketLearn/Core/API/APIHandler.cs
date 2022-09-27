using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using PocketLearn.Core.Learning;
using System.Collections.Generic;

namespace PocketLearn.Win.API
{
    public class APIHandler : WebApiController
    {
        [Route(HttpVerbs.Get, "/GetData")] // http://localhost:424242/api + /GetData
        public List<LearnProject> GetLearnProjects()
        {
            return null;
        }
    }
}
