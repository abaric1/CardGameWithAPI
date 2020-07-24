using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using BackendlessAPI;
using BackendlessAPI.Async;
using BackendlessAPI.Exception;
using CardGame._models;

namespace CardGame
{
    public partial class Program
    {
        public class DataAcess {

        // init check
        public static async Task<string> InitCheckAsync(string path)
        {
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;  
                return result;
            }
            throw new Exception(); // ???
        }

        //http post player
        public static async Task<Uri> CreateNewPlayerAsync(Player player) {
            
        var response = await client.PostAsJsonAsync("player", player);
        response.EnsureSuccessStatusCode();

        // return URI of the created resource
        return response.Headers.Location;
        }

        //http get player
        public static async Task<List<Player>> GetPlayersAsync(string path)
        {
            IList<Player> playerslist = new List<Player>();
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                playerslist = await response.Content.ReadAsAsync<IList<Player>>();
                System.Console.WriteLine("name score");
                foreach (var player in playerslist)
                {
                    System.Console.WriteLine(player.name + "   " + player.score);
                }
                return (List<Player>)playerslist;    
            }
            throw new Exception();
        }

        // http get player/id
        public static async Task<Player> GetPlayerAsync(string path)
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
        public static async Task<Player> UpdatePlayerAsync(Player player)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"player/{player.objectId}", player);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            player = await response.Content.ReadAsAsync<Player>();
            return player;
        }

        //http delete player
        public static async Task<HttpStatusCode> DeletePlayerAsync(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"player/{id}");
            return response.StatusCode;
        } 
}
        

    }
}
