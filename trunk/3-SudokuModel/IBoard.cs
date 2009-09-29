using IEnumerable = System.Collections.Generic.IEnumerable<int>;

namespace _3_SudokuModel
{
    /// <summary> what a Sudoku Model must do. </summary>
    public interface IBoard
    {
        /// <summary> set a digit into a cell. </summary>
        void Set(int cell, int digit);
        /// <summary> indices in same row. </summary>
        IEnumerable Row(int cell);
        /// <summary> indices in same column. </summary>
        IEnumerable Column(int cell);
        /// <summary> indices in same shape. </summary>
        IEnumerable Shape(int cell);
        /// <summary> indices in context of cell. </summary>
        IEnumerable Context(int cell);
    }
}
