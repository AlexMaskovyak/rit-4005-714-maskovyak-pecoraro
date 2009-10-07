﻿using System;
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


namespace _4_SudokuView_UserControlLibrary
{
    /// <summary>Interaction logic for a SudokuCell</summary>
    public partial class SudokuCellUserControl : UserControl, ISudokuViewCell {

        /// <summary>Handler for a Set Event</summary>
        /// <param name="digit">The digit it is now.</param>
        public delegate void OnViewCellSetHandler(SudokuCellUserControl sender, int digit);

        /// <summary>Handler for a Clear Event</summary>
        /// <param name="bits">The digits it was set to.</param>
        public delegate void OnViewCellClearHandler(SudokuCellUserControl sender, int digit);

        /// <summary>Fired when the value is set</summary>
        public event OnViewCellSetHandler OnSet;

        /// <summary>Fired when value is cleared.</summary>
        public event OnViewCellClearHandler OnClear;

        /// <summary>The mini text blocks.</summary>
        protected List<TextBlock> _blocks;

        /// <summary>The large text block.</summary>
        protected TextBlock _bigBlock;

        /// <summary>ReadOnly state</summary>
        protected Boolean _readonly;

        /// <summary>Get/Set ReadOnly Property</summary>
        public bool ReadOnly {
            get { return _readonly; }
            set {
                _readonly = value;
                if (value) {
                    this.Foreground = Brushes.Blue;
                } else {
                    this.Background = Brushes.Black;
                }
            }
        }

        /// <summary>Set the Background Color</summary>
        public Brush BackgroundColor {
            get { return this.Background; }
            set { this.Background = value; }
        }

        /// <summary>Default Constructor uses White Background</summary>
        public SudokuCellUserControl() : this(Brushes.White) {}

        /// <summary>Constructor with Background Color</summary>
        /// <param name="backgroundColor">The desired background</param>
        public SudokuCellUserControl(Brush backgroundColor) {
            InitializeComponent();
            _blocks = new List<TextBlock>();
            _readonly = false;
            this.MouseUp += new MouseButtonEventHandler(Click);
            this.Content = CreateGrid(3, 3);
            this.Background = backgroundColor;
        }

        /// <summary>Helper for initializing a TextBlock</summary>
        /// <param name="initialText">The text to display in the TextBlock.</param>
        /// <returns>A TextBlock with some styles.</returns>
        protected virtual TextBlock CreateTextBlock(string initialText) {
            TextBlock txt = new TextBlock();
            txt.HorizontalAlignment = HorizontalAlignment.Stretch;
            txt.VerticalAlignment = VerticalAlignment.Center;
            txt.TextAlignment = TextAlignment.Center;
            txt.Text = initialText;
            return txt;
        }

        /// <summary>Initializer for the Grid</summary>
        /// <param name="colDimension">The number of columns.</param>
        /// <param name="rowDimension">The number of rows.</param>
        /// <returns>An initialized Grid.</returns>
        protected virtual Grid CreateGrid(int colDimension, int rowDimension) {

            // Grid
            Grid myGrid = new Grid();
            myGrid.Height = 50;
            myGrid.Width = 50;
            myGrid.HorizontalAlignment = HorizontalAlignment.Center;
            myGrid.VerticalAlignment = VerticalAlignment.Center;

            // Rows and Columns
            for (var col = 0; col < colDimension; ++col) {
                myGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (var row = 0; row < rowDimension; ++row) {
                myGrid.RowDefinitions.Add(new RowDefinition());
            }

            // Populate the small TextBlocks
            for (var row = 0; row < rowDimension; ++row) {
                for (var col = 0; col < colDimension; ++col) {
                    string label = ((colDimension * row) + col + 1).ToString();
                    TextBlock littleDigitBlock = CreateTextBlock(label);
                    Grid.SetRow(littleDigitBlock, row);
                    Grid.SetColumn(littleDigitBlock, col);
                    myGrid.Children.Add(littleDigitBlock);
                    _blocks.Add(littleDigitBlock);
                }
            }

            // Populate the large TextBlock
            TextBlock bigDigitBlock = CreateTextBlock("0");
            bigDigitBlock.FontSize = 36;
            bigDigitBlock.Visibility = Visibility.Hidden;
            Grid.SetRow(bigDigitBlock, 0);
            Grid.SetColumn(bigDigitBlock, 0);
            Grid.SetRowSpan(bigDigitBlock, rowDimension);
            Grid.SetColumnSpan(bigDigitBlock, colDimension);
            myGrid.Children.Add(bigDigitBlock);
            _bigBlock = bigDigitBlock;

            // Return
            return myGrid;
        }

        /// <summary>Handler for when the User Clicks on the Control</summary>
        /// <param name="sender">Default sender.</param>
        /// <param name="e">Default event arguments.</param>
        public virtual void Click(object sender, MouseButtonEventArgs e) {
            if (_readonly) return;
            object clickedObject = e.OriginalSource;
            
            // Clicked Big Block
            if (clickedObject == _bigBlock) {
                Console.WriteLine("Clicked Big Number");
                if (OnClear != null) OnClear(this, int.Parse(_bigBlock.Text));
                return;
            }

            // Clicked a Smaller Block
            foreach (TextBlock t in _blocks) {
                if (clickedObject == t) {
                    Console.WriteLine("Clicked: " + t.Text);
                    if (OnSet != null) OnSet(this, int.Parse(t.Text));
                    return;
                }
            }
        }

        /// <summary>Update the UI to display a single digit.</summary>
        /// <param name="digit">The digit to display.</param>
        public virtual void Update(int digit) {
            if (_readonly) return;
            HideAllButBigBlock();
            _bigBlock.Text = digit.ToString();
            _bigBlock.Visibility = Visibility.Visible;
        }

        /// <summary>Update the UI to display multiple values.</summary>
        /// <param name="bits">Indicators of the digits to display.</param>
        public virtual void Update(BitArray bits) {
            if (_readonly) return;
            _bigBlock.Visibility = Visibility.Hidden;
            for (int i = 0; i < bits.Length; ++i) {
                _blocks[i].Visibility = (bits[i] ? Visibility.Visible : Visibility.Hidden);
            }
        }

        /// <summary>Helper for hiding the small labels</summary>
        protected void HideAllButBigBlock() {
            foreach (TextBlock t in _blocks) {
                t.Visibility = Visibility.Hidden;
            }
        }
    }
}