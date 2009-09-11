using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1_Poker
{
    /// <summary>
    /// Currently serves as a test for features.  Will eventually be what receives standard-in's series of cards
    /// to determine whose pokerhand is a winner.
    /// </summary>
    public class Driver
    {
        static void Main(string[] args)
        {
            PlayingCard c = new PlayingCard( PlayingCard.Value.Ace, PlayingCard.Suit.Club );
            System.Console.WriteLine(c);
            PokerHand hand = new PokerHand( c );

            string hands = "2H 3D 5S 9C KD 2C 3H 4S 8C AH"; // System.Console.ReadLine();
            while (hands != null)
            {
                string[] cards = hands.Split(' ');
                // take half of the cards and add them to one pokerhand
                // take the other half of the cards and add them to the other pokerhand

                // init the pokerhand ranker, have it create rank objects for both hands
                // use the overloaded comparison operator on the rank objects to determine the winner


                foreach( string card in cards ) 
                {
                    c = new PlayingCard(card[0], card[1]);
                    System.Console.WriteLine( c );
                }
                hands = null; // System.Console.ReadLine();
            }

            
        }
    }
}
