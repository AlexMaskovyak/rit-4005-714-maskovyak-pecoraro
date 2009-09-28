using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3_SudokuModel
{
    /// <summary>Implementation of the IBoard interface.  Holds the state of a Sudoku board.</summary>
    public class Board : IBoard
    {


        /// <summary>Default constructor.</summary>
        /// <param name="rows"></param>
        public Board(string[] rows) {

        }

        /// <summary>Sets the cells value at the specified index.</summary>
        /// <param name="cellIndex"></param>
        /// <param name="value"></param>
        public void Set(int cell, int digit) {
            
            Console.WriteLine( "setting {0} with {1}", cell, digit );
        }


        public virtual IEnumerable<int> Row(int cell) {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<int> Column(int cell) {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<int> Shape(int cell) {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<int> Context(int cell) {
            throw new NotImplementedException();
        }
    }
}
