==============================================================================
Assignment: Sudoku Model
Author: Alex Maskovyak & Joe Pecoraro
Date: 2009/09/29
==============================================================================

=========================
 == Table of Contents ==
=========================
1. Structure of the Archive
2. Resources Used for Development
3. Setup
3. Executing the Program
=========================


===================================
 == 1. Structure of the Archive ==
===================================
== 3-SudokuModel ==
== meta (/3-SudokuModel/*) ==
readme.txt 			// this file, which describes the contents of 
					// this deliverable and details written 
					// information required by the homework 
					// assignment

== C# source files (/3-SudokuModel/*) ==
Board.cs			// contains Sudoku board model, implements IBoard
Cell.cs				// Sudoku atomic, stores list of potential/actual values
CellRegion.cs		// holds and maintains a partial context of cells: (row, column, shape)
IBoard.cs			// professor provided interface for board model
ReadonlyCell.cs		// Sudoku atomic which cannot be modified (not currently used)

== Visual Studio project (/3-SudokuModel/*) ==
3-SudokuModel.csproj


== 3-SudokuModelAdvanced ==
== C# source files (/SudokuModelAdvanced/*) ==
BoardAdvanced.cs	// contains Sudoku board model, implements IBoardAdvanced (features Clear method)
ClearableCell.cs	// Sudoku atomic, stores list of potential/actual values, allows for clearing
IBoardAdvanced.cs	// Sudoku board model interface with Clear method.

== Visual Studio project (/SudokuModel/Advanced/*) ==
3-SudokuModelAdvanced.csproj


== 3-SudokuTester ==
== C# source files (/SudokuTester/*) ==
Test.cs				// creates a board and sets cells from a test file

== Input (/SudokuTester/*) ==
board.txt			// test file which defines a board and set commands

== Executable (/SudokuTester/bin/Debug/*) ==
3-SudokuTester.exe	// allows execution of the Tester program


== 3-SudokuTesterAdvanced ==
== C# source files (/3-SudokuTesterAdvanced/*) ==
TestAdvanced.cs		// creates a board with clear functionality from a test file

== Input (/3-SudokuTesterAdvanced/*) ==
board.txt			// test file which defines a board, set, and clear commands

== Executable (/3-SudokuTesterAdvanced/bin/Debug/*) ==
3-SudokuTesterAdvanced.exe	// allows execution of the advanced Tester program


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
choosing to which Visual Studio has access.  The project can then be added 
to a solution by right-clicking the solution name in the IDE and selecting 
"Add" > "Existing Project".  The dialog box that appears can be used to 
select the Visual Studio project: "3-SudokuModel.csproj".

The project executable can then be built by right-clicking the project 
name and selecting "Build".


=================================
 == 4.  Executing the Program ==
=================================
Building a project will create an executable in the "bin/Debug/" directory 
called "3-SudokuModel.exe".

This can be run at the command-line and input can be piped into standard-in 
for program operation.

For example, with the Windows PowerShell in the Project's directory:

  pwr-shell> cat board.txt | ./bin/Debug/3-SudokuTester.exe
  pwr-shell> cat board.txt | ./bin/Debug/3-SudokuTesterAdvanced.exe

