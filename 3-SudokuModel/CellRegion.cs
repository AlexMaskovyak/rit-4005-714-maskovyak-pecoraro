using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3_SudokuModel {
    /// <summary>A collection of Cells.</summary>
    /// <remarks>CellRegion monitors a group of cells for value change events for a game of Sudoku.</remarks>
    public class CellRegion {
        private string _name;
        private HashSet<Cell> _cells;
        private HashSet<int> _valuesHeld;

        /// <summary>Default constructor.</summary>
        /// <param name="name">Identity of this CellRegion...GET RID OF THIS AFTER TESTING.</param>
        public CellRegion(string name) {
            _name = name;
            _cells = new HashSet<Cell>();
            _valuesHeld = new HashSet<int>();
        }

        /// <summary>Accessor for the Cells this contains</summary>
        public HashSet<Cell> Cells {
            get { return _cells; }
        }

        /// <summary>Check if this Region contains a cell.</summary>
        /// <param name="cell">The cell to check for.</param>
        /// <returns>True if contained, False otherwise.</returns>
        public virtual bool Contains(Cell cell) {
            return _cells.Contains(cell);
        }

        /// <summary>Add a cell to this region.</summary>
        /// <param name="cell">Cell to add to this region and begin observing.</param>
        public virtual void Add(Cell cell) {
            _cells.Add(cell);
            Observe(cell);
        }

        /// <summary>Remove the cell from this region.</summary>
        /// <param name="cell">Cell to remove from this region and which we should halt observing.</param>
        public virtual void Remove(Cell cell) {
            _cells.Remove(cell);
            StopObserving(cell);
        }

        /// <summary>Registers this CellRegion's updating method to be fired when the specified cell indicates it has an assignment event.</summary>
        /// <param name="cell">Cell to observe.</param>
        public virtual void Observe(Cell cell) {
            cell.Observers += new Cell.ValueAssigned(this.Update);
        }

        /// <summary>De-registers this CellRegion's updating method from the specified cell's delegate.</summary>
        /// <param name="cell">Cell to stop observing.</param>
        public void StopObserving(Cell cell) {
            cell.Observers -= this.Update;
        }


        /// <summary>Updates the state of Cells in this region.</summary>
        /// <param name="cell">Initial cell whose value has changed.</param>
        public void Update(Cell cell) {

            int[] i = cell.Values;

            Console.WriteLine(String.Format("{0} has found cell has: {1}", _name, cell.Values[0]));
        }



        public static void Main() {
            Cell cell = new Cell(1);
            CellRegion column = new CellRegion("column");
            CellRegion row = new CellRegion("row");
            CellRegion shape = new CellRegion("shape");
            column.Observe(cell);
            row.Observe(cell);
            shape.Observe(cell);
            cell.Values = new int[] { 2 };
            column.StopObserving(cell);
            cell.Values = new int[] { 3 };
        }
    }
}
