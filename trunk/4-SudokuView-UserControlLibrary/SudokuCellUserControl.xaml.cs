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
    public partial class SudokuCellUserControl : UserControl, ISudokuViewCell
    {
        protected Label[] _digitLabels;
        protected SudokuViewWindow _window;

        public SudokuCellUserControl()
        {
            CreateGrid(3, 3);
            InitializeComponent();
        }

        public SudokuCellUserControl(SudokuViewWindow window, SolidColorBrush brush)
        {
            

            InitializeComponent();
            _window = window;
            /*_digitLabels = new Label[] { 
                this.BigDigitLabel, 
                this.LittleDigit1Label, 
                this.LittleDigit2Label, 
                this.LittleDigit3Label, 
                this.LittleDigit4Label, 
                this.LittleDigit5Label,
                this.LittleDigit7Label, 
                this.LittleDigit8Label, 
                this.LittleDigit9Label };*/
            //_board = board;
        }

        protected virtual void CreateGrid(int colDimension, int rowDimension)
        {
            Grid myGrid = new Grid();
            myGrid.Height = 100;
            myGrid.Width = 100;
            myGrid.HorizontalAlignment = HorizontalAlignment.Center;
            myGrid.VerticalAlignment = VerticalAlignment.Center;

            this.Content = myGrid;

            for (var col = 0; col < colDimension; ++col) {
                myGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (var row = 0; row < rowDimension; ++row) {
                myGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (var row = 0; row < rowDimension; ++row) {
                for (var col = 0; col < colDimension; ++col) {
                    TextBlock littleDigitBlock = new TextBlock();
                    littleDigitBlock.Text = ((colDimension * row) + col + 1).ToString();
                    littleDigitBlock.FontSize = 12;
                    Grid.SetRow(littleDigitBlock, row);
                    Grid.SetColumn(littleDigitBlock, col);
                    myGrid.Children.Add(littleDigitBlock);
                }
            }

            TextBlock bigDigitBlock = new TextBlock();
            bigDigitBlock.Text = "0";
            Grid.SetRow(bigDigitBlock, 0);
            Grid.SetColumn(bigDigitBlock, 0);
            Grid.SetRowSpan(bigDigitBlock, rowDimension);
            Grid.SetColumnSpan(bigDigitBlock, colDimension);
            myGrid.Children.Add(bigDigitBlock);
        }

        /// <summary>
        /// Sets the value of the central large digit.
        /// </summary>
        /// <param name="digit">Value assigned to this cell.</param>
        /*protected virtual void SetBigDigit(int digit)
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
        }*/


        public virtual void Click(System.Object sender, EventArgs e)
        {
            // this should communicate with the board to set the cell to the value
            //_board.((Label)sender).Content
            
        }

        public virtual void Update()
        {

        }

    }
}
