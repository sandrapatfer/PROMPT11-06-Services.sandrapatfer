using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestDeleteEntry();
            TestDeleteEntry_Error();

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static void TestDeleteEntry()
        {
            var client = new HttpClient();
            var task = client.DeleteAsync("http://localhost:50377/blog/posts/1");
            if (task.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Deleted!");
            }
            else
            {
                Console.WriteLine("Error deleting!");
            }
        }

        private static void TestDeleteEntry_Error()
        {
            var client = new HttpClient();
            var task = client.DeleteAsync("http://localhost:50377/blog/posts/1");
            if (task.Result.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("Not found!");
            }
            else
            {
                Console.WriteLine("Another error deleting!");
            }
        }
    }
}
