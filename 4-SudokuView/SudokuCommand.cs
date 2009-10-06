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
        protected IBoardAdvanced _board;
        protected int _cellIndex;
        protected int? _digit;

        /// <summary>Default constructor.</summary>
        /// <param name="type">Type of Sudoku command.</param>
        /// <param name="board">Reference to board affected.</param>
        /// <param name="cellIndex">Index this command operated upon.</param>
        /// <param name="digit">Previous digit.</param>
        public SudokuCommand(SudokuCommand.Type type, IBoardAdvanced board, int cellIndex, int? digit) {
            _type = type;
            _board = board;
            _cellIndex = cellIndex;
            _digit = digit;
        }

        /// <summary>Does the Command operation on the Board.</summary>
        public void Do() {
            switch (_type)
            {
                case Type.CLEAR:
                    _board.Clear(_cellIndex);
                    break;
                case Type.SET:
                    _board.Set(_cellIndex, _digit);
                    break;
            }
        }

        /// <summary>Undoes the Command operation on the Board.</summary>
        public void Undo() {
            switch (_type)
            {
                case Type.CLEAR:
                    _board.Set(_cellIndex, _digit);
                    break;
                case Type.SET:
                    _board.Clear(_cellIndex);
                    break;
            }
        }
    }
}
