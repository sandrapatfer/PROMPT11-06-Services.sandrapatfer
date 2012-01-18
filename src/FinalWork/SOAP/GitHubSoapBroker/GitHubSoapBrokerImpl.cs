using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ServiceModel;
using System.Net;

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

        private void CheckResponse(HttpResponseMessage result)
        {
            if (result.StatusCode > HttpStatusCode.InternalServerError)
            {
                throw new FaultException<ServiceUnavailableFault>(
                                    new ServiceUnavailableFault
                                    {
                                        Reason = "Connection to Git Hub is down",
                                        RetryPeriod = DateTime.Now.AddMinutes(5)
                                    });
            }
            else if (!result.IsSuccessStatusCode)
            {
                throw new FaultException<InternalServerFault>(new InternalServerFault());
            }
        }

        #region IGitHubSoapBroker Members

        public IssuesCollectionResp GetAllIssues()
        {
            var resp = _httpClient.GetAsync("https://api.github.com/issues/");
            CheckResponse(resp.Result);

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
                }).ToList() : null
            };
        }

        public IssuesCollectionResp GetRepositoryIssues(string repository)
        {
            var uri = string.Format("https://api.github.com/repos/{0}/{1}/issues", UserName, repository);

            var resp = _httpClient.GetAsync(uri);
            CheckResponse(resp.Result);
            
            var issues = resp.Result.Content.ReadAsAsync<IEnumerable<JsonIssue>>().Result;
            return new IssuesCollectionResp()
            {
                Issues = issues != null ? issues.Select(i => new IssueData() {
                    Url = i.url,
                    Number = i.number,
                    State = i.state,
                    Title = i.title,
                    Body = i.body
                }).ToList() : null
            };
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
            CheckResponse(resp.Result);
            if (resp.Result.StatusCode == System.Net.HttpStatusCode.Created)
            {
                var issue = resp.Result.Content.ReadAsAsync<JsonIssue>().Result;
                return new CreationStatusResp() { Code = StatusCode.Ok, NewEntityId = Convert.ToString(issue.number) };
            }

            throw new FaultException<InternalServerFault>(new InternalServerFault());
        }

        public StatusResp EditIssue(string repository, string id, IssueData i)
        {
            var uri = string.Format("https://api.github.com/repos/{0}/{1}/issues/{2}", UserName, repository, id);

            var resp = _httpClient.SendAsync(
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
            CheckResponse(resp.Result);

            return new StatusResp() { Code = StatusCode.Ok };
        }

        public RepositoriesCollectionResp GetRepositories()
        {
            var uri = string.Format("https://api.github.com/user/repos");
            var resp = _httpClient.GetAsync(uri);
            CheckResponse(resp.Result);
 
            var reps = resp.Result.Content.ReadAsAsync<IEnumerable<JsonRepository>>().Result;
            return new RepositoriesCollectionResp()
            {
                Repositories = reps != null ? reps.Select(r => new Repository() {
                    Name = r.name, 
                    Description = r.description,
                    Url = r.url
                }).ToArray() : null
            };
        }

        public CreationStatusResp CreateRepository(RepositoryData r)
        {
            var uri = string.Format("https://api.github.com/user/repos/");
            var resp = _httpClient.PostAsync(uri, new ObjectContent(typeof(JsonNewRepositoryData),
                new JsonNewRepositoryData() { name = r.Name, description = r.Description},
                new List<MediaTypeFormatter>() { new JsonMediaTypeFormatter() }));
            CheckResponse(resp.Result);

            if (resp.Result.StatusCode == System.Net.HttpStatusCode.Created)
            {
                var repo = resp.Result.Content.ReadAsAsync<JsonRepository>().Result;
                return new CreationStatusResp() { Code = StatusCode.Ok, NewEntityId = repo.name };
            }

            throw new FaultException<InternalServerFault>(new InternalServerFault());
        }

        public StatusResp EditRepository(string id, RepositoryData r)
        {
            var uri = string.Format("https://api.github.com/repos/{0}/{1}", UserName, id);

            var resp = _httpClient.SendAsync(
                new HttpRequestMessage(new HttpMethod("PATCH"), uri)
                { Content = new ObjectContent(typeof(JsonNewRepositoryData),
                    new JsonNewRepositoryData() { name = r.Name, description = r.Description },
                    new List<MediaTypeFormatter>() { new JsonMediaTypeFormatter() }) });
            CheckResponse(resp.Result);

            return new StatusResp() { Code = StatusCode.Ok };
        }

        #endregion

    }
}
