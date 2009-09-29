using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3_SudokuModel {
    /// <summary>Implementation of the IBoard interface.  Holds the state of a Sudoku board.</summary>
    public class Board : IBoard {
        CellRegion[] _rowRegions;
        CellRegion[] _columnRegions;
        CellRegion[] _shapeRegions;
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
                _rowRegions[i] = new CellRegion("a");
                _columnRegions[i] = new CellRegion("b");
                _shapeRegions[i] = new CellRegion("c");
            }

            for (int col = 0; col < dimension; ++col) {
                for (int row = 0; row < dimension; ++row) {
                    Cell c = new Cell();
                    _cells[col + (col * row)] = c;
                    _rowRegions[row].Add(c);
                    _columnRegions[col].Add(c);
                    _shapeRegions[int.Parse(rows[col])].Add(c);
                }
            }
        }

        /// <summary>Sets the cells value at the specified index.</summary>
        /// <param name="cellIndex"></param>
        /// <param name="value"></param>
        public void Set(int cell, int digit) {
            Console.WriteLine("setting {0} with {1}", cell, digit);
            try {
                _cells[cell].Values = new int[] { digit };
            } catch (Exception) { }
        }


        public virtual IEnumerable<int> Row(int cell) {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<int> Column(int cell) {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<int> Shape(int cell) {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<int> Context(int cell) {
            throw new NotImplementedException();
        }
    }
}
