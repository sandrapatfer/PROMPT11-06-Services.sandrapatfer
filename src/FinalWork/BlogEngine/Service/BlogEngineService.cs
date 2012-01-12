using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Net.Http;
using System.ServiceModel.Syndication;
using DomainModel;
using System.Net;
using DependencyResolution;
using System.Collections.ObjectModel;
using System.Configuration;

namespace BlogEngine
{
    [ServiceContract]
    public class BlogEngineService
    {
        private readonly IPostManager _postManager;

        public BlogEngineService()
        {
            _postManager = IoC.GetPostManager();
        }

        [WebGet(UriTemplate = "/servicedoc")]
        public HttpResponseMessage GetServiceDoc(HttpRequestMessage request)
        {
            ServiceDocument doc = new ServiceDocument()
            {
                BaseUri = new Uri(request.BaseUrl("servicedoc"))
            };
            var resources = new ResourceCollectionInfo()
            {
                Title = new TextSyndicationContent("Posts"),
                BaseUri = new Uri(string.Format("{0}/posts", request.BaseUrl("servicedoc")))
            };
            //resources.Accepts.Add("application/atom+xml;type=feed");
            var wspace = new Workspace() { Title = new TextSyndicationContent("Blog") };
            wspace.Collections.Add(resources);

            doc.Workspaces.Add(wspace);

            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new ObjectContent(typeof(AtomPub10ServiceDocumentFormatter), doc.GetFormatter())
            };
        }

        [WebGet(UriTemplate = "/posts")]
        public HttpResponseMessage GetPosts(HttpRequestMessage request)
        {
            HttpResponseMessage response = new HttpResponseMessage() { StatusCode = HttpStatusCode.OK };

            List<Post> posts = _postManager.GetAllPosts();
            if (posts == null)
            {
                response.StatusCode = HttpStatusCode.NoContent;
            }
            else
            {
                if (request.AcceptsHtml())
                {
                    //todo generate html e por no response.Content
                }
                else
                {
                    var postsFeed = new SyndicationFeed();
                    postsFeed.Title = new TextSyndicationContent("List of posts");

                    postsFeed.Items = posts.Select(p => new SyndicationItem() { Title = new TextSyndicationContent(p.Title) });

                    if (request.AcceptsAtom())
                    {
                        response.Content = new ObjectContent(typeof(SyndicationFeedFormatter), postsFeed.GetAtom10Formatter());
                    }
                    else if (request.AcceptsRss())
                    {
                        response.Content = new ObjectContent(typeof(SyndicationFeedFormatter), postsFeed.GetAtom10Formatter());
                    }
                    else
                    {
                        response.StatusCode = HttpStatusCode.NoContent;
                    }
                }
            }
            return response;
        }
    }
}