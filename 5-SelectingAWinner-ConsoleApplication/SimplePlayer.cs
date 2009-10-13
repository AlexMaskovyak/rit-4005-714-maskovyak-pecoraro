using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using _1_Poker;

namespace _5_SelectingAWinner_ConsoleApplication
{
    /// <summary> simple player has a pre-determined index to select in a single round game with Referee. </summary>
    public class SimplePlayer : IView {

// fields

        /// <summary>Player's identifier.</summary>
        protected readonly int _playerNumber;

        /// <summary>Unchanging index to select.</summary>
        protected readonly int _indexToSelect;

// constructors

        /// <summary>Convenience constructor.</summary>
        public SimplePlayer(int playerNumber) : this(playerNumber, 0) { }

        /// <summary>Default constructor.</summary>
        /// <param name="indexToSelect">Cards from which to select.</param>
        public SimplePlayer(int playerNumber, int indexToSelect) {
            _playerNumber = playerNumber;
            _indexToSelect = indexToSelect;
        }

// IView implementation

        /// <summary> return <c>0..m-1</c>, index of chosen (and unexposed) card. </summary>
        public virtual int Choose() {
            Output(String.Format("Choosing {0}", _indexToSelect));
            return _indexToSelect;
        }

        /// <summary> find out about a chosen card. </summary>
        public virtual void Tell(int index, int suit, int value) {
            Output(String.Format("Told that {0} was selected with card {1}",
                index, new PlayingCard((PlayingCard.Ranks)value, (PlayingCard.Suits)suit)));
        }

        /// <summary> find out about a round's outcome. </summary>
        public virtual void Winner(bool yes) {
            Output((yes ? "Winner" : "Loser"));
        }

        /// <summary> return once view is ready for a new round. </summary>
        public virtual void Ready() {
            Output("Ready.");
            return; // always ready
        }

// output

        /// <summary> debug output. </summary>
        /// <param name="msg"> string to display. </param>
        protected virtual void Output(string msg) {
            Console.WriteLine("Player {0}: {1}", _playerNumber, msg);
        }

    }
}
