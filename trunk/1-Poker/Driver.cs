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
            bool debug = (args.Length > 0);

            // Two Hands
            PokerHand black = new PokerHand();
            PokerHand white = new PokerHand();

            string input;
            while ((input = Console.ReadLine()) != null) {
                
                // Debug - Some Test Cases and Expected Results
                //input = "2H 3D 5S 9C KD 2C 3H 4S AD AH"; // High Card, Pair
                //input = "2H 3D 4H 5D 6H 3C 4C 5C 6C 7C"; // Straight, Straight Flush
                //input = "2H 2D 3H 3D 4C AH AD TC TD TH"; // Two Pair, Full House (Correctly does 10)
                //input = "2H 2D 2C 4H 5H AH AD AC AS KD"; // 3Kind, 4Kind
                //input = "2H 4H 6H 8H TH 2D 4D 6D 8D TD"; // Flush (Tie)s
                //input = "2H 4D 6H 8D AH 3H 4C 6D 7C AD"; // Both Ace High, Black has Better Kickers
                //input = "2H 2D 4H 4D AH 2S 2C 4S 4C AC"; // Two Pair Real Tie
                if (debug) { Console.WriteLine(input); }

                // Clear the Hands
                black.Clear();
                white.Clear();

                // Parse and load Hands
                try {
                    string[] cardStrings = input.Split(' ');
                    for (int i = 0; i < PokerHand.MAX_HAND_SIZE; ++i) {
                        black.Add(new PlayingCard(cardStrings[i]));
                    }
                    for (int i = 0; i < PokerHand.MAX_HAND_SIZE; ++i) {
                        white.Add(new PlayingCard(cardStrings[i + PokerHand.MAX_HAND_SIZE]));
                    }
                } catch (Exception e) {
                    Console.WriteLine("Bad Card in the Mix");
                    if (debug) { Console.WriteLine(e.StackTrace); }
                    continue;
                }

                // Debug - Output the Scores
                if (debug) {
                    Console.WriteLine("black score: " + black.ScoreHand().ToString());
                    Console.WriteLine("white score: " + white.ScoreHand().ToString());
                }

                int compare = black.CompareTo(white);
                if (compare == 0) {
                    Console.WriteLine("Tie.");
                } else if (compare < 0) {
                    Console.WriteLine("White wins.");
                } else {
                    Console.WriteLine("Black wins.");
                }

            }
        }
    }
}
