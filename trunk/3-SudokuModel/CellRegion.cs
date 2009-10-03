using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3_SudokuModel {

    /// <summary>A collection of Cells.</summary>
    /// <remarks>CellRegion is typically a Row, Column, or Shape in a Sudoku Board.</remarks>
    public class CellRegion {

        /// <summary>Cells containing in this Region.</summary>
        protected HashSet<Cell> _cells;

        /// <summary>Default constructor.</summary>
        public CellRegion() {
            _cells = new HashSet<Cell>();
        }

        /// <summary>Accessor for the Cells this contains</summary>
        public virtual HashSet<Cell> Cells {
            get { return _cells; }
        }

        /// <summary>Check if this Region contains a cell.</summary>
        /// <param name="cell">The cell to check for.</param>
        /// <returns>True if contained, False otherwise.</returns>
        public virtual bool Contains(Cell cell) {
            return _cells.Contains(cell);
        }

        /// <summary>Add a cell to this region.</summary>
        /// <remarks>We maintain a set of cells. So adding the same cell twice will do nothing.</remarks>
        /// <param name="cell">Cell to add to this region.</param>
        public virtual void Add(Cell cell) {
            _cells.Add(cell);
        }

        /// <summary>Remove the cell from this region.</summary>
        /// <param name="cell">Cell to remove from this region.</param>
        public virtual void Remove(Cell cell) {
            _cells.Remove(cell);
        }
    }
}
