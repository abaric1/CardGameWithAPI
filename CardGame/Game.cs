using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using CardGame._models;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Collections;

namespace CardGame
{
    public class Game
    {
        public static async Task<Player> StartGame(Player player)
        {
                bool play = true;
                Console.OutputEncoding = Encoding.UTF8; // za ispisivanje zaka karte
                PlayingCard card1 = await Program.CardAcess.GetCardAsync("card");
                bool count = await Program.CardAcess.CheckIsPackEmptyAsync("card/count");
                while(play && count)
                {
                    int gameInput;
                    do{
                        System.Console.WriteLine(card1);
                        Console.WriteLine("higer or lower card? \n" +
                                          "1. Higher \n" +
                                          "2. Lower");
                    
                        gameInput = Convert.ToInt32(Console.ReadKey().Key);
                        Console.Clear();
                    }while(!IsInputValid(gameInput));

                    PlayingCard card2 = await Program.CardAcess.GetCardAsync("card");
                    System.Console.WriteLine(card2);

                    Input playerInput = new Input(gameInput, card1, card2);

                    bool gameResult = await Program.CardAcess.ReadInputAsync(playerInput);
                    if(gameResult)
                    {
                        Console.WriteLine("That's right! You won a point.");
                        player.score += 1;
                        card1 = card2;
                        count = await Program.CardAcess.CheckIsPackEmptyAsync("card/count");
                        Thread.Sleep(2000);
                    }
                    else
                    {
                        play = false;
                    }
                    Console.Clear();
                }
                return player;
        }


        public static async Task<int> EndGame(Player player, bool newPlayer)
        {
            Console.WriteLine("The end of the game.");
            Console.WriteLine("Your score is: {0} of 52", player.score);

            if (newPlayer)
            {
                await Program.PlayerAcess.CreateNewPlayerAsync(player);
            }
            else
            {
                Player oldData = await Program.PlayerAcess.GetPlayerAsync($"player/{player.objectId}");
                if (oldData.score < player.score) { 
                    await Program.PlayerAcess.UpdatePlayerAsync(player);
                }
            }

            int menuInputEnd;
                do
                {
                    Console.WriteLine("Do you want to continue? \n" + 
                                      "1. New game \n" +
                                      "2. HighScore Table \n" +
                                      "3. Exit");
                    menuInputEnd = Convert.ToInt32(Console.ReadKey().Key);
                    Console.Clear();
                } while (!IsInputValid(menuInputEnd));

            return menuInputEnd;
        }
        

        public static bool IsInputValid(int input)
        {
            if (input == 49 || input == 50 || input == 51)
                return true;

            return false;
        }
    }
}
