using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace GitHubSoapBroker
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new ServiceHost(typeof(GitHubSoapBrokerImpl), new Uri("http://localhost:8080"));
            host.AddServiceEndpoint(typeof(IGitHubSoapBroker), new BasicHttpBinding(), "GitHubBroker");

            // adding support for metadata browsing
            host.Description.Behaviors.Add(new ServiceMetadataBehavior()
            {
                HttpGetEnabled = true,
                HttpGetUrl = new Uri("http://localhost:8080/wsdl")
            });

            host.Open();
            Console.WriteLine("Service is open");
            Console.ReadKey();
        }
    }
}
