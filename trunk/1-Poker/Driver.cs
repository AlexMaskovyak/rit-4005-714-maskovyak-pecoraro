using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1_Poker
{
    public class Driver
    {
        static void Main(string[] args)
        {
            PlayingCard c = new PlayingCard( PlayingCard.Rank.Ace, PlayingCard.Suit.Club );
            System.Console.WriteLine(c);
            PokerHand hand = new PokerHand( c, c, c, c, c );

            string hands = "2H 3D 5S 9C KD 2C 3H 4S 8C AH"; // System.Console.ReadLine();
            while (hands != null)
            {
                string[] cards = hands.Split(' ');
                foreach( string card in cards ) 
                {
                    System.Console.WriteLine( card );
                }
                hands = System.Console.ReadLine();
            }

            
        }
    }
}
