using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _5_SelectingAWinner_ConsoleApplication
{
    /// <summary>Players play some manner of game.</summary>
    public class Player : IView
    {

// constructors
        /// <summary>Default constructor.</summary>
        public Player() {

        }

// IView implementation

        /// <summary> return <c>0..m-1</c>, index of chosen (and unexposed) card. </summary>
        public int Choose() {
            throw new NotImplementedException();
        }

        /// <summary> find out about a chosen card. </summary>
        public void Tell(int index, int suit, int value) {
            throw new NotImplementedException();
        }

        /// <summary> find out about a round's outcome. </summary>
        public void Winner(bool yes) {
            throw new NotImplementedException();
        }

        /// <summary> return once view is ready for a new round. </summary>
        public void Ready() {
            throw new NotImplementedException();
        }
    }
}
