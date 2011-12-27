using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Service3
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new ServiceHost(typeof(Service.Service3Impl), new Uri("http://localhost:8080"));
            host.AddServiceEndpoint(typeof(Service.IService3Service), new BasicHttpBinding(), "service");
            host.Description.Behaviors.Add(new ServiceMetadataBehavior() { 
                HttpGetEnabled = true, HttpGetUrl = new Uri("http://localhost:8080/wsdl") });
            host.Open();
            Console.WriteLine("Service is open");
            Console.ReadKey();
        }
    }
}
