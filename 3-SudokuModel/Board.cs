using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3_SudokuModel {
    /// <summary>Implementation of the IBoard interface.  Holds the state of a Sudoku board.</summary>
    public class Board : IBoard {

        /// <summary>Monitors for rows</summary>
        protected CellRegion[] _rowRegions;

        /// <summary>Monitors for columns</summary>
        protected CellRegion[] _columnRegions;

        /// <summary>Monitors for shapes</summary>
        protected CellRegion[] _shapeRegions;

        /// <summary>Total list of cells</summary>
        protected Cell[] _cells;

        protected int _dimension;

        /// <summary>Default constructor.</summary>
        /// <param name="rows">Board format as strings.</param>
        public Board(string[] rows) {
            ParseRows(rows);
        }

        /// <summary>Parse the Rows provided to the Constructor</summary>
        /// <remarks>
        ///     This is outside of the constructor, and declared virtual because it is expected
        ///     that you can subclass Board and override this in order to Parse difference input
        ///     without needing to change anything else.
        ///     
        ///     This version takes a String array in the format shown below.  Each line specifies
        ///     a "row" of a Sudoku board.  Numbers specify a "shape":
        ///     
        ///     <code>
        ///     111122233
        ///     111122233
        ///     441225333
        ///     444425533
        ///     444555666
        ///     775586666
        ///     777588966
        ///     778889999
        ///     778889999
        ///     </code>
        /// </remarks>
        /// <param name="rows">Board format as strings</param>
        protected virtual void ParseRows(string[] rows) {
            int dimension = rows.Length;

            _dimension = dimension;
            _rowRegions = new CellRegion[dimension];
            _columnRegions = new CellRegion[dimension];
            _shapeRegions = new CellRegion[dimension];
            _cells = new Cell[dimension * dimension];

            for (int i = 0; i < dimension; ++i) {
                _rowRegions[i] = new CellRegion("rows");
                _columnRegions[i] = new CellRegion("columns");
                _shapeRegions[i] = new CellRegion("shapes");
            }

            for (int row = 0; row < dimension; ++row) {
                for (int col = 0; col < dimension; ++col) {
                    int id = col + (row*dimension);
                    int shapeId = int.Parse(rows[row][col].ToString()) - 1;
                    Cell c = new Cell(id);
                    _cells[id] = c;
                    _rowRegions[row].Add(c);
                    _columnRegions[col].Add(c);
                    _shapeRegions[shapeId].Add(c);
                }
            }

        }

// TODO: improve docs here

        /// <summary>Sets the cells value at the specified index.</summary>
        /// <param name="cellIndex"></param>
        /// <param name="value"></param>
        public virtual void Set(int cell, int digit) {
            Console.WriteLine("setting {0} with {1}", cell, digit);
            try {
                _cells[cell].Values = new int[] { digit };
            } catch (Exception) { }
        }


        public virtual IEnumerable<int> Row(int cell) {
            CellRegion region = getRowRegionForCell(cell);
            return EnumerateIds(region.Cells);
        }

        public virtual IEnumerable<int> Column(int cell) {
            CellRegion region = getColumnRegionForCell(cell);
            return EnumerateIds(region.Cells);
        }

        public virtual IEnumerable<int> Shape(int cell) {
            CellRegion region = getShapeRegionForCell(cell);
            return EnumerateIds(region.Cells);
        }

        public virtual IEnumerable<int> Context(int cell) {
            HashSet<Cell> cells = new HashSet<Cell>();
            cells.UnionWith(getRowRegionForCell(cell).Cells);
            cells.UnionWith(getColumnRegionForCell(cell).Cells);
            cells.UnionWith(getShapeRegionForCell(cell).Cells);           
            return EnumerateIds(cells);
        }

        /// <summary>IEnumerable of ids for an IEnumerable of cells</summary>
        /// <param name="cells">The list of cells</param>
        /// <returns>A list of ids</returns>
        private IEnumerable<int> EnumerateIds(IEnumerable<Cell> cells) {
            foreach (Cell c in cells) {
                yield return c.Id;
            }
        }

// TODO: Document from here down
// TODO: Change accessors to protected virtual?

        private CellRegion getRowRegionForCell(int cell) {
            // return getRegionContainingCell(_cells[cell], _rowRegions); // Generic
            return _rowRegions[rowIndexForCell(cell)];
        }

        private CellRegion getColumnRegionForCell(int cell) {
            // return getRegionContainingCell(_cells[cell], _columnRegions); // Generic
            return _columnRegions[columnIndexForCell(cell)];
        }

        private CellRegion getShapeRegionForCell(int cell) {
            return getRegionContainingCell(_cells[cell], _shapeRegions);
        }

        private CellRegion getRegionContainingCell(Cell cell, CellRegion[] regions) {
            foreach (CellRegion region in regions) {
                if (region.Contains(cell)) {
                    return region;
                }
            }
            return null;
        }

// Unused, dependent on a Square Board
// Would be an excellent optimization.

        private int rowIndexForCell(int cell) {
            return (cell / _dimension);
        }

        private int columnIndexForCell(int cell) {
            return (cell % _dimension);
        }

    }
}
