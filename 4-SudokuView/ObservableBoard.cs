using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BitArray = System.Collections.BitArray;

using _3_SudokuModel;
using _3_SudokuModelAdvanced;

namespace _4_SudokuView
{
    /// <summary>
    /// Extends Board to be observable.
    /// </summary>
    public class ObservableBoard : BoardAdvanced
    {
        
        /// <summary>Handler for a Set Event</summary>
        /// <param name="cell">The cell being set.</param>
        /// <param name="digit">The digit it was set to.</param>
        public delegate void OnSetHandler(int cell, int digit);

        /// <summary>Handler for a Possible Event</summary>
        /// <param name="cell">The cell with an update.</param>
        /// <param name="bits">The possible digits it can contain.</param>
        public delegate void OnPossibleHandler(int cell, BitArray bits);

        /// <summary>Fired when the model indicates a Set.</summary>
        public event OnSetHandler OnSet;

        /// <summary>Fired when the model indicates a change in a Possible Set.</summary>
        public event OnPossibleHandler OnPossible;


        /*
        /// <summary>Observers for this Board.</summary>
        protected event BoardUpdated _observers;

        /// <summary>Obtains the observers for this Board, allows for addition and removal.</summary>
        public virtual BoardUpdated Observers {
            get { return _observers; }
            set { _observers = value; }
        }
         * */

        /// <summary>Default constructor.</summary>
        /// <param name="rows">Rows describing how to construct the Sudoku board.</param>
        public ObservableBoard(string[] rows) : base(rows) { }


        /// <summary>Model indicates a Cell is set. Trigger a Set Event.</summary>
        /// <param name="cell">The cell being set.</param>
        /// <param name="digit">The digit it was set to.</param>
        public override void NowSet(int cell, int digit) {
            if (OnSet != null) {
                OnSet(cell, digit);
            }
        }

        /// <summary>Model indicates a Cell is set. Trigger a Possible Event.</summary>
        /// <param name="cell">The cell with an update.</param>
        /// <param name="bits">The possible digits it can contain.</param>
        public override void NowPossible(int cell, BitArray bits) {
            if (OnPossible != null) {
                OnPossible(cell, bits);
            }
        }

    }

}
