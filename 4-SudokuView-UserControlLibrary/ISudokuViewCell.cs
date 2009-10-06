using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using BitArray = System.Collections.BitArray;

namespace _4_SudokuView_UserControlLibrary
{
    /// <summary>Interface for a UI Cell.</summary>
    public interface ISudokuViewCell
    {
        /// <summary>The UI Responds to a Click</summary>
        /// <param name="sender">object being clicked</param>
        /// <param name="e">click event arguments</param>
        void Click(System.Object sender, MouseButtonEventArgs e);

        /// <summary>The control is told to update to a set digit</summary>
        /// <param name="digit">The Cell is set with a single digit</param>
        void Update(int digit);

        /// <summary>The control is told to update to a set digit</summary>
        /// <param name="bits">The Cell is set with a single digit</param>
        void Update(BitArray bits);
    }
}
