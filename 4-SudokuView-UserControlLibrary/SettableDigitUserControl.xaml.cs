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

using _4_SudokuView;

namespace _4_SudokuView_UserControlLibrary
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class SettableDigitUserControl : UserControl
    {
        protected Label[] _digitLabels;
        protected SudokuViewWindow _window;
        //protected ObservableBoard _board;

        public SettableDigitUserControl(SudokuViewWindow window)
        {
            InitializeComponent();
            _window = window;
            _digitLabels = new Label[] { 
                this.BigDigitLabel, 
                this.LittleDigit1Label, 
                this.LittleDigit2Label, 
                this.LittleDigit3Label, 
                this.LittleDigit4Label, 
                this.LittleDigit5Label,
                this.LittleDigit7Label, 
                this.LittleDigit8Label, 
                this.LittleDigit9Label };
            //_board = board;
        }

        /// <summary>
        /// Sets the value of the central large digit.
        /// </summary>
        /// <param name="digit">Value assigned to this cell.</param>
        protected virtual void SetBigDigit(int digit)
        {
            this.BigDigitLabel.Content = digit;
            this.BigDigitLabel.Visibility = System.Windows.Visibility.Visible;
            foreach (Label label in _digitLabels)
            {
                label.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        /// <summary>
        /// Sets the visibility of the surrounding potential digits for this control.
        /// </summary>
        /// <param name="potentialDigits">Digits to make visible.</param>
        protected virtual void SetLittleDigits(params int[] potentialDigits)
        {
            this.BigDigitLabel.Visibility = System.Windows.Visibility.Hidden;
            foreach (int digit in potentialDigits)
            {
                _digitLabels[digit].Visibility = System.Windows.Visibility.Visible;
            }
        }

        protected virtual void DigitMouseUp(System.Object sender, EventArgs e)
        {
            // this should communicate with the board to set the cell to the value
            //_board.((Label)sender).Content
            
        }
    }
}
