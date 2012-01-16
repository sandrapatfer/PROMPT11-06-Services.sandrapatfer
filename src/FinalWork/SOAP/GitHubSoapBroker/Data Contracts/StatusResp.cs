using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GitHubSoapBroker
{
    enum StatusCode
    {
        Ok,
        Error
    }

    [DataContract]
    class StatusResp
    {
        [DataMember]
        public StatusCode Code { get; set; }
    }

    [DataContract]
    class CreationStatusResp : StatusResp
    {
        [DataMember]
        public string NewEntityId { get; set; }
    }
}
