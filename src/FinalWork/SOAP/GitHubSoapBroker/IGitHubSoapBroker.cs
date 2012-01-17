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
        [FaultContract(typeof(ServiceUnavailableFault))]
        IssuesCollectionResp GetAllIssues();

        [OperationContract]
        [FaultContract(typeof(ServiceUnavailableFault))]
        IssuesCollectionResp GetRepositoryIssues(string repository);

        [OperationContract]
        [FaultContract(typeof(ServiceUnavailableFault))]
        RepositoriesCollectionResp GetRepositories();

        [OperationContract]
        [FaultContract(typeof(ServiceUnavailableFault))]
        CreationStatusResp CreateIssue(string repository, IssueData i);

        [OperationContract]
        [FaultContract(typeof(ServiceUnavailableFault))]
        StatusResp EditIssue(string repository, string id, IssueData i);

        [OperationContract]
        [FaultContract(typeof(ServiceUnavailableFault))]
        CreationStatusResp CreateRepository(RepositoryData r);

        [OperationContract]
        [FaultContract(typeof(ServiceUnavailableFault))]
        StatusResp EditRepository(string id, RepositoryData r);
    }
}
