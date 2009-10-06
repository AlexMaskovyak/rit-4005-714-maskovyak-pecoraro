using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using _3_SudokuModel;
using _3_SudokuModelAdvanced;

namespace _4_SudokuView {

    /// <summary>A Sudoku Command (such as set or clear)</summary>
    public interface ISudokuCommand {

        /// <summary>Play the command on the board.</summary>
        /// <param name="board">The board to play on.</param>
        void Do(IBoardAdvanced board);

        /// <summary>Play the reverse command on the board.</summary>
        /// <param name="board">The board to play on.</param>
        void Undo(IBoardAdvanced board);

    }
}
