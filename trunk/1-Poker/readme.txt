==============================================================================
Assignment: Poker Project
Author: Alex Maskovyak & Joe Pecoraro
Date: 2009/09/11
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
== meta (/1-Poker/*) ==
readme.txt 			// this file, which describes the contents of 
					// this deliverable and details written 
					// information required by the homework 
					// assignment

== C# source files (/1-Poker/*) ==
Driver.cs			// contains Main method for execution
PlayingCard.cs		// stores card information
PokerHand.cs		// stores a hand of cards
Score.cs			// Ranks and scores card hands

== Visual Studio project (/1-Poker/*) ==
1-Poker.csproj



=========================================
 == 2. Resources Used for Development ==
=========================================
OS:				Windows Vista Home 64-Bit
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
select the Visual Studio project: "1-Poker.csproj".

The project executable can then be built by right-clicking the project 
name and selecting "Build".


=================================
 == 4.  Executing the Program ==
=================================
Building a project will create an executable in the "bin/Debug/" directory 
called "1-Poker.exe".

This can be run at the command-line and input can be piped into standard-in 
for program operation.

For example, with the Windows PowerShell in the Project's directory:

  pwr-shell> cat SampleHands.txt | ./bin/Debug/1-Poker.exe

