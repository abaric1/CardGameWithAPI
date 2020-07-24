using System;
using System.Collections.Generic;

namespace CardGame.API.Models
{
    public class Input
    {
        public PlayingCard _card1 { get; set; }
        public PlayingCard _card2 { get; set; }
        public int _input { get; set; }

        public Input(int input, PlayingCard card1, PlayingCard card2)
        {
            _input = input;
            _card1 = card1;
            _card2 = card2;
        }
    }
}