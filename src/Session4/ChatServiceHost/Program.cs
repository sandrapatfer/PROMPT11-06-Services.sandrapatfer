using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace ChatServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new ServiceHost(typeof(ServiceContractImpl), new Uri(@"net.tcp://localhost:8080"));
            host.AddServiceEndpoint(typeof(ChatServiceContract.IServiceContract),
                new NetTcpBinding(SecurityMode.None), @"net.tcp://localhost:8080");
            host.Open();
            Console.WriteLine("Service is open");
            Console.ReadKey();
        }
    }
}
