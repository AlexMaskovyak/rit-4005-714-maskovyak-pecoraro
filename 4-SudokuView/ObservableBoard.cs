using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using _3_SudokuModel;
using _3_SudokuModelAdvanced;

namespace _4_SudokuView
{
    /// <summary>
    /// Extends Board to be observable.
    /// </summary>
    public class ObservableBoard : BoardAdvanced
    {
        /// <summary>Delegate for board updates.</summary>
        /// <param name="sender">Object which is indicating it has updated.</param>
        public delegate void BoardUpdated(System.Object sender);

        /// <summary>Observers for this Board.</summary>
        protected event BoardUpdated _observers;

        /// <summary>Obtains the observers for this Board, allows for addition and removal.</summary>
        public virtual BoardUpdated Observers {
            get { return _observers; }
            set { _observers = value; }
        }

        /// <summary>Default constructor.</summary>
        /// <param name="rows">Rows describing how to construct the Sudoku board.</param>
        public ObservableBoard(string[] rows) : base(rows) { }

        /// <summary>Override of Set allowing for a cell's value to be set and notifying all observers that the board has been updated.</summary>
        /// <param name="cell">Index of the cell whose value it to be updated.</param>
        /// <param name="digit">Value to be stored in the cell.</param>
        public override void Set(int cell, int digit) {
            base.Set(cell, digit);
            Observers(this);
        }

        /// <summary>Override of Clear allowing for a cell's value to be cleared and notifying all observers that the board has been updated.</summary>
        /// <param name="cell">Index of the cell whose value is to be cleared.</param>
        public override void Clear(int cell) {
            base.Clear(cell);
            Observers(this);
        }
    }

}
