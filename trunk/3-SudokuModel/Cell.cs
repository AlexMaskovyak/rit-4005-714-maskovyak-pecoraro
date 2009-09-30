using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3_SudokuModel {

    /// <summary>Individual elements of a Sudoku table.</summary>
    public class Cell {

        /// <summary>Constant containing all the values allowed for a Cell</summary>
        protected readonly static int[] AllValues = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        /// <summary>Sudoku Board this Cell belongs too</summary>
        protected Board _board;

        /// <summary>Unique id</summary>
        protected int _id;

        /// <summary>List of possible values for this cell.</summary>
        protected int[] _values;

// Constructors

        /// <summary>Default constructor.</summary>
        public Cell(Board board, int id): this(board, id, null) { }

        /// <summary>Constructor.</summary>
        /// <param name="values">Values for this cell to hold.</param>
        public Cell(Board board, int id, params int[] values) {
            _id = id;
            _board = board;
            Values = (values == null ? AllValues : values);
        }

// Properties

        /// <summary>Accessor for the Cell's Singular Value.  null if more then one possible value.</summary>
        public virtual int Digit {
            get { return (Values.Length == 1 ? _values[0] : 0); }
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
        public virtual int[] Values {
            get { return _values; }
            set { _values = value; }
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
            _values = new int[] { value };
            foreach (Cell c in ContextCells) {
                c.RespondToSet(value);
            }
        }

        /// <summary>Respond to a Cell in this Cell's Context that was set to the given value</summary>
        /// <param name="value">The value that was set on another cell.</param>
        public virtual void RespondToSet(int value) {
            List<int> newValues = new List<int>(Values);
            newValues.Remove(value);
            Values = newValues.ToArray<int>();
        }
    }
}

