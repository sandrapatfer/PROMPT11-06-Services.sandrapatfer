using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GitHubSoapBroker
{
    [DataContract]
    class RepositoriesCollectionResp
    {
        [DataMember]
        public Repository[] Repositories { get; set; }
    }
}
