using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3_SudokuModel {

    /// <summary>Atomic elements of a Sudoku table.</summary>
    public class Cell {

        /// <summary>Unique id</summary>
        protected int _id;

        /// <summary>List of possible values for this cell.</summary>
        protected int[] _values;

        /// <summary>Observable event when a Value is Changed.</summary>
        public event ValueAssigned _observers;

        /// <summary>Observable delegate for value-assigning events.</summary>
        /// <param name="cell"></param>
        public delegate void ValueAssigned(Cell cell);

        /// <summary>Accessor/mutator for a Cell's delegate.</summary>
        public virtual ValueAssigned Observers {
            get { return _observers; }
            set { _observers = value; }
        }

        /// <summary>Accessor/mutator for a Cell's Value.</summary>
        /// <remarks>Setting with a new value will notify the Listeners.</remarks>
        public virtual int[] Values {
            get { return _values; }
            set {
                if (_values == null || _values.Length != value.Length) {
                    _values = value;
                    Notify();
                }
            }
        }

        /// <summary>Accessor for a Cell's id.</summary>
        public virtual int Id {
            get { return _id; }
        }

        /// <summary>Default constructor.</summary>
        public Cell(int id): this(id, null) { }

        /// <summary>Constructor.</summary>
        /// <param name="values">Values for this cell to hold.</param>
        public Cell(int id, params int[] values) {
            _id = id;
            _values = (values == null ? new int[] {1,2,3,4,5,6,7,8,9} : values);
        }

        /// <summary>Notifies Observers of an event.</summary>
        public virtual void Notify() {
            _observers(this);
        }

// TODO: Document

        public virtual void RespondToSet(int value) {
            List<int> newValues = new List<int>();
            foreach (int i in _values) {
                if (i != value) {
                    newValues.Add(i);
                }
            }

            Values = newValues.ToArray<int>();
        }
    }
}

