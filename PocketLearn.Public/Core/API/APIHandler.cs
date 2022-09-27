using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketLearn.Win.API
{
    public class APIHandler : WebApiController
    {
        [Route(HttpVerbs.Get, "/GetData")] // http://localhost:424242/api + /GetData
        public List<LearnProject>
    }
}
