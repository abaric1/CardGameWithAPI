using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using BackendlessAPI;  
using BackendlessAPI.Async;  
using BackendlessAPI.Exception;
using CardGame._models;

namespace CardGame
{
    class Program
    {
        static HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {   
            using (client)
            {
                // spajanje na API
                client.BaseAddress = new Uri("http://localhost:5000/");
                Player player = new Player("Petra", 5);
                // await CreateNewPlayerAsync(player);
                // await GetPlayersAsync("player");
                // await GetPlayerAsync("player/60F886EC-962B-4412-8754-C56929CF43BD");
                // player.score = 7;
                // player.objectId = "60F886EC-962B-4412-8754-C56929CF43BD";
                // await UpdatePlayerAsync(player);
                // await DeletePlayerAsync("60F886EC-962B-4412-8754-C56929CF43BD");
            }

            Console.ReadLine();

        }

        //http post player
        static async Task<Uri> CreateNewPlayerAsync(Player player) {
            
        var response = await client.PostAsJsonAsync("player", player);
        response.EnsureSuccessStatusCode();

        // return URI of the created resource
        return response.Headers.Location;
        }

        //http get player
        static async Task<List<Player>> GetPlayersAsync(string path)
        {
            IList<Player> playerslist = new List<Player>();
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                playerslist = await response.Content.ReadAsAsync<IList<Player>>();
                foreach (var player in playerslist)
                {
                    System.Console.WriteLine(player.name);
                    System.Console.WriteLine(player.score); 
                }
                return (List<Player>)playerslist;    
            }
            throw new Exception();
        }

        // http get player/id
        static async Task<Player> GetPlayerAsync(string path)
        {
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                Player player = await response.Content.ReadAsAsync<Player>();
                System.Console.WriteLine(player.name);
                return player;
            }
            throw new Exception(); // ???
        }

        //http put player
        static async Task<Player> UpdatePlayerAsync(Player player)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"player/{player.objectId}", player);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            player = await response.Content.ReadAsAsync<Player>();
            return player;
        }

        //http delete player
        static async Task<HttpStatusCode> DeletePlayerAsync(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"player/{id}");
            return response.StatusCode;
        } 

    }
}
