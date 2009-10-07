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
using _4_SudokuView_UserControlLibrary;

namespace _4_SudokuView
{
    /// <summary>SudokuView</summary>
    public partial class SudokuViewWindow : Window {

        /// <summary>Color Map for Shapes</summary>
        public static Brush[] ColorMap = new Brush[] {
            Brushes.White,
            Brushes.Wheat,
            Brushes.Thistle,
            Brushes.AliceBlue,
            Brushes.Beige,
            Brushes.Azure,
            Brushes.BlueViolet,
            Brushes.Bisque,
            Brushes.Coral
        };

        /// <summary>Reference to the Model.</summary>
        protected ObservableBoard _board;

        /// <summary>Loading mode indicator.</summary>
        protected bool _loading = true;

        /// <summary>Commands that have been run.</summary>
        protected Stack<ISudokuCommand> _undoStack;

        /// <summary>Commands that have been reverted.</summary>
        protected Stack<ISudokuCommand> _redoStack;

        /// <summary>Grid containing Sudoku View Cells</summary>
        protected Grid _sudokuGrid;

        /// <summary>The Sudoku View Cells</summary>
        protected List<ISudokuViewCell> _sudokuCells;

        /// <summary>Access/mutate model.</summary>
        public ObservableBoard Board
        {
            get { return _board; }
            set {
                if (_board != null) {
                    _board.OnSet -= Board_OnSet;
                    _board.OnPossible -= Board_OnPossible;
                }
                _board = value;
                _board.OnSet += new ObservableBoard.OnSetHandler(Board_OnSet);
                _board.OnPossible += new ObservableBoard.OnPossibleHandler(Board_OnPossible);
            }
        }

        /// <summary>Deafult Constructor</summary>
        public SudokuViewWindow() {
            InitializeComponent();
            _undoStack = new Stack<ISudokuCommand>();
            _redoStack = new Stack<ISudokuCommand>();
            _sudokuCells = new List<ISudokuViewCell>();
            InitializeView();
        }



        public SudokuViewWindow(string filename) {
            InitializeComponent();
            _undoStack = new Stack<ISudokuCommand>();
            _redoStack = new Stack<ISudokuCommand>();
            LoadFromFile(filename);
        }


        protected void LoadFromFile(string filename) {

            // Initialize the Board
            TextReader reader = new StreamReader(filename);
            string[] rows = ReadUntilBlankOrEnd(reader).ToArray<string>();
            Board = new ObservableBoard(rows);

            // Set Loading Cells
            _loading = true;
            List<string> lines = ReadUntilBlankOrEnd(reader);
            foreach (string line in lines) {
                string[] args = line.Split(' ');
                if (args.Length == 2) {
                    SetLine(args);
                } else if (args.Length == 1) {
                    ClearLine(args);
                }
            }
            _loading = false;
        }

        /// <summary>Parse a Set Command</summary>
        /// <param name="args">Arguments [cell, value]</param>
        protected void SetLine(string[] args) {
            int cell = int.Parse(args[0]);
            int value = int.Parse(args[1]);
            Console.WriteLine("set in {0}: {1}", cell, value);
            _board.Set(cell, value);
        }

        /// <summary>Parse a Clear Command</summary>
        /// <param name="args">Arguments [cell]</param>
        protected void ClearLine(string[] args) {
            int cell = int.Parse(args[0]);
            Console.WriteLine("clear {0}", cell);
            _board.Clear(cell);
        }

        /// <summary>Constructor with Model.</summary>
        /// <param name="board">Reference to model.</param>
        public SudokuViewWindow(ObservableBoard board) {
            // TODO: Test this.
            InitializeComponent();
            Board = board;

            _undoStack = new Stack<ISudokuCommand>();
            _redoStack = new Stack<ISudokuCommand>();

        }

        /// <summary>Constructs a new window view based upon the model of the specified window.</summary>
        /// <param name="window">Window off of which to pattern a new window.</param>
        public SudokuViewWindow(SudokuViewWindow window) : this(window.Board) {
            // TODO: Implement and Test
        }


        /// <summary>Helper Method to Draw UI Components.</summary>
        protected virtual void InitializeView() {

            // Set the Buttons (make a method?)
            UpdateUndoRedoState();
            

            // Build the Grid
            Grid myGrid = new Grid();
            myGrid.Height = 500;
            myGrid.Width = 500;
            myGrid.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            myGrid.VerticalAlignment = VerticalAlignment.Center;

            // Rows and Columns
            for (var col = 0; col < 9; ++col) { myGrid.ColumnDefinitions.Add(new ColumnDefinition()); }
            for (var row = 0; row < 9; ++row) { myGrid.RowDefinitions.Add(new RowDefinition()); }

            // Handlers
            SudokuCellUserControl.OnViewCellSetHandler onsetHandler = new SudokuCellUserControl.OnViewCellSetHandler(Cell_OnSet);
            SudokuCellUserControl.OnViewCellClearHandler onclearHandler = new SudokuCellUserControl.OnViewCellClearHandler(Cell_OnClear);


            // Populate
            for (var row = 0; row < 9; ++row) {
                for (var col = 0; col < 9; ++col) {
                    SudokuCellUserControl cell = new SudokuCellUserControl(Brushes.Yellow);
                    cell.OnSet += onsetHandler;
                    cell.OnClear += onclearHandler;
                    Grid.SetRow(cell, row);
                    Grid.SetColumn(cell, col);
                    myGrid.Children.Add(cell);
                    _sudokuCells.Add(cell);
                }
            }

            // Add the Grid
            _sudokuGrid = myGrid;
            _sudokuGrid.VerticalAlignment = VerticalAlignment.Top;
            _sudokuGrid.Margin = new Thickness(0, 10, 0, 0);
            grid.Children.Add(_sudokuGrid);
        }




        /// <summary>Creates a duplicate view of the model.</summary>
        /// <param name="sender">Object which initiated this method call.</param>
        /// <param name="e">Event which caused this call.</param>
        protected virtual void Dup_Clicked(System.Object sender, EventArgs e) {
            new SudokuViewWindow(this);
        }

        /// <summary>Presents a file dialogue and creates a new view for the model loaded.</summary>
        /// <param name="sender">Object which initiated this method call.</param>
        /// <param name="e">Event which caused this call.</param>
        protected virtual void New_Clicked(System.Object sender, EventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select a Sudoku file to load:";
            ofd.Filter = "txt|*.txt|All Files|*.*";

            // obtain selection
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                LoadFromFile(ofd.FileName);
            }
        }

        /// <summary>Undoes the last Command on the stack.</summary>
        /// <param name="sender">Object which initiated this method call.</param>
        /// <param name="e">Event which caused this call.</param>
        protected virtual void Undo_Clicked(System.Object sender, EventArgs e) {
            ISudokuCommand command = _undoStack.Pop();
            command.Undo(Board);
            _redoStack.Push(command);
            UpdateUndoRedoState();
        }

        /// <summary>Redoes the last Command that was Undone.</summary>
        /// <param name="sender">Object which initiated this method call.</param>
        /// <param name="e">Even which caused this call.</param>
        protected virtual void Redo_Clicked(System.Object sender, EventArgs e) {
            ISudokuCommand command = _redoStack.Pop();
            command.Do(Board);
            _undoStack.Push(command);
            UpdateUndoRedoState();
        }

        /// <summary>Update the Buttons Based on Stack History</summary>
        protected virtual void UpdateUndoRedoState() {
            UndoBtn.IsEnabled = (_undoStack.Count != 0);
            RedoBtn.IsEnabled = (_undoStack.Count != 0);
        }

        /// <summary>Reads lines from Standard Input until the first blank line</summary>
        /// <param name="fileName">File to read from.</param>
        /// <returns>The list of lines.</returns>
        protected virtual List<string> ReadUntilBlankOrEnd(TextReader reader) {
            string input;
            List<string> rows = new List<string>();
            while ((input = reader.ReadLine()) != null) {
                input = input.Trim();
                if (input.Equals("")) {
                    break; 
                } else {
                    rows.Add(input);
                }
            }
            return rows;
        }

        /// <summary>Board Set Listener</summary>
        /// <param name="cell">The cell being set.</param>
        /// <param name="digit">The digit it was set to.</param>
        protected void Board_OnSet(int cell, int digit) {
            _sudokuCells[cell].Update(digit);
            _sudokuCells[cell].ReadOnly = _loading;
        }

        /// <summary>Board Possible Listener</summary>
        /// <param name="cell">The cell being updated.</param>
        /// <param name="bits">The digits it is allowed to have.</param>
        protected void Board_OnPossible(int cell, System.Collections.BitArray bits) {
            _sudokuCells[cell].Update(bits);
        }

        /// <summary>A Cell was Set, relay to the Model</summary>
        /// <param name="sender">The cell that was clicked.</param>
        /// <param name="digit">The value it was set to.</param>
        protected void Cell_OnSet(SudokuCellUserControl sender, int digit) {
            int cell = _sudokuCells.IndexOf(sender);
            _board.Set(cell, digit);
            _undoStack.Push(new SetSudokuCommand(cell, digit));
            UndoBtn.IsEnabled = true;
        }


        /// <summary>A Cell was Cleared, relay to the Model</summary>
        /// <param name="sender">The cell that was clicked.</param>
        /// <param name="digit">The value it had.</param>
        protected void Cell_OnClear(SudokuCellUserControl sender, int digit) {
            int cell = _sudokuCells.IndexOf(sender);
            _board.Clear(cell);
            _undoStack.Push(new ClearSudokuCommand(cell, digit));
            UndoBtn.IsEnabled = true;
        }


  

    }
}
