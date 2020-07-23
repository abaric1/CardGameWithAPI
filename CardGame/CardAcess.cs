using System;
using System.Threading.Tasks;
using CardGame._models;
using System.Net.Http.Headers;
using System.Net;
using System.Net.Http;
using System.Collections;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CardGame
{
    partial class Program
    {
        // podjeli 1. kartu
        // prihvati input posalji input
        // podjeli drugu kartu - ispisi rezultat
        // ....
        public class CardAcess {

        public static async Task<PlayingCard> GetCardAsync(string path)
        {
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                PlayingCard card = await response.Content.ReadAsAsync<PlayingCard>();
                // System.Console.WriteLine(card);
                return card;    
            }
            throw new Exception("error");
        }

        public static async Task<bool> ReadInputAsync(Input inputParameters) {
            ArrayList paramList = new ArrayList();
            paramList.Add(inputParameters._card1);
            paramList.Add(inputParameters._card2);
            paramList.Add(inputParameters._input);
            var response = await client.PostAsJsonAsync("card", paramList);
            if (response.IsSuccessStatusCode)
            {
                bool result = await response.Content.ReadAsAsync<bool>();
                // System.Console.WriteLine(card);
                return result;    
            }
            throw new Exception();
        }

        }
 

  




    }
}
