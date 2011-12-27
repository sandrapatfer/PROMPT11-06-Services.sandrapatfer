using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Web;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Net;
using System.Xml;
using System.IO;
using System.Net.Http.Headers;

namespace GitHubWebService
{
    class GitHubServiceImpl
    {


        [WebGet(UriTemplate="commits1")]
        HttpResponseMessage GetAllCommits(HttpRequestMessage msg)
        {
            string uri1;
            IEnumerable<CommitInfo> commits;
            GetCommits(out uri1, out commits);

            var feedTree = new SyndicationFeed("Commit List", "Commits from GITHub", new Uri(uri1));
            var items = new List<SyndicationItem>();
            foreach (var commitInfo in commits)
            {
                var item = new SyndicationItem();
                item.Title = new TextSyndicationContent("item");
                item.BaseUri = new Uri(commitInfo.url);
                if (commitInfo.commit.author != null)
                {
                    item.PublishDate = System.Convert.ToDateTime(commitInfo.commit.author.date);
                    item.Authors.Add(new SyndicationPerson() { Name = commitInfo.commit.author.name });
                }
                items.Add(item);
            }
            feedTree.Items = items;

            //var str = new StringBuilder();
            //var wr = XmlWriter.Create(str);

            var mem = new MemoryStream();
            var wr = XmlWriter.Create(mem);
            
            feedTree.SaveAsAtom10(wr);
            wr.Flush();

            //var content = new StringContent(str.ToString());
            var content = new StreamContent(mem);
            content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/atom+xml");

            var resp = new HttpResponseMessage() { StatusCode = HttpStatusCode.OK, Content = content };
            //var resp = new HttpResponseMessage() { StatusCode = HttpStatusCode.OK, Content = new StringContent("teste") };
            resp.Headers.Date = DateTimeOffset.Now;
            return resp;
        }

        [WebGet(UriTemplate = "commits")]
        HttpResponseMessage<IEnumerable<CommitInfo>> GetAllCommitsWithFormatter(HttpRequestMessage msg)
        {
            string uri1;
            IEnumerable<CommitInfo> commits;
            GetCommits(out uri1, out commits);

            var resp = new HttpResponseMessage<IEnumerable<CommitInfo>>(commits) { StatusCode = HttpStatusCode.OK };
            return resp;
        }


        private static void GetCommits(out string uri1, out IEnumerable<CommitInfo> commits)
        {
            var client = new HttpClient();
            uri1 = string.Format("https://api.github.com/repos/{0}/{1}/commits", "NancyFX", "Nancy");
            var resp1 = client.GetAsync(uri1);

            commits = resp1.Result.Content.ReadAsAsync<IEnumerable<CommitInfo>>().Result;
        }
    }



    public class Author
    {
        public string date { get; set; }
        public string name { get; set; }
    }
    public class Committer
    {
        public Author author { get; set; }
    }
    public class CommitInfo
    {
        public string url { get; set; }
        public Committer commit { get; set; }
    }
}
