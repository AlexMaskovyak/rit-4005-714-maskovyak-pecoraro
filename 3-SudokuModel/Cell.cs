using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3_SudokuModel {

    /// <summary>Atomic elements of a Sudoku table.</summary>
    public class Cell {

        protected int _id;
        protected int[] _values;
        protected event ValueAssigned _observers;

        /// <summary>Observable delegate for value-assigning events.</summary>
        /// <param name="cell"></param>
        public delegate void ValueAssigned(Cell cell);

        /// <summary>Accessor/mutator for a Cell's delegate.</summary>
        public virtual ValueAssigned Observers {
            get { return _observers; }
            set { _observers = value; }
        }

        /// <summary>Accessor/mutator for a Cell's Value.</summary>
        public virtual int[] Values {
            get { return _values; }
            set { _values = value; Notify(); } //TODO: Don't notify if no change.
        }

        /// <summary>Accessor for a Cell's id.</summary>
        public virtual int Id {
            get { return _id; }
        }

        /// <summary>Default constructor.</summary>
        public Cell(int id) {
            _id = id;
        }

        /// <summary>Constructor.</summary>
        /// <param name="values">Values for this cell to hold.</param>
        public Cell(int id, params int[] values) {
            _id = id;
            Values = values;
        }

        /// <summary>Notifies Observers of an event.</summary>
        public virtual void Notify() {
            _observers(this);
        }
    }
}

