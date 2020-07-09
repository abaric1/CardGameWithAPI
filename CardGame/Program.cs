using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CardGame
{
    class Program
    {
        static HttpClient client = new HttpClient();
        static void Main(string[] args)
        {
            using (client)
            {
                client.BaseAddress = new Uri("http://localhost:5000/");

                //HTTP GET
                var responseTask = client.GetAsync("player");
                responseTask.Wait();
                
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<string>>();
                    readTask.Wait();

                    foreach (var player in readTask.Result)
                    {
                        Console.WriteLine(player);
                    }
                }
            }

            Console.ReadLine();

        }
    }
}
