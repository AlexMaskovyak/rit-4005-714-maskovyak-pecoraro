using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _5_SelectingAWinner_ConsoleApplication
{
    /// <summary> simple player has a pre-determined index to select in a single round game with Referee. </summary>
    public class SimplePlayer : IView {

// fields

        /// <summary>Unchanging index to select.</summary>
        protected readonly int _indexToSelect;

// constructors

        /// <summary>Convenience constructor.</summary>
        public SimplePlayer() : this(0) { }

        /// <summary>Default constructor.</summary>
        /// <param name="indexToSelect">Cards from which to select.</param>
        public SimplePlayer(int indexToSelect) {
            _indexToSelect = indexToSelect;
        }

// IView implementation

        /// <summary> return <c>0..m-1</c>, index of chosen (and unexposed) card. </summary>
        public int Choose() {
            return _indexToSelect;
        }

        /// <summary> find out about a chosen card. </summary>
        public void Tell(int index, int suit, int value) {
            Console.WriteLine("I was told: {0} {1} {2}", index, suit, value);
        }

        /// <summary> find out about a round's outcome. </summary>
        public void Winner(bool yes) {
            Console.WriteLine((yes) ? "Winner" : "Loser");
        }

        /// <summary> return once view is ready for a new round. </summary>
        public void Ready() {
            return;
        }
    }
}
