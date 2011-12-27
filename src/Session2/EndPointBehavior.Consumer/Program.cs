using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace EndPointBehavior.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var fact = new ChannelFactory<Service3.IService3ServiceChannel>(new BasicHttpBinding(),
                new EndpointAddress("http://localhost:8080/service"));
            fact.Endpoint.Behaviors.Add(new Behaviors.EndpointBehavior());
            Service3.WordResp resp = fact.CreateChannel().WordLookup(new Service3.WordReq() { Word = "teste" });
            Console.WriteLine("teste");
            Console.WriteLine(resp.Description);
            Console.ReadKey();
        }
    }
}
