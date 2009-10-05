using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using _3_SudokuModel;
using _3_SudokuModelAdvanced;

namespace _4_SudokuView
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SudokuViewWindow : Window
    {
        /// <summary>Reference to the Model.</summary>
        protected ObservableBoard _board;

        /// <summary>Access/mutate model.</summary>
        public ObservableBoard Board
        {
            get { return _board; }
            set {
                if (_board != null) { _board.Observers -= this.UpdateView; }
                _board = value; 
                _board.Observers += new ObservableBoard.BoardUpdated(this.UpdateView);
                UpdateView(_board); 
            }
        }

        /// <summary>Default constructor.</summary>
        /// <param name="board">Reference to model.</param>
        public SudokuViewWindow(ObservableBoard board) {
            InitializeComponent();
            Board = board;
        }

        /// <summary>Updates the view of the model.</summary>
        /// <param name="board">Reference to board which was updated.</param>
        public void UpdateView(System.Object board) {

        }

        /// <summary>Creates a duplicate view of the model.</summary>
        /// <param name="sender">Object which initiated this method call.</param>
        /// <param name="e">Event which caused this call.</param>
        protected virtual void Dup_Clicked(System.Object sender, EventArgs e) {
            new SudokuViewWindow(Board);
        }

        /// <summary>Presents a file dialogue and creates a new view for the model loaded.</summary>
        /// <param name="sender">Object which initaited this method call.</param>
        /// <param name="e">Event which caused this call.</param>
        protected virtual void New_Clicked(System.Object sender, EventArgs e) {
            OpenFileDialog ofg = new OpenFileDialog();
            ofg.Title = "Select a Sudoku file to load:";
            ofg.Filter = "txt|*.txt|All Files|*.*";

            // obtain selection
            if (ofg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string[] rows = ReadUntilBlankOrEnd(ofg.FileName).ToArray<string>();
                new SudokuViewWindow( new ObservableBoard( rows ));
            }
        }



        /// <summary>Reads lines from Standard Input until the first blank line</summary>
        /// <param name="fileName">File to read from.</param>
        /// <returns>The list of lines.</returns>
        protected virtual List<string> ReadUntilBlankOrEnd(string fileName)
        {
            string input;
            List<string> rows = new List<string>();
            TextReader reader = new StreamReader(fileName);
            while ((input = reader.ReadLine()) != null)
            {
                input = input.Trim();
                if (input.Equals("")) {
                    break; 
                } else {
                    rows.Add(input);
                }
            }
            return rows;
        }
    }
}
