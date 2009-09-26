using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3_SudokuModel
{
    public interface IBoardAdvanced : IBoard
    {
        void Set(int index);
    }
}
