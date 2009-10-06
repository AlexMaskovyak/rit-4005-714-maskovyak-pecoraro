using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _4_SudokuView_UserControlLibrary
{
    /// <summary>Interface for a UI Cell.</summary>
    public interface ISudokuViewCell
    {
        /// <summary>The UI Responds to a Click</summary>
        /// <param name="sender">object being clicked</param>
        /// <param name="e">click event arguments</param>
        void Click(System.Object sender, EventArgs e);

        /// <summary>The control is told to update</summary>
        void Update();
    }
}
