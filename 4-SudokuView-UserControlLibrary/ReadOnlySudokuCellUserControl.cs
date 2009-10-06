using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BitArray = System.Collections.BitArray;

using _4_SudokuView;

namespace _4_SudokuView_UserControlLibrary
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public class ReadOnlySudokuCellUserControl : SudokuCellUserControl
    {
        /// <summary>Default Constructor</summary>
        public ReadOnlySudokuCellUserControl() {
            throw new InvalidOperationException("A ReadOnlySudokuCellUserControl must be given a Digit to display.");
        }

        /// <summary>Default Constructor with Value</summary>
        /// <param name="digit">The Digit to display.</param>
        public ReadOnlySudokuCellUserControl(int digit) : this(digit, Brushes.White) { }

        /// <summary>Constructor with Value and Background Color.</summary>
        /// <param name="digit">The Digit to display.</param>
        /// <param name="backgroundColor">The background color to apply to this control.</param>
        public ReadOnlySudokuCellUserControl(int digit, Brush backgroundColor) : base(backgroundColor) {
            HideAllButBigBlock();
            _bigBlock.Foreground = Brushes.Blue;
            _bigBlock.Text = digit.ToString();
            _bigBlock.Visibility = Visibility.Visible;
        }

        /// <summary>Read-only cells should not respond to a click event with any sort of play commnd.</summary>
        /// <param name="sender">Ignored.</param>
        /// <param name="e">Ignored.</param>
        public override void Click(System.Object sender, MouseButtonEventArgs e) { }

        /// <summary>Readonly cells cannot update anything.</summary>
        /// <param name="digit">Ignored.</param>
        public override void Update(int digit) { }

        /// <summary>Readonly cells cannot update anything.</summary>
        /// <param name="bits">Ignored.</param>
        public override void Update(BitArray bits) { }
    }
}
