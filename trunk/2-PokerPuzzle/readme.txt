Images Source:
"Classic" Playing Cards
http://www.jfitz.com/cards/


==============================================================================
Assignment: #2 - Poker Puzzle
Author: Alex Maskovyak & Joe Pecoraro
Date: 2009/09/22
==============================================================================

===================================
 == Table of Contents           ==
===================================
1. Structure of the Archive
2. Resources Used for Development
3. Setup
4. Executing the Program
5. Explanation of Assignment 1 Use
===================================


===================================
 == 1. Structure of the Archive ==
===================================
== meta (/2-PokerPuzzle/*) ==
readme.txt 			// this file, which describes the contents of 
					// this deliverable and details written 
					// information required by the homework 
					// assignment
					
== C# Referenced Assembly ==
1-Poker.exe			// contains updated version of the first assignment

== C# source files (/2-PokerPuzzle/*) ==
Deck.cs				// contains Main method for execution
Permutation.cs		// creates n choose m permutations
Puzzle.cs			// extension of a PokerHand, stores the Puzzle's cards
Result.cs			// stores the game results for a user's selection
Windows1.xaml		// xml-based gui informatin
Windows1.xsml.cs	// C# gui construction

== Visual Studio project (/2-PokerPuzzle/*) ==
2-PokerPuzzle.csproj



=========================================
 == 2. Resources Used for Development ==
=========================================
OS:				Snow Leopard using a Virtual Machine to run Windows XP Professional
				Vista Home 64-bit Edition
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
select the Visual Studio project: "2-PokerPuzzle.csproj".

Please be sure to include a reference to 1-Poker.exe, a modified assembly
of last week's source to ensure proper execution.

The project executable can then be built by right-clicking the project 
name and selecting "Build".


=================================
 == 4.  Executing the Program ==
=================================
All Main methods can be run from inside Visual Studio.  
These include:
 Deck.Main		// demonstrates deck construction and shuffling
 Puzzle.Main	// demonstrates puzzle creation and the determination of the
					best had possible
 Windows1.xaml.cs	// runs the gui program
 
Be sure that the project is set to "Console Application" in 
Properties > "Application" > "Output Type" when running the Deck and Puzzle
methods.  Be sure that it is set to "Windows Application when running 
Windwos1.xaml.cs.

  
===========================================
 == 5.  Explanation of Assignment 1 Use ==
===========================================
Assignment 1 could have been reused for this one, however, we determined that
Puzzle had significant similarities to PokerHand.  In the course of developing
Puzzle it became apparent that we were re-implementing very many of the same
operations.  Puzzle seemingly is a special case of PokerHand, one which can
have 5 or greater cards.  PokerHand stores cards, adds cards, displays them, 
score's itself, and compares its score against others.  This is all functionality
that Puzzle required.  So we rebuilt PokerHand to have additional constructors,
and to allow for certain methods to be overriden by Puzzle.
