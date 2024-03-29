﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3_SudokuModel {

    /// <summary>A read-only Sudoku cell.</summary>
    public class ReadonlyCell : Cell {

        /// <summary>Accessor to the value stored in this Cell.</summary>
        public override List<int> Values {
            get { return base.Values; }
            set { throw new InvalidOperationException("ReadonlyCell is a read-only Sudoku element whose value cannot be assigned after creation."); }
        }

        /// <summary>Default constructor.</summary>
        public ReadonlyCell(Board board, int id) : base(board, id) { }

        /// <summary>Constructor.</summary>
        /// <remarks>Constructs a new read-only cell from the cell specified.</remarks>
        /// <param name="cell">Cell from which to create a new read-only version.</param>
        public ReadonlyCell(Cell cell) : this(cell.Board, cell.Id, cell.Values.ToArray<int>()) { }

        /// <summary>Constructor.</summary>
        /// <remarks>Creates a non-assignable Cell with the specified permanent value.</remarks>
        /// <param name="board">Reference to the board possessing this cell.</param>
        /// <param name="id">Identifier for this cell.</param>
        /// <param name="values">Value for this Cell to hold.</param>
        public ReadonlyCell(Board board, int id, int[] values) : base(board, id, values) { }
    }

}
