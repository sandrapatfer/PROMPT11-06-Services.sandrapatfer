using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace GitHubSoapBroker
{
    class GitHubSoapBrokerImpl : IGitHubSoapBroker
    {
        #region Properties
        private HttpClient _httpClient;
        const string UserName = "spf-uc06";
        #endregion

        private GitHubSoapBrokerImpl()
        {
            _httpClient = new HttpClient();
            var bytes = Encoding.ASCII.GetBytes("spf-uc06:spf1234");
            var authHeader = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytes));
            _httpClient.DefaultRequestHeaders.Authorization = authHeader;
        }

        #region IGitHubSoapBroker Members

        public IssuesCollectionResp GetAllIssues()
        {
            // TODO meter no bionding qq coisa q verifique para todas as operações se o 
            // httpclient existe e se não lancar uma fault exception, não soap exception
            // perceber a diferença tb era bonito!

            var resp = _httpClient.GetAsync("https://api.github.com/issues/");
            if (resp.Result.IsSuccessStatusCode)
            {
                var issues = resp.Result.Content.ReadAsAsync<IEnumerable<JsonIssue>>().Result;
                return new IssuesCollectionResp()
                {
                    Issues = issues != null ? issues.Select(i => new IssueData()
                    {
                        Url = i.url,
                        Number = i.number,
                        State = i.state,
                        Title = i.title,
                        Body = i.body
                        //            labels = i.labels.Select(l => l.name);
                    }).ToList() : null
                };
            }

            throw new SoapException("Error processing request", SoapException.ServerFaultCode);
        }

        public IssuesCollectionResp GetRepositoryIssues(string repository)
        {
            var uri = string.Format("https://api.github.com/repos/{0}/{1}/issues", UserName, repository);
            var resp = _httpClient.GetAsync(uri);
            if (resp.Result.IsSuccessStatusCode)
            {
                var issues = resp.Result.Content.ReadAsAsync<IEnumerable<JsonIssue>>().Result;
                return new IssuesCollectionResp()
                {
                    Issues = issues != null ? issues.Select(i => new IssueData() {
                        Url = i.url,
                        Number = i.number,
                        State = i.state,
                        Title = i.title,
                        Body = i.body
//            labels = i.labels.Select(l => l.name);
                    }).ToList() : null
                };
            }

            throw new SoapException("Error processing request", SoapException.ServerFaultCode);
        }

        public CreationStatusResp CreateIssue(string repository, IssueData i)
        {
            var uri = string.Format("https://api.github.com/repos/{0}/{1}/issues", UserName, repository);
            var resp = _httpClient.PostAsync(uri, new ObjectContent(typeof(JsonIssue),
                new JsonIssue() {
                    title = i.Title,
                    body = i.Body
                },
                new List<MediaTypeFormatter>() { new JsonMediaTypeFormatter() }));
            if (resp.Result.StatusCode == System.Net.HttpStatusCode.Created)
            {
                var issue = resp.Result.Content.ReadAsAsync<JsonIssue>().Result;
                return new CreationStatusResp() { Code = StatusCode.Ok, NewEntityId = Convert.ToString(issue.number) };
            }

            throw new SoapException("Error processing request", SoapException.ServerFaultCode);
        }

        public StatusResp EditIssue(string repository, string id, IssueData i)
        {
            var uri = string.Format("https://api.github.com/repos/{0}/{1}/issues/{2}", UserName, repository, id);

            var task = _httpClient.SendAsync(
                new HttpRequestMessage(new HttpMethod("PATCH"), uri)
                {
                    Content = new ObjectContent(typeof(JsonIssue),
                      new JsonIssue()
                      {
                          title = i.Title,
                          body = i.Body
                      },
                      new List<MediaTypeFormatter>() { new JsonMediaTypeFormatter() })
                });
            if (task.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return new StatusResp() { Code = StatusCode.Ok };
            }

            throw new SoapException("Error processing request", SoapException.ServerFaultCode);
        }

        public RepositoriesCollectionResp GetRepositories()
        {
            var uri = string.Format("https://api.github.com/user/repos");
            var task = _httpClient.GetAsync(uri);
            if (task.Result.IsSuccessStatusCode)
            {
                var reps = task.Result.Content.ReadAsAsync<IEnumerable<JsonRepository>>().Result;
                return new RepositoriesCollectionResp()
                {
                    Repositories = reps != null ? reps.Select(r => new Repository() {
                        Name = r.name, 
                        Description = r.description,
                        Url = r.url
                    }).ToArray() : null
                };
            }

            throw new SoapException("Error processing request", SoapException.ServerFaultCode);
        }

        public CreationStatusResp CreateRepository(RepositoryData r)
        {
            var uri = string.Format("https://api.github.com/user/repos/");
            var task = _httpClient.PostAsync(uri, new ObjectContent(typeof(JsonNewRepositoryData),
                new JsonNewRepositoryData() { name = r.Name, description = r.Description},
                new List<MediaTypeFormatter>() { new JsonMediaTypeFormatter() }));
            if (task.Result.StatusCode == System.Net.HttpStatusCode.Created)
            {
                var repo = task.Result.Content.ReadAsAsync<JsonRepository>().Result;
                return new CreationStatusResp() { Code = StatusCode.Ok, NewEntityId = repo.name };
            }

            throw new SoapException("Error processing request", SoapException.ServerFaultCode);
        }

        public StatusResp EditRepository(string id, RepositoryData r)
        {
            var uri = string.Format("https://api.github.com/repos/{0}/{1}", UserName, id);

            var task = _httpClient.SendAsync(
                new HttpRequestMessage(new HttpMethod("PATCH"), uri)
                { Content = new ObjectContent(typeof(JsonNewRepositoryData),
                    new JsonNewRepositoryData() { name = r.Name, description = r.Description },
                    new List<MediaTypeFormatter>() { new JsonMediaTypeFormatter() }) });
            if (task.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return new StatusResp() { Code = StatusCode.Ok };
            }

            throw new SoapException("Error processing request", SoapException.ServerFaultCode);
        }

        #endregion

    }
}
