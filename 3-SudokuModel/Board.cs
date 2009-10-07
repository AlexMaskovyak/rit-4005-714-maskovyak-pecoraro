using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BitArray = System.Collections.BitArray;

namespace _3_SudokuModel {
    /// <summary>Implementation of the IBoard interface.  Holds the state of a Sudoku board.</summary>
    public class Board : IBoard
    {
        /// <summary>Object to lock against.</summary>
        protected Object _locker = new Object();

        /// <summary>Monitors for rows</summary>
        protected CellRegion[] _rowRegions;

        /// <summary>Monitors for columns</summary>
        protected CellRegion[] _columnRegions;

        /// <summary>Monitors for shapes</summary>
        protected CellRegion[] _shapeRegions;

        /// <summary>Total list of cells</summary>
        protected Cell[] _cells;

        /// <summary>The default board is assumed to be square. This is the dimension size.</summary>
        protected int _dimension;

        /// <summary>Default constructor.</summary>
        /// <param name="rows">Board format as strings.</param>
        public Board(string[] rows) {
            ParseRows(rows);
        }

        /// <summary>Parse the Rows provided to the Constructor</summary>
        /// <remarks>
        ///     This is outside of the constructor, and declared virtual because it is expected
        ///     that you can subclass Board and override this in order to Parse different input
        ///     without needing to change anything else or put it in a Constructor.
        ///     
        ///     This version takes a String array in the format shown below.  Each line specifies
        ///     a "row" of a Sudoku board.  Numbers specify a "shape":
        ///     
        ///     <example>
        ///     111122233
        ///     111122233
        ///     441225333
        ///     444425533
        ///     444555666
        ///     775586666
        ///     777588966
        ///     778889999
        ///     778889999
        ///     </example>
        ///     
        ///     This does no currently check to ensure that the dimension restriction is met by
        ///     all cell containing regions (rows, columns, and shapes).
        /// </remarks>
        /// <param name="rows">Board format as strings</param>
        protected virtual void ParseRows(string[] rows) {
            int dimension = rows.Length;

            // assign protected members
            _dimension = dimension;
            _rowRegions = new CellRegion[dimension];
            _columnRegions = new CellRegion[dimension];
            _shapeRegions = new CellRegion[dimension];
            _cells = new Cell[dimension * dimension];

            // create regions to contain cells
            for (int i = 0; i < dimension; ++i) {
                _rowRegions[i] = new CellRegion();
                _columnRegions[i] = new CellRegion();
                _shapeRegions[i] = new CellRegion();
            }

            // go through all rows and assign cells to their regions
            for (int row = 0; row < dimension; ++row) {
                for (int col = 0; col < dimension; ++col) {
                    int id = col + (row * dimension);
                    int shapeId = int.Parse(rows[row][col].ToString()) - 1;
                    Cell c = CreateCell(id);
                    _cells[id] = c;
                    _rowRegions[row].Add(c);
                    _columnRegions[col].Add(c);
                    _shapeRegions[shapeId].Add(c);
                }
            }
        }

        /// <summary> template method: digit is set into cell.</summary>
        /// <param name="cell">The cell index of the cell that is now set.</param>
        /// <param name="digit">The new value of the cell.</param>
        public virtual void NowSet(int cell, int digit) {}

        /// <summary> template method: candidate set is set into cell.</summary>
        /// <param name="cell">The cell index of the cell that changed.</param>
        /// <param name="digit">The possible values.</param>
        public virtual void NowPossible(int cell, BitArray digit) {}

        /// <summary>Factory Method for Creating cells</summary>
        /// <param name="id">The Cell's id</param>
        /// <returns>A new cell</returns>
        public virtual Cell CreateCell(int id) {
            return new Cell(this, id);
        }

        /// <summary>Set the Digit of a Cell</summary>
        /// <param name="cell">The id of the cell to set.</param>
        /// <param name="digit">The value to set it.</param>
        public virtual void Set(int cell, int digit) {
            lock (_locker) {
                _cells[cell].Set(digit);
            }
        }

        /// <summary>List of cell ids for cells in the same row as the provided cell.</summary>
        /// <param name="cell">The cell id to find others in the same row.</param>
        /// <returns>The cell ids of all other cells in the same row.</returns>
        public virtual IEnumerable<int> Row(int cell) {
            CellRegion region = GetRowRegionForCell(cell);
            return EnumerateIds(region.Cells, cell);
        }

        /// <summary>List of cell ids for cells in the same column as the provided cell.</summary>
        /// <param name="cell">The cell id to find others in the same column.</param>
        /// <returns>The cell ids of all other cells in the same column.</returns>
        public virtual IEnumerable<int> Column(int cell) {
            CellRegion region = GetColumnRegionForCell(cell);
            return EnumerateIds(region.Cells, cell);
        }

        /// <summary>List of cell ids for cells in the same shape as the provided cell.</summary>
        /// <param name="cell">The cell id to find others in the same shape.</param>
        /// <returns>The cell ids of all other cells in the same shape.</returns>
        public virtual IEnumerable<int> Shape(int cell) {
            CellRegion region = GetShapeRegionForCell(cell);
            return EnumerateIds(region.Cells, cell);
        }

        /// <summary>List of cell ids for cells in the same context as the provided cell.</summary>
        /// <remarks>The "context" is the union of the Row, Column, and Shape for the provided cell.</remarks>
        /// <param name="cell">The cell id to find others in the same context.</param>
        /// <returns>The cell ids of all other cells in the same context.</returns>
        public virtual IEnumerable<int> Context(int cell) {
            HashSet<Cell> cells = new HashSet<Cell>();
            cells.UnionWith(GetRowRegionForCell(cell).Cells);
            cells.UnionWith(GetColumnRegionForCell(cell).Cells);
            cells.UnionWith(GetShapeRegionForCell(cell).Cells);
            return EnumerateIds(cells, cell);
        }

        /// <summary>IEnumerable of ids for an IEnumerable of cells</summary>
        /// <param name="cells">The list of cells</param>
        /// <param name="without">Cell to be excluded from enumeration.</param>
        /// <returns>A list of ids</returns>
        private IEnumerable<int> EnumerateIds(IEnumerable<Cell> cells, int without) {
            foreach (Cell c in cells) {
                if (c.Id != without) {
                    yield return c.Id;
                }
            }
        }

        /// <summary>Get the Context as Cells instead of Ids</summary>
        /// <param name="cell">The cell id to find the others in the same context.</param>
        /// <returns>The other cells in the same context.</returns>
        public virtual IEnumerable<Cell> ContextCells(int cell) {
            foreach (int id in Context(cell)) {
                yield return _cells[id];
            }
        }

        /// <summary>Get the CellRegion of the Row for a cell id</summary>
        /// <param name="cell">The cell id.</param>
        /// <returns>The CellRegion representing that Row.</returns>
        protected virtual CellRegion GetRowRegionForCell(int cell) {
            // return getRegionContainingCell(_cells[cell], _rowRegions); // Generic
            return _rowRegions[RowIndexForCell(cell)];
        }

        /// <summary>Get the CellRegion of the Column for a cell id</summary>
        /// <param name="cell">The cell id.</param>
        /// <returns>The CellRegion representing that Column.</returns>
        protected virtual CellRegion GetColumnRegionForCell(int cell) {
            // return getRegionContainingCell(_cells[cell], _columnRegions); // Generic
            return _columnRegions[ColumnIndexForCell(cell)];
        }

        /// <summary>Get the CellRegion of the Shape for a cell id</summary>
        /// <param name="cell">The cell id.</param>
        /// <returns>The CellRegion representing that Shape.</returns>
        protected virtual CellRegion GetShapeRegionForCell(int cell) {
            return GetRegionContainingCell(_cells[cell], _shapeRegions);
        }

        /// <summary>A generic way to find a cell in a list of CellRegions</summary>
        /// <param name="cell">The cell to find.</param>
        /// <param name="regions">The regions to search.</param>
        /// <returns>The region containing the cell, null if none of the regions.</returns>
        protected virtual CellRegion GetRegionContainingCell(Cell cell, CellRegion[] regions) {
            foreach (CellRegion region in regions) {
                if (region.Contains(cell)) {
                    return region;
                }
            }
            return null;
        }

        /// <summary>Calculate the row for a cell</summary>
        /// <param name="cell">The cell id.</param>
        /// <returns>The row index (0-indexed)</returns>
        protected virtual int RowIndexForCell(int cell) {
            return (cell / _dimension);
        }

        /// <summary>Calculate the column for a cell</summary>
        /// <param name="cell">The cell id.</param>
        /// <returns>The column index (0-indexed)</returns>
        protected virtual int ColumnIndexForCell(int cell) {
            return (cell % _dimension);
        }

        /// <summary>Calculate the shape for a cell</summary>
        /// <param name="cell">The cell id.</param>
        /// <returns>The shape index (0-indexed), -1 if not found</returns>
        protected virtual int ShapeIndexForCell(int cell) {
            Cell c = _cells[cell];
            for (int i = 0; i < _shapeRegions.Length; ++i) {
                CellRegion region = _shapeRegions[i];
                if (region.Contains(c)) {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>Get the Information about Shapes</summary>
        /// <returns>Lists of Lists containing Shape Cells</returns>
        public virtual List<List<int>> GetShapes() {
            List<List<int>> shapes = new List<List<int>>();
            foreach (CellRegion region in _shapeRegions) {
                List<int> cellids = new List<int>();
                foreach (Cell c in region.Cells) {
                    cellids.Add(c.Id);
                }
                shapes.Add(cellids);
            }

            return shapes;
        }



        /// <summary>Convenience method to obtain the potential assignable values for a cell at the specified cell index.</summary>
        /// <remarks>Used for debuggin.</remarks>
        /// <param name="cellIndex">Index of the cell whose values are to be retrieved.</param>
        /// <returns>Array of potential values this cell can be assigned.</returns>
        public int[] GetPotentialCellValues(int cellIndex) {
            return _cells[cellIndex].Values.ToArray<int>();
        }

        
        /*public void Debug() {
            for (int i = 0; i < _cells.Length; ++i) {
                if ((i % _dimension) == 0) {
                    Console.WriteLine();
                }
                Cell c = _cells[i];
                if (c.Digit != 0) {
                    Console.Write(c.Digit + " ");
                }
                else {
                    Console.Write("- ");
                }
            }
        }*/
    }
}
