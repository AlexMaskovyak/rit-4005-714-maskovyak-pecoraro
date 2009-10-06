using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BitArray = System.Collections.BitArray;

namespace _3_SudokuModel {

    /// <summary>Individual elements of a Sudoku table.</summary>
    public class Cell {

        /// <summary>Sudoku Board this Cell belongs too</summary>
        protected Board _board;

        /// <summary>Unique id</summary>
        protected int _id;

        /// <summary>List of possible values for this cell.</summary>
        protected List<int> _values;

// Constructors

        /// <summary>Default constructor.</summary>
        public Cell(Board board, int id): this(board, id, null) { }

        /// <summary>Constructor.</summary>
        /// <param name="values">Values for this cell to hold.</param>
        public Cell(Board board, int id, params int[] values) {
            _id = id;
            _board = board;
            if (values == null)
                Values = new List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            else
                Values = new List<int>(values);
        }

// Properties

        /// <summary>Accessor for the Cell's Singular Value.  null if more then one possible value.</summary>
        public virtual int? Digit {
            get { return (Values.Count == 1 ? _values[0] : (int?)null); }
        }

        /// <summary>Accessor for a Cell's id.</summary>
        public virtual int Id {
            get { return _id; }
        }

        /// <summary>Accessor for the board a Cell is in</summary>
        public virtual Board Board {
            get { return _board; }
        }

        /// <summary>Accessor for the ContextCells for this Cell</summary>
        public virtual IEnumerable<Cell> ContextCells {
            get { return _board.ContextCells(_id); }
        }

        /// <summary>Accessor/mutator for a Cell's Potential Values.</summary>
        public virtual List<int> Values {
            get { return _values; }
            set { _values = value; _values.Sort(); }
        }

// Public Methods

        /// <summary>Check if this Cell can be a Particular Value</summary>
        /// <param name="value">The value to check</param>
        /// <returns>True if it can be, False otherwise.</returns>
        public virtual bool CanBe(int value) {
            foreach (int x in Values) {
                if (x == value) {
                    return true;
                }
            }

            return false;
        }

        /// <summary>Set the Value of this Cell</summary>
        /// <remarks>Update all other Cells in this Cell's context.</remarks>
        /// <param name="value">The New Value to set</param>
        public virtual void Set(int value) {
            _values = new List<int>(1);
            _values.Add(value);
            _board.NowSet(Id, value);
            foreach (Cell c in ContextCells) {
                c.RespondToSet(value);
            }
        }

        /// <summary>Respond to a Cell in this Cell's Context that was set to the given value</summary>
        /// <param name="value">The value that was set on another cell.</param>
        public virtual void RespondToSet(int value) {
            if (_values.Remove(value)) {
                _board.NowPossible(Id, ValuesToBitArray(_values));
            }
        }

        /// <summary>Convert Values into a BitArray</summary>
        /// <param name="values">Potential Int Values</param>
        /// <returns></returns>
        protected virtual BitArray ValuesToBitArray(List<int> values) {
            BitArray bits = new BitArray(10); // sudoku size
            foreach (int i in values) {
                if (i < bits.Length) { // potential error if greater.
                    bits[i] = true;
                }
            }
            return bits;
        }
    }
}

