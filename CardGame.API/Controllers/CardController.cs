using System;
using System.Collections;
using CardGame.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CardGame.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardController : ControllerBase
    { 
        public CardController() { }

        // get card
        [HttpGet]
        public PlayingCard GetRandomCard()
        {
            var pack = new Pack();
            PlayingCard card = pack.DealCardFromPack();
            return card;
        }

        // get number of cards
        [HttpGet("count")]
        public bool GetCheckIsPackEmpty()
        {
            var pack = new Pack();
            bool empty = pack.IsListEmpty();
            return empty;
        }

        // post input
        [HttpPost]
        public bool PostInput(ArrayList input)
        {   
            PlayingCard card1 = Newtonsoft.Json.JsonConvert
                .DeserializeObject<PlayingCard>(input[0].ToString());
            PlayingCard card2 = Newtonsoft.Json.JsonConvert
                .DeserializeObject<PlayingCard>(input[1].ToString());
            int option = Newtonsoft.Json.JsonConvert
                .DeserializeObject<int>(input[2].ToString());
            
            switch (option)
            {
                case 49:
                    if ((int)card2.Value >= (int)card1.Value)
                        return true;
  
                    return false;
                    
                case 50:
                    if ((int)card2.Value <= (int)card1.Value)
                        return true;
                    
                    return false;

                default: throw new Exception();
            } 
        }
    }
}