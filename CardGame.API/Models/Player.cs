namespace CardGame.API.Models
{
    public class Player
    {
        public string objectId { get; set; }
        public string name { get; set; }
        public int score { get; set; }
        public Player() { } // mora biti parametless ctor zbog backendlessa
        public Player(string name, int score) 
        { 
            this.name = name;
            this.score = score;
        }
    }
}
