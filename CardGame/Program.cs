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
                // var database = new DataAcess();
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

                // get player
                // check name??

                // Player player = new Player("Petra", 5); - get who is the player
                // await CreateNewPlayerAsync(player);
                // await DataAcess.GetPlayersAsync("player");
                // await DataAcess.GetPlayerAsync("player/60F886EC-962B-4412-8754-C56929CF43BD");
                // player.score = 7;
                // player.objectId = "60F886EC-962B-4412-8754-C56929CF43BD";
                // await UpdatePlayerAsync(player);
                // await DeletePlayerAsync("60F886EC-962B-4412-8754-C56929CF43BD");

                // await GetCardAsync("card");

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
                /*
                bool play = true;
                do
                {
                    Player player = new Player(name, 0);

                    GameLogic.StartGame(player);
                    int option4 = GameLogic.EndGame(player, newPlayer);

                    switch (option4)
                    {
                        case 49:
                            player = new Player(name, 0);
                            newPlayer = false;
                            GameLogic.StartGame(player);
                            GameLogic.EndGame(player, newPlayer);
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
                } while (play);    

                */
            }
            Console.ReadLine();
        }  

        // podjeli 1. kartu
        // prihvati input posalji input
        // podjeli drugu kartu - ispisi rezultat
        // ....
        static async Task<PlayingCard> GetCardAsync(string path)
        {
            // PlayingCard card = new PlayingCard();
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                PlayingCard card = await response.Content.ReadAsAsync<PlayingCard>();
                System.Console.WriteLine(card);
                return card;    
            }
            throw new Exception("error");
        }

        static async Task<Uri> ReadInputAsync(int input) {
            
        var response = await client.PostAsJsonAsync("card", input);
        response.EnsureSuccessStatusCode();

        // return URI of the created resource
        return response.Headers.Location;
        }




    }
}
