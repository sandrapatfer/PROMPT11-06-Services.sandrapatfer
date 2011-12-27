using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Service3Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var svc = new Service3.Service3ServiceClient();
            Service3.WordResp resp = svc.WordLookup(new Service3.WordReq() { Word = "cavalo" });
            Console.WriteLine("cavalo");
            Console.WriteLine(resp.Description);
            Console.ReadKey();
        }
    }
}
