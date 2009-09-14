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
            // Two Hands
            PokerHand black = new PokerHand();
            PokerHand white = new PokerHand();

            string input;
            //while ((input = Console.ReadLine()) != null) {

                // TODO: REMOVE THIS
                input = "2H 3D 5S 9C KD 2C 3H 4S AD AH"; // High Card, Pair
                //input = "2H 3D 4H 5D 6H 3C 4C 5C 6C 7C"; // Straight, Straight Flush
                //input = "2H 2D 3H 3D 4C AH AD TC TD TH"; // Two Pair, Full House (Correctly does 10)
                //input = "2H 2D 2C 4H 5H AH AD AC AS KD"; // 3Kind, 4Kind
                //input = "2H 4H 6H 8H TH 2D 4D 6D 8D TD"; // Flush (Tie)s

                // No More Input (hackish way to detect end of input??)
                //if (input.Length < 2) {
                //    break;
                //}

                // Parse and load Hands
                string[] cardStrings = input.Split(' ');
                for (int i = 0; i < PokerHand.MAX_HAND_SIZE; ++i) {
                    black.Add(new PlayingCard(cardStrings[i]));
                }
                for (int i = 0; i < PokerHand.MAX_HAND_SIZE; ++i) {
                    white.Add(new PlayingCard(cardStrings[i + PokerHand.MAX_HAND_SIZE]));
                }

                // Get the Score for Each Hand
                Score blackScore = black.ScoreHand();
                Score whiteScore = white.ScoreHand();

                Console.WriteLine("black score: " + blackScore.ToString());
                Console.WriteLine("white score: " + whiteScore.ToString());

                int compare = black.CompareTo(white);// blackScore.CompareTo(whiteScore);
                if (compare == 0) {
                    Console.WriteLine("Tie.");
                }
                else if (compare < 0) {
                    Console.WriteLine("White wins.");
                }
                else {
                    Console.WriteLine("Black wins.");
                }

            // TODO: REMOVE THIS
            //break;

            //}
        




            Console.WriteLine("Hello World!");

        }
    }
}
