using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace GitHubSoapBroker
{
    [ServiceContract]
    interface IGitHubSoapBroker
    {
        [OperationContract]
        IssuesCollectionResp GetAllIssues();

        [OperationContract]
        IssuesCollectionResp GetRepositoryIssues(string repository);

        [OperationContract]
        RepositoriesCollectionResp GetRepositories();

        [OperationContract]
        CreationStatusResp CreateIssue(string repository, IssueData i);

        [OperationContract]
        StatusResp EditIssue(string repository, string id, IssueData i);

        [OperationContract]
        CreationStatusResp CreateRepository(RepositoryData r);

        [OperationContract]
        StatusResp EditRepository(string id, RepositoryData r);
    }
}
