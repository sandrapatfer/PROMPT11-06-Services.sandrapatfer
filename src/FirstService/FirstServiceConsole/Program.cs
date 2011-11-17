using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace FirstServiceConsole
{
    [ServiceContract]
    interface IDictService
    {
        [OperationContract]
        WordLookupResp WordLookup(WordLookupReq req);
    }

    class DictService : IDictService
    {
        Dictionary<string, string> m_dictionary = new Dictionary<string, string>() { { "professor", "aquele que ensina" } };
        public WordLookupResp WordLookup(WordLookupReq req)
        {
            string descr;
            if (m_dictionary.TryGetValue(req.Word, out descr))
            {
                return new WordLookupResp() { Exists = true, Description = descr};
            }
            else
            {
                return new WordLookupResp() { Exists = false };
            }       
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var host = new ServiceHost(typeof(DictService), new Uri("http://localhost:8080"));
            host.AddServiceEndpoint(typeof(IDictService), new BasicHttpBinding(), "d1");
            host.Description.Behaviors.Add(new ServiceMetadataBehavior() { HttpGetEnabled = true, HttpGetUrl = new Uri("http://localhost:8080/wsdl") });
            host.Open();
            Console.WriteLine("Service is open");
            Console.ReadKey();
        }
    }
}
