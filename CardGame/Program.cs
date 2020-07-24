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

                int menuInput, goBackInput;
                do
                {
                    Console.WriteLine("WELCOME TO CARD GAME \n" +
                                      "Please, write number for option you want: \n" +
                                      "1. Start new game \n" +
                                      "2. HighScore Tabe \n" +
                                      "3. Exit");
                                 
                    menuInput = Convert.ToInt32(Console.ReadKey().Key);

                    if (menuInput == 51) 
                        Environment.Exit(0);

                    if (menuInput == 50)
                    {
                        Console.Clear();
                        await PlayerAcess.GetPlayersAsync("player");
                        do
                        {
                            Console.WriteLine("Press 1 to go back");
                            goBackInput = Convert.ToInt32(Console.ReadKey().Key);
                            Console.Clear();
                        } while (goBackInput != 49);
                    }
                    Console.Clear();
                } while (menuInput != 49);

                string name;
                do {
                    Console.WriteLine("Enter your name");
                    name = Console.ReadLine();
                    Console.Clear();
                } while (name == "");

                string objectId = await PlayerAcess.InitCheckAsync($"player/init/{name}");
                
                bool newPlayer = true;
                if (objectId != "")
                {
                    bool valid = false;
                    while (!valid)
                    {
                        Console.WriteLine("Name is already taken. Do you want to continue as {0}?", name);
                        Console.WriteLine("1. Continue \n" +
                                          "2. Pick another name");

                        int nameMenuInput = Convert.ToInt32(Console.ReadKey().Key);
                        Console.Clear();

                        switch (nameMenuInput)
                        {
                            case 49:
                                newPlayer = false;
                                valid = true;
                                break;
                            case 50:
                                Console.WriteLine("Enter new name");
                                name = Console.ReadLine();
                                string checkNewName = await PlayerAcess.InitCheckAsync($"player/init/{name}");
                                if(checkNewName == "")
                                    valid = true;
                                break;
                        }
                    }
                }

                Console.Clear();
                Console.WriteLine("Welcome {0}, game is starting...Good luck!", name);
                Thread.Sleep(3000);
                Console.Clear();
                
                int menuInputEnd = 49;
                while(menuInputEnd == 49)
                {
                    Player player = new Player(objectId, name, 0);
                    player = await Game.StartGame(player);
                    menuInputEnd = await Game.EndGame(player, newPlayer);
                }
                            
                switch (menuInputEnd)
                {
                    case 50:
                        await PlayerAcess.GetPlayersAsync("player");
                        Console.ReadKey();
                        Environment.Exit(0);
                        break;
                    case 51:
                        break;
                    default:
                        break;
                }

                Console.ReadLine();
            }
        }  
    }
}
