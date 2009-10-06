using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using _3_SudokuModel;
using _3_SudokuModelAdvanced;

namespace _4_SudokuView
{
    /// <summary>Command object for operations on a Sudoku board.</summary>
    public class SudokuCommand
    {
        public enum Type { SET, CLEAR };

        protected SudokuCommand.Type _type;
        protected int _cellIndex;
        protected int _digit;

        /// <summary>Default constructor.</summary>
        /// <param name="type">Type of Sudoku command.</param>
        /// <param name="board">Reference to board affected.</param>
        /// <param name="cellIndex">Index this command operated upon.</param>
        /// <param name="digit">Previous digit.</param>
        public SudokuCommand(SudokuCommand.Type type, int cellIndex, int digit) {
            _type = type;
            _cellIndex = cellIndex;
            _digit = digit;
        }

        /// <summary>Does the Command operation on the Board.</summary>
        /// <param name="board">Board upon which to process this command.</param>
        public void Do(IBoardAdvanced board) {
            switch (_type)
            {
                case Type.CLEAR:
                    board.Clear(_cellIndex);
                    break;
                case Type.SET:
                    board.Set(_cellIndex, _digit);
                    break;
            }
        }

        /// <summary>Undoes the Command operation on the Board.</summary>
        /// <param name="board">Board upon which to process thic command.</param>
        public void Undo(IBoardAdvanced board) {
            switch (_type)
            {
                case Type.CLEAR:
                    board.Set(_cellIndex, _digit);
                    break;
                case Type.SET:
                    board.Clear(_cellIndex);
                    break;
            }
        }
    }
}
