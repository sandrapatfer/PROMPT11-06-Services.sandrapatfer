using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using GitHubSoapBroker.Data_Contracts;

namespace GitHubSoapBroker
{
    [DataContract]
    class IssueData
    {
        public IssueData()
        { }

        public IssueData(JsonIssue i)
        {
            Title = i.title;
            Body = i.body;
        }

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
        public string[] labels { get; set; }
    }

    public class JsonIssue
    {
        public string url { get; set; }
        public int number { get; set; }
        public string state { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public JasonLabel[] labels { get; set; }
        public JasonUser assignee { get; set; }
        public DateTime? closed_at { get; set; }
        public DateTime created_at { get; set; }
    }

}
