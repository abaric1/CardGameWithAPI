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
        public class PlayerAcess {

        // init check
        public static async Task<string> InitCheckAsync(string path)
        {
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;  
                return result;
            }
            throw new Exception("Provjera imena neuspješna.");
        }

        // http post player
        public static async Task<HttpStatusCode> CreateNewPlayerAsync(Player player) {
            
            var response = await client.PostAsJsonAsync("player", player);
            response.EnsureSuccessStatusCode();
            
            return response.StatusCode;
        }

        // http get player
        public static async Task<List<Player>> GetPlayersAsync(string path)
        {
            IList<Player> playerslist = new List<Player>();
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                playerslist = await response.Content.ReadAsAsync<IList<Player>>();
                PrintRow("name", "score"); // get column names
                PrintLine();
                foreach (var player in playerslist)
                {
                    PrintRow(player.name, player.score.ToString());
                    PrintLine();
                }
                return (List<Player>)playerslist;    
            }
            throw new Exception("Igrači nisu dohvaćeni.");
        }

        // http get player/id
        public static async Task<Player> GetPlayerAsync(string path)
        {
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                Player player = await response.Content.ReadAsAsync<Player>();
                return player;
            }
            throw new Exception("Igrač nije dohvaćen.");
        }

        // http put player
        public static async Task<Player> UpdatePlayerAsync(Player player)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"player/{player.objectId}", player);
            response.EnsureSuccessStatusCode();

            player = await response.Content.ReadAsAsync<Player>();
            return player;
        }

        // http delete player
        public static async Task<HttpStatusCode> DeletePlayerAsync(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"player/{id}");
            return response.StatusCode;
        } 


        // creaating a table
        static int tableWidth = 50;

        static void PrintLine()
        {
            Console.WriteLine(new string('-', tableWidth));
        }

        static void PrintRow(params string[] columns)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
        }

        static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
        
        }
    }
}
