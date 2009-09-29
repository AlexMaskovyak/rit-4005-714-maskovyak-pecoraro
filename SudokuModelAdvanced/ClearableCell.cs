using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using _3_SudokuModel;

namespace _3_SudokuModelAdvanced {
    public class ClearableCell : Cell {

        public ClearableCell(BoardAdvanced board, int id) : base(board, id) { }

        protected virtual void Clear() {
            // TODO
        }

    }
}
