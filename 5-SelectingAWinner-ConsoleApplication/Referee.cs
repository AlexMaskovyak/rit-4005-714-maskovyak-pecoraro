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

        /// <summary> cell holding moves. </summary>
        protected Cell<SuitPrecedencePlayingCard> _moves;

        /// <summary> deck of cards. </summary>
        protected Deck _deck;

        /// <summary> number of cards. </summary>
        protected int _cards;




// constructors

        /// <summary> conevenience constructor. </summary>
        public Referee() : this( new Deck(), 5 ) { }

        /// <summary> convenience constructor. </summary>
        /// <param name="cards"> number of cards in a game. </param>
        public Referee(int cards) : this(new Deck(), cards) { }

        /// <summary> allows for a previously created Deck to be used.</summary>
        /// <param name="deck"> deck for this referee to use. </param>
        /// <param name="cards"> number of cards in a game. </param>
        public Referee(Deck deck, int cards) {
            _deck = deck;
            _deck.Shuffle();

            _cards = cards;
        }

// responsibilities



// tester
        
        /// <summary>Test for Referee</summary>
        /// <param name="args">[mCards dealt] i[1]-i[n] cards selected per player</param>
        public static void Main(string[] args)
        {
            // terminate early
            if (args == null || args.Count() < 3) {
                System.Console.Error.WriteLine("use: [cards in game] i[player 1 card index selected]-i[player n card index selected]");
                Environment.Exit(1);
            }

            int numberOfCards = int.Parse(args[0]);
            var playerIndexSelections = args.Skip(1);

            // create referee
            Referee referee = new Referee();


            List<IView> views = new List<IView>();

            foreach (var selection in playerIndexSelections)
            {
                holders.Add();
            }

            Del[] result = new Del[3];
            for (int i = 0; i < 3; ++i) {
                int x = i * 2 + 1;
                result[i] = delegate { return selection; };
            }

            foreach (Del d in result)
            {
                d();
            }
            
        }

        delegate void Del ();

        public interface Holder
        {
            int getNumber();
        }
    }
}
