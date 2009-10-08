using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using _1_Poker;
using _2_PokerPuzzle;

namespace _5_SelectingAWinner_ConsoleApplication
{
    /// <summary>Runs one or more rounds of the Random selection game.</summary>
    public class Referee
    {

// fields

        /// <summary> deck of cards.</summary>
        protected Deck _deck;

        protected Cell

// constructors

        /// <summary> conevenience constructor.</summary>
        public Referee() : this( new Deck() ) { }

        /// <summary> allows for a previously created Deck to be used.</summary>
        /// <param name="deck">Deck for this referee to use.</param>
        public Referee(Deck deck) {
            _deck = deck;
            _deck.Shuffle();
        }

// tester

        /// <summary>Test for Referee</summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            // terminate early
            if (args == null || args.Count != 3) {
                System.Console.Error.WriteLine("use: [number of players] [number of cards] [url for card images]");
                Environment.Exit(1);
            }

            int numberOfPlayers = int.Parse(args[0]);
            int numberOfCards = int.Parse(args[1]);
            string urlOfCardsImages = args[2];

            // create referee
            Referee referee = new Referee();

            // create n views


        }
    }
}
