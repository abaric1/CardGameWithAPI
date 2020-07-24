using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using CardGame._models;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;

namespace CardGame
{
    public class GameLogic
    {
        /*
        public static async void StartGame(Player player)
        {
            bool play = true;
            PlayingCard card1 = await Program.CardAcess.GetCardAsync("card");
            //System.Console.WriteLine(card1);
            do{}while(card1 == null);
            while (play)
            {
                PlayingCard card2 = await Program.CardAcess.GetCardAsync("card");
                do{}while(card2 == null);
                System.Console.WriteLine(card2);
                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine(card1);
                Console.WriteLine("higer or lower card? \n 1. Higher \n 2. Lower");

                int option3 = Convert.ToInt32(Console.ReadKey().Key);
                Console.WriteLine("\n");
                Console.Clear();

                //http post input vraca bool - yes that right/ or no that wrong

                if (IsInputValid(option3))
                {
                    Console.WriteLine(card2);
                    bool result = await Program.CardAcess.ReadInputAsync();
                    if(result)
                    {
                        Console.WriteLine("That's right! You won a point.");
                        player.score += 1;
                        // card1 = card2;
                        Thread.Sleep(2000);
                    }
                    else
                    {
                        play = false;
                    }
                    
                    Console.Clear();
                } 
            }
        }

*/
        public static async Task<int> EndGame(Player player, bool newPlayer)
        {
            Console.WriteLine("The end of the game.");
            Console.WriteLine("Your score is: {0} of 52", player.score);

            if (newPlayer)
            {
                await Program.DataAcess.CreateNewPlayerAsync(player);
            }
            else
            {
                Player oldData = await Program.DataAcess.GetPlayerAsync($"player/{player.objectId}");
                if (oldData.score < player.score) { 
                    await Program.DataAcess.UpdatePlayerAsync(player);
                 }
            }

            int option4;
            do
            {
                Console.WriteLine("Do you want to continue? \n 1. New game \n 2. HighScore Table \n 3. Exit");
                option4 = Convert.ToInt32(Console.ReadKey().Key);
                Console.Clear();
            } while (!IsInputValid(option4));
            return option4;
        }
        

        private static bool IsInputValid(int input)
        {
            if (input == 49 || input == 50 || input == 51)
            {
                return true;
            }
            return false;
        }
    }
}
