using System;
using InstaClient;

namespace ClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            InstaRestClient client = new InstaRestClient()
            { };
            var result = client.LogIn("REPLACE_WITH_YOUR_VALUE", "REPLACE_WITH_YOUR_VALUE");
            var profile = client.GetLocalInfo("millionaire_mentor/");
        }
    }
}
