using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using BackendlessAPI;
using BackendlessAPI.Async;
using BackendlessAPI.Exception;
using CardGame._models;

namespace CardGame
{
    partial class Program
    {
        static HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {   
            using (client)
            {
                // spajanje na API
                client.BaseAddress = new Uri("http://localhost:5000/");
                // Player player = new Player("Petra", 5); - get who is the player
                // await CreateNewPlayerAsync(player);
                // await DataAcess.GetPlayersAsync("player");
                // await DataAcess.GetPlayerAsync("player/60F886EC-962B-4412-8754-C56929CF43BD");
                // player.score = 7;
                // player.objectId = "60F886EC-962B-4412-8754-C56929CF43BD";
                // await UpdatePlayerAsync(player);
                // await DeletePlayerAsync("60F886EC-962B-4412-8754-C56929CF43BD");

                // await GetCardAsync("card");
                int option1, option2;
                do
                {
                    Console.WriteLine("**WELCOME TO CARD GAME** \n Please, write number for option you want: \n 1. Start new game \n 2. HighScore Tabe \n 3. Exit");
                    option1 = Convert.ToInt32(Console.ReadKey().Key);
                    if (option1 == 51)
                    {
                        Environment.Exit(0);
                    }
                    if (option1 == 50)
                    {
                        Console.Clear();
                        await DataAcess.GetPlayersAsync("player");
                        do
                        {
                            Console.WriteLine("Press 1 to go back");
                            option2 = Convert.ToInt32(Console.ReadKey().Key);
                            Console.Clear();
                        } while (option2 != 49);
                    }
                    Console.Clear();
                } while (option1 != 49);

                Console.WriteLine("Enter your name");
                string name = Console.ReadLine();

                bool result = await DataAcess.InitCheckAsync($"player/init/{name}");

                bool newPlayer = true;
                 if (!result)
                {
                    bool valid = false;
                    while (!valid)
                    {
                        Console.WriteLine("Name is already taken. Do you want to continue as {0}?", name);
                        Console.WriteLine("1. Continue \n2. Pick another name");
                        int opt = Convert.ToInt32(Console.ReadKey().Key);
                        Console.Clear();
                        switch (opt)
                        {
                            case 49:
                                newPlayer = false;
                                valid = true;
                                break;
                            case 50:
                                Console.WriteLine("Enter new name");
                                name = Console.ReadLine();
                                valid = await DataAcess.InitCheckAsync($"player/init/{name}");
                                break;
                        }
                    }
                }

                Console.Clear();
                Console.WriteLine("Welcome {0}, game is starting...Good luck!", name);
                Thread.Sleep(3000);
                Console.Clear();
                
                PlayingCard card1 = await Program.CardAcess.GetCardAsync("card");
                bool play = true;
                do
                {
                    System.Console.WriteLine(card1);
                    Player player = new Player(name, 0);
                    // System.Console.WriteLine(card1);
                    Console.WriteLine("higer or lower card? \n 1. Higher \n 2. Lower");
                    int option3 = Convert.ToInt32(Console.ReadKey().Key);
                    PlayingCard card2 = await Program.CardAcess.GetCardAsync("card");
                    System.Console.WriteLine(card2);

                    var inputtt = new Input();
                    inputtt._input = option3;
                    inputtt._card1 = card1;
                    inputtt._card2 = card2;


                    bool resultt = await Program.CardAcess.ReadInputAsync(inputtt);
                    if(resultt)
                    {
                        Console.WriteLine("That's right! You won a point.");
                        player.score += 1;
                        // card1 = card2;
                        Thread.Sleep(2000);
                        card1 = card2;
                    }
                    else
                    {
                        Console.WriteLine("End of game");
                        play = false;
                    }

                    Console.Clear();
                    // GameLogic.StartGame(player);
                    //System.Console.WriteLine("End Game");
                    //int option4 = GameLogic.EndGame(player, newPlayer);
                    /*
                    switch (option4)

                    {
                        case 49:
                            player = new Player(name, 0);
                            newPlayer = false;
                            GameLogic.StartGame(player);
                            System.Console.WriteLine("End Game");
                            //GameLogic.EndGame(player, newPlayer);
                            Console.Clear();
                            break;
                        case 50:
                            database.SelectData();
                            Console.ReadKey();
                            Environment.Exit(0);
                            break;
                        case 51:
                            break;
                        default:
                            break;
                    }
                    */
                } while (play);          
            }
            Console.ReadLine();
        }  

    }
}
