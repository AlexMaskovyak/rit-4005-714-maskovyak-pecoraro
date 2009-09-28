using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3_SudokuModel
{
    /// <summary></summary>
    public class AssignableCell : Cell
    {
        private Boolean _validValue;


        public override int Value {
            get { return base.Value; }
            set { _value = value; base.observers(this); }
        }

        public AssignableCell() : base(-1) {
        }

        public static void Main() {
            AssignableCell cell = new AssignableCell();
            CellRegion column = new CellRegion("column");
            CellRegion row = new CellRegion("row");
            CellRegion shape = new CellRegion("shape");
            column.Observe(cell);
            row.Observe(cell);
            shape.Observe(cell);
            cell.Value = 2;
        }
    }
}
