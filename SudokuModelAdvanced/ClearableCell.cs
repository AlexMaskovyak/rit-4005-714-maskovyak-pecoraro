using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using _3_SudokuModel;

namespace _3_SudokuModelAdvanced {

    /// <summary>A Cell that Can be Cleared</summary>
    public class ClearableCell : Cell {

        /// <summary>The Advanced Board this Cell belongs to.</summary>
        protected BoardAdvanced _boardAdvanced;

        /// <summary>Default Constructor</summary>
        /// <param name="board">Board this Cell belongs to.</param>
        /// <param name="id">This Cell's id.</param>
        public ClearableCell(BoardAdvanced board, int id) : base(board, id) {
            _boardAdvanced = board;
        }

        /// <summary>Clear this Cell's value</summary>
        public virtual void Clear() {

            // Do nothing if not set
            int oldDigit = Digit;
            if (oldDigit == 0) {
                return;
            }

            // Set the blank value, its inferred later
            Values = new int[] {};

            // Have Context Respond to the Clear
            foreach (Cell cell in ContextCells) {
                ClearableCell c = cell as ClearableCell; // TODO: lookup "as" syntax
                c.RespondToClear(oldDigit);
            }

            // Infer the Value from the Context
            InferValueFromContext();

        }

        /// <summary>Respond to a digit in this context changing from a value.</summary>
        /// <remarks>Check context to make sure you can re-enable that value as a potential value.</remarks>
        /// <param name="digit">The value it no longer is.</param>
        protected virtual void RespondToClear(int digit) {
            if (Digit != 0)
                return;

            foreach (Cell cell in ContextCells) {
                if (cell.Digit == digit) {
                    return;
                }
            }

            List<int> newValues = new List<int>(Values);
            newValues.Add(digit);
            Values = newValues.ToArray<int>();
        }

        /// <summary>Infer all the potential Values for this Cell from its Context Cells</summary>
        public virtual void InferValueFromContext() {
            List<int> newValues = new List<int>(Cell.AllValues);
            foreach (Cell cell in ContextCells) {
                if (cell.Digit != 0) {
                    newValues.Remove(cell.Digit);
                }
            }
            Values = newValues.ToArray<int>();
        }

    }

}
