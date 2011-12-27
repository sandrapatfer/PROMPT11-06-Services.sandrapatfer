using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ApplicationServer.Http;

namespace GitHubWebService
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new HttpServiceHost(typeof(GitHubServiceImpl), "http://localhost:8080/");
            host.Open();

            Console.WriteLine("Service is running...");
            Console.ReadLine();
        }
    }
}
