using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using _1_Poker;
using _2_PokerPuzzle;

namespace _5_SelectingAWinner_ConsoleApplication
{
    /// <summary> implements IReferee for use with IView and random card selection game. </summary>
    public class Referee : AbstractReferee<IView>
    {

// constructors 

        /// <summary> conevenience constructor. </summary>
        public Referee() : this( 5 ) { }

        /// <summary> convenience constructor. </summary>
        /// <param name="cards"> number of cards in a game. </param>
        public Referee(int cards) : this( cards, 2 ) { }

        /// <summary> allows for a previously created Deck to be used.</summary>
        /// <param name="cards"> number of cards in a game. </param>
        /// <param name="maxPlayers"> maximum number of players to allow for a game. </param>
        public Referee(int cards, int maxPlayers) : base(cards, maxPlayers, (int)DateTime.Now.Ticks) { }

// methods

        /// <summary> provides main logic for holding a game. </summary>
        protected override void GameLoop() {
            // shuffle the cards and select the first m cards
            List<PlayingCard> cards = 
                _deck.Shuffle().Take(_cards).ToList<PlayingCard>();

            // ensure that all players are ready
            foreach(IView player in Players()) {
                player.Ready();
            }

            // obtain every player's chosen card index
            // tell all players which card was selected
            foreach(IView player in Players()) {
                int index = player.Choose();
                // ensure it is in range
                if( index < 0 || index > cards.Count ) {
                    throw new IndexOutOfRangeException(
                        String.Format(
                            "A card was selected outside of the range of accepted values: 0 through {1}", cards.Count ) );
                }
                // ensure that it hasn't been selected


                // tell everyone the result
                //player.Tell(index
            }

            foreach(IView player in Players())
            {
                //player.Winner();
            }
            
            throw new NotImplementedException();
        }

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
            IReferee<IView> referee = new Referee(numberOfCards, playerIndexSelections.Count());

            // create players
            foreach( var selection in playerIndexSelections ) {
                referee.Join(new Player());
            }
        }
    }
}
