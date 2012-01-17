using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GitHubSoapBroker
{
    [DataContract]
    class ServiceUnavailableFault
    {
        [DataMember]
        public string Reason { get; set; }

        [DataMember]
        public DateTime RetryPeriod { get; set; }
    }
}
