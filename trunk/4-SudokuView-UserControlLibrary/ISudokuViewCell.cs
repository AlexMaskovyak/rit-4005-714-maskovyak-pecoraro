using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Media;
using BitArray = System.Collections.BitArray;

namespace _4_SudokuView_UserControlLibrary
{
    /// <summary>Interface for a UI Cell.</summary>
    public interface ISudokuViewCell {

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

        /// <summary>Reset the Cell Completely</summary>
        void Reset();

        /// <summary>Duplicate the Cell Completely.</summary>
        /// <returns>A duplicate Cell in the same state.</returns>
        ISudokuViewCell Duplicate();

        /// <summary>Is the Cell ReadOnly or Not</summary>
        bool ReadOnly { get; set; }

        /// <summary>Set the Background Color for the Cell</summary>
        Brush BackgroundColor { get; set; }

    }
}
