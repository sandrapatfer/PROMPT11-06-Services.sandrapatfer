using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace EndPointBehavior
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new ServiceHost(typeof(Service3.Service.Service3Impl), new Uri("http://localhost:8080"));
            host.AddServiceEndpoint(typeof(Service3.Service.IService3Service), new BasicHttpBinding(), "service");
            host.Description.Behaviors.Add(new ServiceMetadataBehavior()
            {
                HttpGetEnabled = true,
                HttpGetUrl = new Uri("http://localhost:8080/wsdl")
            });
            host.Description.Behaviors.Add(new Behaviors.ServiceBehavior());
            host.Description.Endpoints[0].Behaviors.Add(new Behaviors.EndpointBehavior());
            host.Description.Endpoints[0].Contract.Operations.Find("WordLookup").Behaviors.Add(new Behaviors.OperationBehavior());
            host.Open();
            Console.WriteLine("Service is open");
            Console.ReadKey();
        }
    }
}
