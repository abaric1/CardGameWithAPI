using System;
using System.Collections.Generic;

namespace CardGame._models
{
    class Pack
    {
        private const int NumSuits = 4;
        private const int CardsPerSuit = 13;
        private readonly Random randomCardSelector = new Random();
        private readonly List<PlayingCard> cards = new List<PlayingCard>(CardsPerSuit);

        // set initial pack
        public Pack()
        {
            for(Suit suit = Suit.Clubs; suit <= Suit.Spades; suit++)
            {
                for(Value value = Value.Ace; value <= Value.King; value++)
                {
                    cards.Add(new PlayingCard(suit, value));
                }
            }
        }

        // deal random card
        public PlayingCard DealCardFromPack()
        {

            Suit suit = (Suit)randomCardSelector.Next(NumSuits);
            Value value = (Value)randomCardSelector.Next(CardsPerSuit);
            while (IsCardAlreadyDeal(suit, value))
            {
                suit = (Suit)randomCardSelector.Next(NumSuits);
                value = (Value)randomCardSelector.Next(CardsPerSuit);
            }

            PlayingCard card = cards.Find(c => c.Value == value && c.Suit == suit);
            cards.Remove(card);
            return card;
        }

        public bool IsListEmpty()
        {
            if(cards.Count > 0)
            {
                return true;
            }
            else
            {
                Console.WriteLine("There is no more cards.");
                return false;
            }
        }
        
        private bool IsCardAlreadyDeal(Suit suit, Value value)
        {
            return (!cards.Exists(c => c.Value == value && c.Suit == suit));
        }
        
    }
}
