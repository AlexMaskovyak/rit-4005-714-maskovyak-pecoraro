using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3_SudokuModel
{
    public class CellRegion
    {
        private string _name;
        private List<Cell> _cells;
        private HashSet<int> _valuesHeld;

        public CellRegion(string name) {
            _name = name;
            _cells = new List<Cell>();
            _valuesHeld = new HashSet<int>();
        }



        public void Observe(Cell cell) {
            cell.observers += new Cell.ValueAssigned(this.ValueCheck);
            _cells.Add(cell);
        }



        /// <summary>
        /// Determines whether 
        /// </summary>
        /// <param name="cell"></param>
        public void ValueCheck(Cell cell) {
            
            int i = cell.Value;

            Console.WriteLine(String.Format("{0} has found cell has: {1}", _name, cell.Value));
        }

    }
}
