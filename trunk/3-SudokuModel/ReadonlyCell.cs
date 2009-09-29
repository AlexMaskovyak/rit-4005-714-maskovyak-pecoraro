﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3_SudokuModel {

    /// <summary>A read-only Sudoku cell.</summary>
    public class ReadonlyCell : Cell {

        /// <summary>Accessor to the value stored in this Cell.</summary>
        public override int[] Values {
            get { return base.Values; }
            set { throw new InvalidOperationException("ReadonlyCell is a read-only Sudoku element whose value cannot be assigned after creation."); }
        }

        /// <summary>Default constructor.</summary>
        public ReadonlyCell(int id, IBoard board) : base(id, board) { }

        /// <summary>Constructor.</summary>
        /// <remarks>Constructs a new read-only cell from the cell specified.</remarks>
        /// <param name="cell">Cell from which to create a new read-only version.</param>
        public ReadonlyCell(Cell cell) : this(cell.Id, cell.Board, cell.Values) { }

        /// <summary>Constructor.</summary>
        /// <remarks>Creates a non-assignable Cell with the specified permanent values.</remarks>
        /// <param name="values">Values for this Cell to hold.</param>
        public ReadonlyCell(int id, IBoard board, int[] values) : base(id, board, values) { }
    }

}