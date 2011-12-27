using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace BlogEngine.Service
{
    [ServiceContract]
    public class BlogEngineService
    {
        [WebGet(UriTemplate = "")]
        string GetPosts()
        {
            return "testing";
        }
    }
}