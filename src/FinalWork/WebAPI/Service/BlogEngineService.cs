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
using System.Net.Http.Formatting;
using System.Xml;

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
            string baseUrl = request.BaseUrl("servicedoc");

            ServiceDocument doc = new ServiceDocument();
            var postCollection = new ResourceCollectionInfo()
            {
                Title = new TextSyndicationContent("Posts"),
                Link = new Uri(string.Format("{0}/posts", baseUrl))
            };
            postCollection.Accepts.Add("application/atom+xml;type=entry");

            var wspace = new Workspace() { Title = new TextSyndicationContent("The Blog") };
            wspace.Collections.Add(postCollection);

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
            string baseUrl = request.BaseUrl("posts");

            List<Post> posts = _postManager.GetAllPosts();
            if (posts == null || posts.Count == 0)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.NotFound
                };
            }
            else
            {
                if (request.AcceptsHtml())
                {
                    return new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new ObjectContent(typeof(List<Post>), posts, new List<MediaTypeFormatter>() { new XmlMediaTypeFormatter() })
                    };
                }
                else
                {
                    var postsFeed = new SyndicationFeed()
                    {
                        Id = baseUrl,
                        Title = new TextSyndicationContent("List of posts"),
                        LastUpdatedTime = new DateTimeOffset(DateTime.Now)
                    };
                    postsFeed.Links.Add(new SyndicationLink() 
                    {
                        Uri = request.RequestUri,
                        RelationshipType = "self"
                    });
                    postsFeed.Items = posts.Select(p => new SyndicationItem()
                    {
                        Id = p.Id,
                        Title = new TextSyndicationContent(p.Title),
                        LastUpdatedTime = p.LastUpdatedTime,
                        Content =  new TextSyndicationContent(p.Content)
                    });

                    if (request.AcceptsAtom() || request.AcceptsAll())
                    {
                        return new HttpResponseMessage()
                        {
                            StatusCode = HttpStatusCode.OK,
                            Content = new ObjectContent(typeof(Atom10FeedFormatter), postsFeed.GetAtom10Formatter())
                        };
                    }

                    return new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.InternalServerError
                    };
                }
            }
        }

        [WebGet(UriTemplate = "/posts/{id}")]
        public HttpResponseMessage GetPost(string id, HttpRequestMessage request)
        {
            var post = _postManager.Get(id);
            if (post == null)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            if (request.AcceptsHtml())
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new ObjectContent(typeof(Post), post, new List<MediaTypeFormatter>() { new XmlMediaTypeFormatter() })
                };
            }
            else if (request.AcceptsAtom())
            {
                var item = new SyndicationItem()
                {
                    Id = post.Id,
                    Title = new TextSyndicationContent(post.Title),
                    LastUpdatedTime = post.LastUpdatedTime,
                    Content = new TextSyndicationContent(post.Content)
                };
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new ObjectContent(typeof(Atom10ItemFormatter), item.GetAtom10Formatter())
                };
            }
            else
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        [WebInvoke(UriTemplate = "/posts", Method = "POST")]
        public HttpResponseMessage CreatePost(HttpRequestMessage request)
        {
            var reader = XmlReader.Create(request.Content.ReadAsStreamAsync().Result);
            var item = SyndicationItem.Load(reader);

            var post = new Post()
            {
                Title = item.Title.Text,
                Content = item.Content is TextSyndicationContent? ((TextSyndicationContent)item.Content).Text : string.Empty,
                LastUpdatedTime = new DateTimeOffset(DateTime.Now)
            };

            try
            {
                _postManager.Create(post);
                HttpResponseMessage response = new HttpResponseMessage() { StatusCode = HttpStatusCode.Created };
                response.Headers.Location = new Uri(string.Format("{0}/{1}", request.BaseUrl("posts"), post.Id));
                return response;
            }
            catch
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        [WebInvoke(UriTemplate = "/posts/{id}", Method = "PUT")]
        public HttpResponseMessage EditPost(string id, HttpRequestMessage request)
        {
            var post = _postManager.Get(id);
            if (post == null)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            var reader = XmlReader.Create(request.Content.ReadAsStreamAsync().Result);
            var item = SyndicationItem.Load(reader);

            post.Title = item.Title.Text;
            post.Content = item.Content is TextSyndicationContent ? ((TextSyndicationContent)item.Content).Text : string.Empty;
            post.LastUpdatedTime = new DateTimeOffset(DateTime.Now);

            try
            {
                _postManager.Update(post);
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        [WebInvoke(UriTemplate = "/posts/{id}", Method = "DELETE")]
        public HttpResponseMessage DeletePost(string id, HttpRequestMessage request)
        {
            var post = _postManager.Get(id);
            if (post == null)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            try
            {
                _postManager.Delete(id);
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}