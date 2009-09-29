using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3_SudokuModel {
    /// <summary>Implementation of the IBoard interface.  Holds the state of a Sudoku board.</summary>
    public class Board : IBoard {

        /// <summary>Monitors for rows</summary>
        CellRegion[] _rowRegions;

        /// <summary>Monitors for columns</summary>
        CellRegion[] _columnRegions;

        /// <summary>Monitors for shapes</summary>
        CellRegion[] _shapeRegions;

        /// <summary>Total list of cells</summary>
        Cell[] _cells;

        /// <summary>Default constructor.</summary>
        /// <param name="rows">String array in the format: 111222333 that specifies a row of a Sudoku board.  Numbers specify a "shape".</param>
        public Board(string[] rows) {
            int dimension = rows.Length;

            _rowRegions = new CellRegion[dimension];
            _columnRegions = new CellRegion[dimension];
            _shapeRegions = new CellRegion[dimension];
            _cells = new Cell[dimension * dimension];

            for (int i = 0; i < dimension; ++i) {
                _rowRegions[i] = new CellRegion("rows");
                _columnRegions[i] = new CellRegion("columns");
                _shapeRegions[i] = new CellRegion("shapes");
            }

            for (int col = 0; col < dimension; ++col) {
                for (int row = 0; row < dimension; ++row) {
                    int id = col + (col * row);
                    Cell c = new Cell(id);
                    _cells[id] = c;
                    _rowRegions[row].Add(c);
                    _columnRegions[col].Add(c);
                    _shapeRegions[int.Parse(rows[col])].Add(c);
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
            CellRegion region = getRowRegionForCell(_cells[cell]);
            return EnumerateIds(region.Cells);
        }

        public virtual IEnumerable<int> Column(int cell) {
            CellRegion region = getColumnRegionForCell(_cells[cell]);
            return EnumerateIds(region.Cells);
        }

        public virtual IEnumerable<int> Shape(int cell) {
            CellRegion region = getShapeRegionForCell(_cells[cell]);
            return EnumerateIds(region.Cells);
        }

        public virtual IEnumerable<int> Context(int cell) {
            Cell theCell = _cells[cell];
            CellRegion rowRegion = getRowRegionForCell(theCell);
            CellRegion columnRegion = getColumnRegionForCell(theCell);
            CellRegion shapeRegion = getShapeRegionForCell(theCell);
            HashSet<Cell> cells = new HashSet<Cell>();
            cells.UnionWith(rowRegion.Cells);
            cells.UnionWith(columnRegion.Cells);
            cells.UnionWith(shapeRegion.Cells);
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

        private CellRegion getRegionContainingCell(Cell cell, CellRegion[] regions) {
            foreach (CellRegion region in regions) {
                if (region.Contains(cell)) {
                    return region;
                }
            }
            return null;
        }

        private CellRegion getRowRegionForCell(Cell cell) {
            return getRegionContainingCell(cell, _rowRegions);
        }

        private CellRegion getColumnRegionForCell(Cell cell) {
            return getRegionContainingCell(cell, _columnRegions);
        }

        private CellRegion getShapeRegionForCell(Cell cell) {
            return getRegionContainingCell(cell, _shapeRegions);
        }

    }
}
