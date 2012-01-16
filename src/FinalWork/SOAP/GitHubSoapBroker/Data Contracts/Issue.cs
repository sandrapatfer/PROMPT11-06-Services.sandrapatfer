using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GitHubSoapBroker
{
    [DataContract]
    public class IssueData
    {
        [DataMember]
        public string Url { get; set; }
        [DataMember]
        public int Number { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Body { get; set; }
        [DataMember]
        public IEnumerable<string> labels { get; set; }
    }

    public class JsonIssue
    {
        public string url { get; set; }
        public int number { get; set; }
        public string state { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public IEnumerable<JsonLabel> labels { get; set; }
        public JsonUser assignee { get; set; }
        public string closed_at { get; set; }
        public string created_at { get; set; }
    }

}
