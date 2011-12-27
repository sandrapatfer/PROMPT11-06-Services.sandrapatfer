using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Json;

namespace Session5.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new HttpClient();
            var uri1 = string.Format("https://api.github.com/repos/{0}/{1}/commits", "NancyFX", "Nancy");
            var resp = client.GetAsync(uri1);

            //var commits = resp.Result.Content.ReadAsAsync<JsonValue>().Result;
            //System.Console.WriteLine(commits.ToString(JsonSaveOptions.EnableIndent));

            var commits = resp.Result.Content.ReadAsAsync<IEnumerable<CommitInfo>>().Result;
            foreach (var commitInfo in commits)
            {
                System.Console.WriteLine("Commit url {0}", commitInfo.url);
                if (commitInfo.commit.author != null)
                {
                    System.Console.WriteLine("Commit date {0}", commitInfo.commit.author.date);
                    System.Console.WriteLine("Commit by {0}", commitInfo.commit.author.name);
                }
            }
            System.Console.ReadLine();
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
