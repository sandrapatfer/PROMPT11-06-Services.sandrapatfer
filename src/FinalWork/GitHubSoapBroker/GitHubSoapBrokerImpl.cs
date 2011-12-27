using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;

namespace GitHubSoapBroker
{
    class GitHubSoapBrokerImpl : IGitHubSoapBroker
    {
        #region Properties
        private HttpClient _httpClient;
        const string UserName = "sandrapatfer";
        #endregion

        private GitHubSoapBrokerImpl()
        {
            _httpClient = new HttpClient();
            var bytes = Encoding.ASCII.GetBytes("sandrapatfer:ticha123");
            var authHeader = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytes));
            _httpClient.DefaultRequestHeaders.Authorization = authHeader;
        }

        #region IGitHubSoapBroker Members

        public IssuesCollectionResp GetAllIssues()
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/json"));
            var resp = client.GetAsync("http://developer.github.com/issues/");
            var issues = resp.Result.Content.ReadAsAsync<JsonIssue[]>().Result;
            return new IssuesCollectionResp() { Issues = issues.Select(i => new IssueData(i)).ToArray() };
        }

        public IssuesCollectionResp GetRepositoryIssues(string repository)
        {
            var uri = string.Format("https://api.github.com/repos/{0}/{1}/issues", UserName, repository);
            var resp = _httpClient.GetAsync(uri);
            var issues = resp.Result.Content.ReadAsAsync<IEnumerable<JsonIssue>>().Result;

            return new IssuesCollectionResp() { Issues = issues.Select(i => new IssueData(i)).ToArray() };
        }

        public RepositoriesCollectionResp GetRepositories()
        {
            var uri = string.Format("https://api.github.com/user/repos");
            var resp = _httpClient.GetAsync(uri);

            return null;
        }

        public StatusResp CreateIssue(string repository, IssueData i)
        {
            var uri = string.Format("https://api.github.com/repos/{0}/{1}/issues", "sandrapatfer", repository);
            var task = _httpClient.PostAsync(uri, new ObjectContent(typeof(JsonIssue), new JsonIssue() { title = i.Title }, 
                new List<MediaTypeFormatter>() { new JsonMediaTypeFormatter() }));
            var resp = task.Result;
            
            //var str = resp.Content.ReadAsStringAsync().Result;

            var ret = new StatusResp() { Code = StatusCode.Ok };
            return ret;
        }

        public StatusResp EditIssue(string repository, string id, IssueData i)
        {
            var uri = string.Format("https://api.github.com/repos/{0}/{1}/issues/{2}", "sandrapatfer", repository, id);

            var task = _httpClient.SendAsync(
                new HttpRequestMessage(new HttpMethod("PATCH"), uri)
                { Content = new ObjectContent(typeof(JsonIssue), 
                    new JsonIssue() { title = "aaa" }, 
                    new List<MediaTypeFormatter>() { new JsonMediaTypeFormatter() }) });
            var resp = task.Result;
            
            //var str = resp.Content.ReadAsStringAsync().Result;
        
            var ret = new StatusResp() { Code = StatusCode.Ok };
            return ret;
        }

        #endregion

    }
}
