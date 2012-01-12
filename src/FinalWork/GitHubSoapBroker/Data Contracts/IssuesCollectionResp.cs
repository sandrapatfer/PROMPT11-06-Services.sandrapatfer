using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GitHubSoapBroker
{
    [DataContract]
    class IssuesCollectionResp
    {
        [DataMember]
        public List<IssueData> Issues { get; set; }
        //public IEnumerable<IssueData> Issues { get; set; }
        //public IssueData[] Issues { get; set; }
    }
}
