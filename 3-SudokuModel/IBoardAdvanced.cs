using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3_SudokuModel {
    public interface IBoardAdvanced : IBoard {
        /// <summary>Removes the effect of an earlier Set message.</summary>
        /// <param name="index"></param>
        void Clear(int index);
    }
}
