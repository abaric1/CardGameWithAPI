namespace CardGame._models
{
    public class Player
    {
        public string objectId { get; set; }
        public string name { get; set; }
        public int score { get; set; }
        public Player(string objectId, string name, int score) 
        { 
            this.objectId = objectId;
            this.name = name;
            this.score = score;
        }
    }
}

