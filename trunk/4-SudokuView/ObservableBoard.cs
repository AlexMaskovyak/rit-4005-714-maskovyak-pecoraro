using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using _3_SudokuModel;
using _3_SudokuModelAdvanced;

namespace _4_SudokuView
{
    /// <summary>
    /// Extends Board to be observable.
    /// </summary>
    public class ObservableBoard : BoardAdvanced
    {
        public delegate void BoardUpdated(System.Object sender);

        protected event BoardUpdated _observers;

        public virtual BoardUpdated Observers
        {
            get { return _observers; }
            set { _observers = value; }
        }

        public ObservableBoard(string[] rows) : base(rows) { }

        public override void Set(int cell, int digit)
        {
            base.Set(cell, digit);
            Observers(this);
        }

        public override void Clear(int cell)
        {
            base.Clear(cell);
            Observers(this);
        }
    }

}
