using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3_SudokuModel
{
    public interface IBoard
    {
        void Set(int cell, int digit);
        IEnumerable<int> Row(int cell);
        IEnumerable<int> Column(int cell);
        IEnumerable<int> Shape(int cell);
        IEnumerable<int> Context(int cell);
    }
}
