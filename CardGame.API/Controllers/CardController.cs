using CardGame.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CardGame.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardController : ControllerBase
    { 
        public CardController() { }

        //get card
        [HttpGet]
        public PlayingCard GetRandomCard()
        {
            // provjeriti ili li jos karata
            // provjeriti je li vec karta podjeljena

            var pack = new Pack();
            PlayingCard card = pack.DealCardFromPack();
            return card;
        }

        [HttpPost]
        public void PostInput(int input)
        {   
            
        }

    }
}