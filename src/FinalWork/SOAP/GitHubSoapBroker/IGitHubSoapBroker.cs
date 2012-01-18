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
        [FaultContract(typeof(InternalServerFault))]
        IssuesCollectionResp GetAllIssues();

        [OperationContract]
        [FaultContract(typeof(ServiceUnavailableFault))]
        [FaultContract(typeof(InternalServerFault))]
        IssuesCollectionResp GetRepositoryIssues(string repository);

        [OperationContract]
        [FaultContract(typeof(ServiceUnavailableFault))]
        [FaultContract(typeof(InternalServerFault))]
        CreationStatusResp CreateIssue(string repository, IssueData i);

        [OperationContract]
        [FaultContract(typeof(ServiceUnavailableFault))]
        [FaultContract(typeof(InternalServerFault))]
        StatusResp EditIssue(string repository, string id, IssueData i);

        [OperationContract]
        [FaultContract(typeof(ServiceUnavailableFault))]
        [FaultContract(typeof(InternalServerFault))]
        RepositoriesCollectionResp GetRepositories();

        [OperationContract]
        [FaultContract(typeof(ServiceUnavailableFault))]
        [FaultContract(typeof(InternalServerFault))]
        CreationStatusResp CreateRepository(RepositoryData r);

        [OperationContract]
        [FaultContract(typeof(ServiceUnavailableFault))]
        [FaultContract(typeof(InternalServerFault))]
        StatusResp EditRepository(string id, RepositoryData r);
    }
}
