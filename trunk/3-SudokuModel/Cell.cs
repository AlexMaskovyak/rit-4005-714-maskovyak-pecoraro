using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3_SudokuModel {

    /// <summary>Atomic elements of a Sudoku table.</summary>
    public class Cell {

        /// <summary>Constant Representation of All Values</summary>
        static int[] AllValues = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        /// <summary>Unique id</summary>
        protected int _id;

        /// <summary>List of possible values for this cell.</summary>
        protected int[] _values;

        /// <summary>Observable event when a Value is Changed.</summary>
        public event ValueAssigned _observers;

        /// <summary>Board to which this Cell belongs.</summary>
        public IBoard _board;

        /// <summary>Observable delegate for value-assigning events.</summary>
        /// <param name="cell"></param>
        public delegate void ValueAssigned(Cell cell);

        /// <summary>Accessor/mutator for a Cell's delegate.</summary>
        public virtual ValueAssigned Observers {
            get { return _observers; }
            set { _observers = value; }
        }

        /// <summary>Accessor/mutator for a Cell's Value.</summary>
        /// <remarks>Setting with a new value will notify the Listeners only if the value is a singleton.</remarks>
        public virtual int[] Values {
            get { return _values; }
            set {
                if (_values.Length != value.Length) {
                    _values = value;
                    if (_values.Length == 1) {
                        Notify();
                    }
                }
            }
        }

        /// <summary>Accessor for a Cell's id.</summary>
        public virtual int Id {
            get { return _id; }
        }

        public virtual IBoard Board {
            get { return _board;  }
            set { _board = value;  }
        }

        /// <summary>Default constructor.</summary>
        public Cell(int id, IBoard board): this(id, board, null) { }

        /// <summary>Constructor.</summary>
        /// <param name="values">Values for this cell to hold.</param>
        public Cell(int id, IBoard board, params int[] values) {
            _id = id;
            Board = board;
            _values = (values == null ? AllValues : values);
        }

        /// <summary>Notifies Observers of an event.</summary>
        public virtual void Notify() {
            _observers(this);
        }

        /// <summary>Respond to a Cell in this Cell's Context that was set to the given value</summary>
        /// <param name="value">The value that was set on another cell.</param>
        public virtual void RespondToSet(int value) {
            List<int> newValues = new List<int>(_values);
            newValues.Remove(value);
            Values = newValues.ToArray<int>();
        }
    }
}

