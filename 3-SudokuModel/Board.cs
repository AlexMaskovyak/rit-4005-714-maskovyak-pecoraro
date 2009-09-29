﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3_SudokuModel
{
    /// <summary>Implementation of the IBoard interface.  Holds the state of a Sudoku board.</summary>
    public class Board : IBoard
    {

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
        public Board(string[] rows)
        {
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
        protected virtual void ParseRows(string[] rows)
        {
            int dimension = rows.Length;

            _dimension = dimension;
            _rowRegions = new CellRegion[dimension];
            _columnRegions = new CellRegion[dimension];
            _shapeRegions = new CellRegion[dimension];
            _cells = new Cell[dimension * dimension];

            for (int i = 0; i < dimension; ++i)
            {
                _rowRegions[i] = new CellRegion("rows");
                _columnRegions[i] = new CellRegion("columns");
                _shapeRegions[i] = new CellRegion("shapes");
            }

            for (int row = 0; row < dimension; ++row)
            {
                for (int col = 0; col < dimension; ++col)
                {
                    int id = col + (row * dimension);
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
        public virtual void Set(int cell, int digit)
        {
            Console.WriteLine("setting {0} with {1}", cell, digit);
            //try {
            _cells[cell].Values = new int[] { digit };
            //} catch (Exception e) {
            //Console.WriteLine(e.StackTrace);
            //}
        }

        /// <summary>List of cell ids for cells in the same row as the provided cell.</summary>
        /// <param name="cell">The cell id to find others in the same row.</param>
        /// <returns>The cell ids of all other cells in the same row.</returns>
        public virtual IEnumerable<int> Row(int cell)
        {
            CellRegion region = getRowRegionForCell(cell);
            return EnumerateIds(region.Cells, cell);
        }

        /// <summary>List of cell ids for cells in the same column as the provided cell.</summary>
        /// <param name="cell">The cell id to find others in the same column.</param>
        /// <returns>The cell ids of all other cells in the same column.</returns>
        public virtual IEnumerable<int> Column(int cell)
        {
            CellRegion region = getColumnRegionForCell(cell);
            return EnumerateIds(region.Cells, cell);
        }

        /// <summary>List of cell ids for cells in the same shape as the provided cell.</summary>
        /// <param name="cell">The cell id to find others in the same shape.</param>
        /// <returns>The cell ids of all other cells in the same shape.</returns>
        public virtual IEnumerable<int> Shape(int cell)
        {
            CellRegion region = getShapeRegionForCell(cell);
            return EnumerateIds(region.Cells, cell);
        }

        /// <summary>List of cell ids for cells in the same context as the provided cell.</summary>
        /// <remarks>The "context" is the union of the Row, Column, and Shape for the provided cell.</remarks>
        /// <param name="cell">The cell id to find others in the same context.</param>
        /// <returns>The cell ids of all other cells in the same context.</returns>
        public virtual IEnumerable<int> Context(int cell)
        {
            HashSet<Cell> cells = new HashSet<Cell>();
            cells.UnionWith(getRowRegionForCell(cell).Cells);
            cells.UnionWith(getColumnRegionForCell(cell).Cells);
            cells.UnionWith(getShapeRegionForCell(cell).Cells);
            return EnumerateIds(cells, cell);
        }

        /// <summary>IEnumerable of ids for an IEnumerable of cells</summary>
        /// <param name="cells">The list of cells</param>
        /// <returns>A list of ids</returns>
        private IEnumerable<int> EnumerateIds(IEnumerable<Cell> cells, int without)
        {
            foreach (Cell c in cells)
            {
                if (c.Id != without)
                {
                    yield return c.Id;
                }
            }
        }

        // TODO: Change some accessors below to protected virtual?

        /// <summary>Get the CellRegion of the Row for a cell id</summary>
        /// <param name="cell">The cell id.</param>
        /// <returns>The CellRegion representing that Row.</returns>
        private CellRegion getRowRegionForCell(int cell)
        {
            // return getRegionContainingCell(_cells[cell], _rowRegions); // Generic
            return _rowRegions[rowIndexForCell(cell)];
        }

        /// <summary>Get the CellRegion of the Column for a cell id</summary>
        /// <param name="cell">The cell id.</param>
        /// <returns>The CellRegion representing that Column.</returns>
        private CellRegion getColumnRegionForCell(int cell)
        {
            // return getRegionContainingCell(_cells[cell], _columnRegions); // Generic
            return _columnRegions[columnIndexForCell(cell)];
        }

        /// <summary>Get the CellRegion of the Shape for a cell id</summary>
        /// <param name="cell">The cell id.</param>
        /// <returns>The CellRegion representing that Shape.</returns>
        private CellRegion getShapeRegionForCell(int cell)
        {
            return getRegionContainingCell(_cells[cell], _shapeRegions);
        }

        /// <summary>A generic way to find a cell in a list of CellRegions</summary>
        /// <param name="cell">The cell to find.</param>
        /// <param name="regions">The regions to search.</param>
        /// <returns>The region containing the cell, null if none of the regions.</returns>
        private CellRegion getRegionContainingCell(Cell cell, CellRegion[] regions)
        {
            foreach (CellRegion region in regions)
            {
                if (region.Contains(cell))
                {
                    return region;
                }
            }
            return null;
        }

        /// <summary>Calculate the row for a cell</summary>
        /// <param name="cell">The cell id.</param>
        /// <returns>The row index (0-indexed)</returns>
        protected virtual int rowIndexForCell(int cell)
        {
            return (cell / _dimension);
        }

        /// <summary>Calculate the column for a cell</summary>
        /// <param name="cell">The cell id.</param>
        /// <returns>The column index (0-indexed)</returns>
        protected virtual int columnIndexForCell(int cell)
        {
            return (cell % _dimension);
        }

        /// <summary>Calculate the shape for a cell</summary>
        /// <param name="cell">The cell id.</param>
        /// <returns>The shape index (0-indexed), -1 if not found</returns>
        protected virtual int shapeIndexForCell(int cell)
        {
            Cell c = _cells[cell];
            for (int i = 0; i < _shapeRegions.Length; ++i)
            {
                CellRegion region = _shapeRegions[i];
                if (region.Contains(c))
                {
                    return i;
                }
            }

            return -1;
        }


        // TODO: Remove this later
        // Debug output of the board

        public void Debug()
        {
            for (int i = 0; i < _cells.Length; ++i)
            {
                if ((i % _dimension) == 0)
                {
                    Console.WriteLine();
                }
                Cell c = _cells[i];
                if (c.Values.Length == 1)
                {
                    Console.Write(c.Values[0] + " ");
                }
                else
                {
                    Console.Write("- ");
                }
            }
        }

        // TODO: Remove this public accessor, or genericize it to just return a BitArray of Values
        // this is currently only available for debugging.

        public Cell getCell(int cell)
        {
            return _cells[cell];
        }

    }
}