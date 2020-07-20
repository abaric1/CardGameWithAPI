using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BackendlessAPI;  
using BackendlessAPI.Async;  
using BackendlessAPI.Exception;
using CardGame.API.Models;
using System.Threading.Tasks;
using System;
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

        [HttpGet("init/{name}")]
        public bool InitPlayer(string name)
        {
            DataQueryBuilder queryBuilder = DataQueryBuilder.Create();
            queryBuilder.SetWhereClause( $"name = '{name}'");
            var result = Backendless.Data.Of<Player>().Find( queryBuilder );
            // System.Console.WriteLine(result[0]);
            if(result.Count > 0)
                return false;
            
            return true;
        }

        //get player
        [HttpGet]
        public List<Player> GetPlayersList()
        {
            IList<Player> playerslist = Backendless.Data.Of<Player>().Find();
            return (List<Player>)playerslist; 
        }

        [HttpGet("{id}")]
        public Player GetPlayer(string id)
        {
            Player player = Backendless.Data.Of<Player>().FindById(id);
            return player;
        }

        [HttpPost]
        public void PostPlayer(Player player)
        {   
            Backendless.Data.Of<Player>().Save(player);
        }

        [HttpPut("{id}")]
        public void PutPlayer(Player updatedplayer)
        {
            Player player = Backendless.Data.Of<Player>().FindById(updatedplayer.objectId);
            player.score = updatedplayer.score;

            Backendless.Persistence.Of<Player>().Save(player);

        }

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