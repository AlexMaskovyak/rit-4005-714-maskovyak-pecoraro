using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poker {
    class Driver {
        static void Main(string[] args) {

            // Two Hands
            Hand black = new Hand();
            Hand white = new Hand();

            string input;
            //while ((input = Console.ReadLine()) != null) {

                // TODO: REMOVE THIS
                input = "2H 3D 5S 9C KD 2C 3H 4S AD AH"; // High Card, Pair
                input = "2H 3D 4H 5D 6H 3C 4C 5C 6C 7C"; // Straight, Straight Flush
                input = "2H 2D 3H 3D 4C AH AD TC TD TH"; // Two Pair, Full House (Correctly does 10)
                input = "2H 2D 2C 4H 5H AH AD AC AS KD"; // 3Kind, 4Kind
                input = "2H 4H 6H 8H TH 2D 4D 6D 8D TD"; // Flush (Tie)s

                // No More Input (hackish way to detect end of input??)
                //if (input.Length < 2) {
                //    break;
                //}

                // Parse and load Hands
                string[] cardStrings = input.Split(' ');
                for (int i = 0; i < Hand.Limit; ++i) {
                    black.addCard(new Card(cardStrings[i]));
                }
                for (int i = 0; i < Hand.Limit; ++i) {
                    white.addCard(new Card(cardStrings[i+Hand.Limit]));
                }
                
                // Get the Score for Each Hand
                Score blackScore = black.Score;
                Score whiteScore = white.Score;

                Console.WriteLine(blackScore.ToString());
                Console.WriteLine(whiteScore.ToString());

                int compare = blackScore.CompareTo(whiteScore);
                if (compare == 0) {
                    Console.WriteLine("Tie.");
                } else if (compare < 0) {
                    Console.WriteLine("White wins.");
                } else {
                    Console.WriteLine("Black wins.");
                }

                // TODO: REMOVE THIS
                //break;

            //}

            
            


            Console.WriteLine("Hello World!");
        }



    }
}
