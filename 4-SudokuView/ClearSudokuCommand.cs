﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using _3_SudokuModel;
using _3_SudokuModelAdvanced;

namespace _4_SudokuView {
    /// <summary>Describes a clear command in Sudoku.</summary>
    public class ClearSudokuCommand : ISudokuCommand {

        /// <summary>The cell being modified.</summary>
        protected int _cellIndex;

        /// <summary>The value it was set to.</summary>
        protected int _digit;

        /// <summary>Default constructor.</summary>
        /// <param name="cellIndex">Index this command operated upon.</param>
        /// <param name="digit">Old digit.</param>
        public ClearSudokuCommand(int cellIndex, int digit) {
            _cellIndex = cellIndex;
            _digit = digit;
        }

        /// <summary>Does the Command operation on the Board.</summary>
        /// <param name="board">Board upon which to process this command.</param>
        public void Do(IBoardAdvanced board) {
            board.Clear(_cellIndex);
        }

        /// <summary>Undoes the Command operation on the Board.</summary>
        /// <param name="board">Board upon which to process thic command.</param>
        public void Undo(IBoardAdvanced board) {
            board.Set(_cellIndex, _digit);
        }

    }
}
