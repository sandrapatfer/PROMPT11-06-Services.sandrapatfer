using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;

namespace GitHubSoapBroker
{
    class GitHubConnection : HttpClient
    {
        public static bool IsConnected { get; set; }
    }
}
