==============================================================================
 == Assignment: Selecting a Winner                                         ==
 == Author: Alex Maskovyak & Joe Pecoraro                                  ==
 == Date: 2009/10/14                                                       ==
==============================================================================

========================================
 == Table of Contents                ==
========================================
 = 1. Structure of the Archive       ==
 = 2. Resources Used for Development ==
 = 3. Setup                          ==
 = 4. Executing the Program          ==
 = 5. Design Decisions               ==
 = 6. Compatibility                  ==
========================================


===================================
 == 1. Structure of the Archive ==
===================================
 == meta (/*) ==
  readme.txt 	// this file, which describes the contents of
				// this deliverable and details written
				// information required by the homework
				// assignment

 == (/5-SelectingAWinner-ConsoleApplication/*) == 
 = C# source files =
  IReferee.cs 		// interface for referee objects
  AbstractReferee.cs	// implements common referee functionality
  Referee.cs		// provides convenience constructors, 
			//	overrides the GameLoop, 
			//	and implements Main test

  IView.cs		// professor provided interface for all objects 
			//  capable of viewing Referee
  SimplePlayer.cs	// implements IView for a simple-minded player, 
			// displays debug information to standard out

  Cell.cs	// producer/consumer cell for use with implementers of 
		//  IView

  SuitPrecedencePlayingCard.cs	// subclass of PlayingCard used to 
				//  override Suit enumeration and the 
				//  CompareTo method

 = VS Project =
  5-SelectingAWinner-ConsoleApplication.csproj
							
							
 == (/5-SelectingAWinner-UserControlLibrary/*) ==
 = C# source files =
  CardUserControl.xaml		// wpf user control layout
  CardUserControl.xaml.cs	// logic for displaying, getting and 
				//  setting a card

 = VS Project =
  5-SelectingAWinner-UserControlLibrary.csproj


 == (/5-SelectingAWinner-WPFApplication/*) ==
 = C# source files =
  CardGameViewWindow.xaml	// main wpf gui layout
  CardGameViewWinder.xaml.cs	// wpf gui view
  SelectedAWinner.cs		// driver program, runs the GUI
  
 = VS Project =
  5-SelectingAWinner-WPFApplication.csproj


==============================================================================
 == 2. Resources Used for Development                                      ==
==============================================================================
OS: 			Windows XP Professional VM (on Snow Leopard)
			Windows Vista 64-bit
EXE Environment:	Powershell
Language: 		C# 3.5
IDE: 			Microsoft Visual Studio 9.0
SVN Repository: 	Googlecode 			
		(http://code.google.com/p/rit-4005-714-maskovyak-pecoraro/)
		
SVN Plugin: 		AnkhSVN
SVN Management: 	TortoiseSVN


External resources:
Alex's Reading list: http://rit-4005-714-alexandermaskovyak.blogspot.com
Joe's Reading list: http://www.cs.rit.edu/~jjp1820/csharp/


==============================================================================
 == 3. Setup                                                               ==
==============================================================================
The contents of this archive should be unzipped into a directory of your
choosing to which Visual Studio has access. The projects can then be added
to a solution by right-clicking the solution name in the IDE and selecting
"Add" > "Existing Project". The dialog box that appears can be used to
select the Visual Studio project: 

   "5-SelectingAWinner-ConsoleApplication.csproj" and
   "5-SelectingAWinner-UserControlLibrary.csproj" and
   "5-SelectingAWinner-GUIApplication.csproj"

The project executable can then be built by right-clicking the project
names and selecting "Build".


==============================================================================
 == 4. Executing the Program                                               ==
==============================================================================
 = General =
Building a project will create an executable in the "bin/Debug/" directory 
which can be run in Powershell.  Alternatively, one can run Referee's Main 
method in Visual Studio itself by hitting ctrl-F5 when Referee.cs is in 
view in the main code view panel.


 = Console/Standard-Out Application =
Implemented in Referee.Main.

5-SelectingAWinner-ConsoleApplication.exe 
  <num cards in game> 
  i<player 1 card index selected>...i<player n card index selected>

*The number of cards must be greater than or equal to the number of 
selected indices.
  
 = GUI Application =
5-SelectingAWinner-GUIApplication.exe
  <num of cards in game>
  <num of players>
  <uri of card images>
  [ <seed> ]
  
*The number of cards must be greater than or equal to the number of 
players.  The seed is an optional numeric value used for predictable
shuffling/testing.

==============================================================================
 == 5. Design Decisions                                                    ==
==============================================================================
This week's code forced a refactoring of the code from previous weeks.  
Specifically: Deck did not provide virtual methods which could be overriden
to allow for the creation of a Deck with anything but PlayingCard objects.
This is insufficient for the needs of the current assignment which required
a subclassing of PlayingCard to make use of a different Suit ranking.


==============================================================================
 == 6. Compatibility                                                       ==
==============================================================================
GUI elements were predominately developed on the 32 bit Windows XP system. 
At some point during development, successful execution of the GUI application
on Windows Vista 64-bit was lost.  It is unknown how or why this occurred,
however, the program operates successfully in Windows XP.