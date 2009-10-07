==============================================================================
Assignment: SudokuView
Author: Alex Maskovyak & Joe Pecoraro
Date: 2009/10/07
==============================================================================

=========================
 == Table of Contents ==
=========================
1. Structure of the Archive
2. Resources Used for Development
3. Setup
4. Executing the Program
5. CRC
6. Design Decisions
=========================


===================================
 == 1. Structure of the Archive ==
===================================
== 4-SudokuView ==
== meta (/*) ==
readme.txt 			// this file, which describes the contents of 
					// this deliverable and details written 
					// information required by the homework 
					// assignment

== C# source files (/4-SudokuView/*) ==
ClearSudokuCommand.cs	// represents a Sudoku Command that cleared a cell
ISudokuCommand.cs		// interface for Sudoku Commands, allows for a command to be done or undone 
ObservableBoard.cs		// extends AdvancedBoard, providing a model which triggers events and which can have observers
SetSudokuCommand.cs		// represents a Sudoku Command that set a cell to some value
SudokuViewWindow.cs		// window for the application, provides Main method for execution

== Visual Studio project (/4-SudokuView/*) ==
3-SudokuView.csproj

== Executable (/4-SudokuView/bin/Debug/*) ==
3-SudokuView.exe		// executable for the window


== 4-SudokuView-UserControlLibrary ==
== C# source files (/4-SudokuuView-UserControlLibrary/*) ==
ISudokuViewCell.cs					// interface defining what a Cell's UI operations
ReadOnlySudokuCellUserControl.cs	// Cell UI for a read-only cell, unfortunately we were unable to determine how to
									// replace Cells in a Grid and this is unused, forcing some of its logic to be 
									// to be used in SudokuCellUserControl
SudokuCellUserControl.cs			// implementation of the Cell UI and extension of a User Control, notifies its
									// observers of the user's interaction with itself, provides logic for displaying
									// and hiding digits

== Visual Studio project (/4-SudokuView-UserControlLibrary/*) ==
4-SudokuView-UserControlLibrary.csproj


=========================================
 == 2. Resources Used for Development ==
=========================================
OS:				Snow Leopard using a Virtual Machine to run Windows XP Professional
EXE Environment:Powershell
Language:		C# 3.5
IDE:			Microsoft Visual Studio 9.0
SVN Repository:	Googlecode 
				(http://code.google.com/p/rit-4005-714-maskovyak-pecoraro/)
SVN Plugin:		AnkhSVN
SVN Management:	TortoiseSVN


External resources:
Alex's Reading list: http://rit-4005-714-alexandermaskovyak.blogspot.com
Joe's Reading list: http://www.cs.rit.edu/~jjp1820/csharp/


=================================
 == 3.  Setup ==
=================================
The contents of this archive should be unzipped into a directory of your 
choosing to which Visual Studio has access.  The projects can then be added 
to a solution by right-clicking the solution name in the IDE and selecting 
"Add" > "Existing Project".  The dialog box that appears can be used to 
select the Visual Studio project: "4-SudokuView.csproj" and
"4-SudokuView-UserControlLibrary.csproj"

The project executable can then be built by right-clicking the project 
name and selecting "Build".


=================================
 == 4.  Executing the Program ==
=================================
Building a project will create an executable in the "bin/Debug/" directory 
called "4-SudokuView.exe".

This will open an empty window from which one can load a new board from a
file containing the definition of a board in the proper format.


===============
 == 5.  CRC ==
===============
_3_SudokuModel {
  Board: IBoard
      construct from string[]

      Set: set digit into cell with given index
          Locate Cell and call Cell.Set with the digit

      GetShapes: list of lists describing shapes
          Return Shape Cell Regions

      Row: enumerate row containing the cell index
      Column: enumerate column containing the cell index
      Shape: enumerate shape containing the cell index
      Context: enumerate context containing the cell index
          Set Union of Row, Column, Shape

      NowSet: template hook method for when a Cell is set
      NowPossible: template hook method for when a Cell has new potential values

      CreateCell: factory method for cell
	      Cell: constructor

  Cell:
      Set: Set the digit of this cell
        Fire Board's NowSet
        Have Context Respond to Set

      RespondToSet: the digit something got set in context
        Remove digit from possible values
        Fire Board's NowPossible if values changed

      Digit: Set Value or null
      Values: possible values for this cell
}


_3_SudokuModelAdvanced {
  BoardAdvanced: IBoard, IBoardAdvanced
      Clear: remove digit from cell with given index
          Locate Cell and call Cell.Clear

      CellContext: Given a Cell this gets the Cells in its context
          Use Board.Context enumerator

  ClearableCell: Cell
      Clear: remove digit value
          Remove Value
          Have Context Respond to Clear
          Infer new value from Context
          Fire NowPossible for inferred values

      RespondToClear: digit that is now available
          Check if value can be added to possible values
          Fire NowPossible if values changes
}


_4_SudokuView {
  ISudokuCommand:
      Undo: returns the move that would be the opposite of the saved move
      Redo: returns the original move, unchanged

  SetCommand: ICommand
      construct with digit
      Undo: return Reverse
      Redo: return this

  ClearCommand: ICommand
      construct with digit
      Undo: return Reverse
      Redo: return this

  ObservableBoard: _3_SudokuModelAdvanced.AdvancedBoard
      "onset" event: fired when something is set
      "onpossible" event: fired when something has changed

      NowSet: fire onset event
      NowPossible: fire onpossible event

  SudokuWindow: Window
      empty constructor:
          Initialize the UI and wait for a file to load.

      copy constructor:
          Duplicate the UI elements to have the same states and Model
      
      Init: initilaize the UI
      Reset: clear the UI

      LoadFromFile: given a stream
          Read lines to Initialize Board
          Read lines to Set ReadOnly Cells
          Color Shapes

      Play: play a move, by sending to the model
          Call Set or Clear in the Model
          Place the ICommand on the UndoStack

      UndoClick: handler, cancel the last move on this view
          Pop from UndoStack
          Play the ICommand.Undo
          Place the ICommand on the RedoStack

      RedoClick: handler, rerun a move that was cancelled
          Pop from RedoStack
          Play the ICommand.Redo
          Place the ICommand on the UndoStack

      DupClick: handler, create a new view hooked into the same model
          Create a new SudokuWindow with a copy of the current state and model
          Use Copy Constructor

      NewClick: handler, create a new view, provide a file to load
          Get FileName from UI
          Open Stream
          Use LoadFromFile
      
      CellSet: handler, relay to the board
          Relay cell index and value to the Board
          Place ICommand of action onto the UndoStack
      
      CellClear: handler, relay to the board
          Relay cell index and value to the Board
          Place ICommand of action onto the UndoStack

}


_4_SudokuView_UserControlLibrary {
  ISudokuViewCell:
      Click: respond to a user click
      Update: bit array of potential values
      Update: digit a single value
      Duplicate: duplicate the cell, colors, and values

  ReadonlySudokuCellUserControl: SudokuCell
      construct with value and optional color
          Value is the BigDigit
          Show the BigDigit, hide others

      Click: do nothing, cell cannot change
      Update: do nothing, cell cannot change
      Update: do nothing, cell cannot change

  SudokuCellUserControl: UserControl, ISudokuViewCell
      construct with optional color
          Hide the BigDigit
          Show Small Digits

      "onset" event: Fired when small digit clicked, contains the digit's value
      "onclear event": Fired when big digit clicked, contains the digit's value

      Click: user has clicked the cell
          Determine what label was clicked, any action needed?
          Build a Move object from the action
          Notify the Window to Play the Move

      Update: bit array of potential values
          Show/Hide the proper labels
          Redraw on the UI

      Update: digit a single value
          Hide small labels
          Show the big label
          Redraw on the UI

      Duplicate: duplicate the cell, colors, and values
          Set Values
          Show/Hide Properly
          Set Background and Foreground Colors
}



=================================
 == 6.  Design Decisions ==
=================================

