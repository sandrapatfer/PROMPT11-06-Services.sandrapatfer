using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GitHubSoapBroker
{
    [DataContract]
    public class RepositoryData
    {
        public RepositoryData()
        { }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }
    }

    [DataContract]
    public class Repository : RepositoryData
    {
        [DataMember]
        public string Id { get; set; }
    }

    public class JsonRepository
    {
        public string url { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int open_issues { get; set; }
        public string created_at { get; set; }
    }

    public class JsonNewRepositoryData
    {
        public string name { get; set; }
        public string description { get; set; }
    }

}
