using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _5_SelectingAWinner_ConsoleApplication
{

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

// functionality

        /// <summary> provides main logic for holding a game. </summary>
        protected override void GameLoop() {
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
