using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace CardGame.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerController : ControllerBase
    {

        [HttpGet]
        public List<string> GetPlayers()
        {
            // var values = await _context.Values.ToListAsync();
            // return Ok(values);

            string[] players = { "Mike", "Don" };    
            List<string> playersList = new List<string>(players);

            return playersList;
        }

        [HttpGet("{id}")]
        public string GetPlayer(int id)
        {
            // var value = await _context.Values.FirstOrDefaultAsync(x => x.Id == id);
            // return Ok(value);

            var player = "Bob";
            return player;
        }

        [HttpPost]
        public void Post(string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        
    }
}