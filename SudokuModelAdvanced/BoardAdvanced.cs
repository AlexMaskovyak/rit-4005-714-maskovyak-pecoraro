using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using _3_SudokuModel;

namespace _3_SudokuModelAdvanced {

    /// <summary>Advanced Board containing the Clear Operation</summary>
    public class BoardAdvanced : Board, IBoardAdvanced {

        /// <summary>Default constructor.</summary>
        /// <param name="rows">Board format as strings.</param>
        public BoardAdvanced(string[] rows): base(rows) { }

        /// <summary>Factory Method for Creating Cells</summary>
        /// <param name="id">The Cell Id</param>
        /// <returns>A Clearable Cell</returns>
        public override Cell CreateCell(int id) {
            return new ClearableCell(this, id);
        }

        /// <summary>Unset a Cell's Value</summary>
        /// <param name="cell">The cell to clear.</param>
        public virtual void Clear(int cell) {
            // TODO: look up "as" syntax
            ((ClearableCell)_cells[cell]).Clear();
        }

    }

}
