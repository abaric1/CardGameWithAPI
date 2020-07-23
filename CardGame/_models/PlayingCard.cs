using System;

namespace CardGame._models
{
    public class PlayingCard
    {
        public Suit Suit { get; }
        public Value Value { get; }
        public PlayingCard(Suit suit, Value value)
        {
            Suit = suit;
            Value = value;
        }
        public override string ToString()
        {
            string symbol = GetSymbol(this.Suit);
            string result = string.Format("{0} {1}", (int)this.Value, symbol);
            return result;
        }

        private string GetSymbol(Suit suit)
        {
            return suit switch
            {
                Suit.Clubs => "\u2663",
                Suit.Spades => "\u2660",
                Suit.Diamonds => "\u2666",
                Suit.Hearts => "\u2665",
                _ => throw new NotImplementedException(),
            };
        }

    }
}