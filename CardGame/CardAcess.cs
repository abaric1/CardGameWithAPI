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
        public class CardAcess {

            public static async Task<PlayingCard> GetCardAsync(string path)
            {
                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    PlayingCard card = await response.Content.ReadAsAsync<PlayingCard>();
                    return card;    
                }
                throw new Exception("Karta nije dohvaćena.");
            }

            public static async Task<bool> ReadInputAsync(Input inputParameters) {
                ArrayList paramList = new ArrayList() {
                    inputParameters.Card1, inputParameters.Card2, inputParameters.PlayerInput};

                var response = await client.PostAsJsonAsync("card", paramList);
                if (response.IsSuccessStatusCode)
                {
                    bool result = await response.Content.ReadAsAsync<bool>();
                    return result;    
                }
                throw new Exception("Input nije poslan.");
            }
        }
    }
}
