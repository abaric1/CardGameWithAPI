using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BackendlessAPI;
using CardGame.API.Models;
using BackendlessAPI.Persistence;

namespace CardGame.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerController : ControllerBase
    {
        public PlayerController()
        {
            Backendless.InitApp( "47BE5FFA-993F-0CAC-FFA9-CD4995406400",
                                 "3B4C0B58-ED57-4DEB-835C-517094CB7EEC" );
        }

        // get player
        [HttpGet]
        public List<Player> GetPlayersList()
        {
            IList<Player> playerslist = Backendless.Data.Of<Player>().Find();
            return (List<Player>)playerslist; 
        }

        // get player/id
        [HttpGet("{id}")]
        public Player GetPlayer(string id)
        {
            Player player = Backendless.Data.Of<Player>().FindById(id);
            return player;
        }

        // get player/init/name
        [HttpGet("init/{name}")]
        public string InitPlayer(string name)
        {
            DataQueryBuilder queryBuilder = DataQueryBuilder.Create();
            queryBuilder.SetWhereClause( $"name = '{name}'");
            var result = Backendless.Data.Of<Player>().Find( queryBuilder );
            if(result.Count > 0)
                return result[0].objectId;
            return "";
        }

        // post player
        [HttpPost]
        public void PostPlayer(Player player)
        {   
            Backendless.Data.Of<Player>().Save(player);
        }

        // put player/id
        [HttpPut("{id}")]
        public void PutPlayer(Player updatedplayer)
        {
            Player player = Backendless.Data.Of<Player>().FindById(updatedplayer.objectId);
            player.score = updatedplayer.score;

            Backendless.Persistence.Of<Player>().Save(player);

        }

        // delete player/id
        [HttpDelete("{id}")]
        public void DeletePlayer(string id)
        {
            Player player = Backendless.Data.Of<Player>().FindById(id);
            Backendless.Data.Of<Player>().Remove(player);
        }

        /*
        //get player async
        [HttpGet]
        public List<Player> GetPlayersList()
        {
            var check = 0;
            AsyncCallback<IList<Player>> responder = new AsyncCallback<IList<Player>>(
                responseHandler => {
                    Console.WriteLine("response");
                    System.Console.WriteLine(responseHandler);
                    playerslist = responseHandler;
                    check = 1;
                }
                ,errorHandler => Console.WriteLine("error")
                );

           Backendless.Data.Of<Player>().Find(responder);
           do {  } while(check == 0);
           return (List<Player>)playerslist;
            
        }

        //post player async
        [HttpPost]
        public void PostPlayer(Player player)
        {   
            // 2. save object asynchronously
            AsyncCallback<Player> responder = new AsyncCallback<Player>(
                result =>
                {
                System.Console.WriteLine("player has been saved");
                },
                fault =>
                {
                System.Console.WriteLine("server reported an error");
                });

            Backendless.Data.Of<Player>().Save(player, responder);
        }

        */
        
    }
}