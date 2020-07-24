using System;
using System.Collections.Generic;

namespace CardGame._models
{
    public class Input
    {
        public PlayingCard Card1 { get; set; }
        public PlayingCard Card2 { get; set; }
        public int PlayerInput { get; set; }
        public Input(int playerInput, PlayingCard card1, PlayingCard card2)
        {
            PlayerInput = playerInput;
            Card1 = card1;
            Card2 = card2;
        }
    }
}