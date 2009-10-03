using IBoard = _3_SudokuModel.IBoard;

namespace _3_SudokuModel {
    /// <summary>Advanced Sudoku interface.</summary>
    public interface IBoardAdvanced : IBoard {
        /// <summary>Removes the effect of an earlier Set message.</summary>
        /// <param name="index">The position to clear.</param>
        void Clear(int index);
    }
}
