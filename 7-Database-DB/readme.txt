=======================================================================
== Assignment: Database ==
== Author: Alex Maskovyak & Joe Pecoraro ==
== Date: 2009/10/28 ==
=======================================================================


========================================
== Table of Contents ==
========================================
= 1. Structure of the Archive =
= 2. Resources Used for Development =
= 3. Setup =
= 4. Executing the Program =
= 5. Design Decisions =
= 6. CRC
========================================


=======================================================================
== 1. Structure of the Archive ==
=======================================================================
== Meta ==
readme.txt			-- this file, which describes the contents of
					-- this deliverable and details written
					-- information required by the homework
					-- assignment
== DB Solution ==
/7-Database-DB/*			-- project directory
7-Database.csproj			-- VS project file
7-Database/*				-- Project directory
CommandLineDBAccess.cs		-- base class for testing
DB.cs						-- basic IDB implementation
DBDelegateFactory.cs		-- factory for creating 
								common delegates
DBTest-input.txt			-- test commands for for DB
DBTest.cs					-- test program for DB
IDB.cs						-- professors database interface


== LocalDB Solution ==
/7-Database-LocalDB/*		-- project directory
IModel.cs					-- interface for LocalDB
LocalDB.cs					-- extension of DB which implements
								the IModel facade
LocalDBTest-input.txt		-- test commands for LocalDB
LocalDBTest.cs				-- test program for LocalDB


== DocProject ==
/7-Database-DocProject/*	-- DocProject directory


=======================================================================
== 2. Resources Used for Development ==
=======================================================================
OS:						Windows XP Professional VM (on Snow Leopard)
						Windows Vista 64-bit
EXE Environment:		Powershell
Language:				C# 3.5
IDE:					Microsoft Visual Studio 9.0
SVN Repository:			Googlecode
			(http://code.google.com/p/rit-4005-714-maskovyak-pecoraro/)
SVN Plugin:				AnkhSVN
SVN Management:			TortoiseSVN

External resources:
Alex's Reading list: 
	http://rit-4005-714-alexandermaskovyak.blogspot.com
Joe's Reading list: 
	http://www.cs.rit.edu/~jjp1820/csharp/


=======================================================================
== 3. Setup ==
=======================================================================
The contents of this archive should be unzipped into a directory of 
your choosing to which Visual Studio has access. A solution can be 
viewed from the Visual Studio IDE by going to 
	File -> Open -> "Project/Solution". 

This will add the projects contained therein. This will have to be 
done for each project you wish to view. 

The projects can be built by right-clicking the project names and 
selecting "Build".


=======================================================================
== 4. Executing the Program ==
=======================================================================
= General =
Building a project will create an executable in the "bin/Debug/" 
directory which can be run in Powershell for executable projects. 

= DB Run =
shell> cat DBTest-input.txt | 7-Database-DB.exe

= LocalDB Run =
shell> cat LocalDBTest-input.txt | 7-Database-LocalDB.exe


=======================================================================
== 5. Design Decisions ==
=======================================================================
= Add behavior =

Spec: "adds a tuple and returns true if it replaces a tuple with equal 
content as determined by the match argument." 
Interpretation: remove ALL tuples with equal content as determined by 
the match argument, add the given tuple, and return true if any were 
removed (read as "replaced").


= Search behavior =
Spec: "display all tuples where all corresponding fields are equal to 
the non-empty words in the command." / 
"words to be shown in each field"
Interpretation: creates modified tuples for display where every nth
tuple returned is formed from the nth word of all matching tuples


=======================================================================
== 6. CRC ==
=======================================================================
_7_Database {
	
	IDB<T>:
		Add: adds or replaces a tuple based upon a match delegate
		Extract: creates an array of values for matching tuples using
					a report delegate
		Delete: removes a tuple using a match delegate
	
	
	DB: IDB<T>
		implements IDB<T> as expected using an ICollection compatible
		construct with nothing
		
		CreateTupeCollection: constructs a List
		ToString: prints out the database's tuples
		
		
	IModel<T>:
		Size: obtains a count of database tuples
		Search: finds tuples whose values match the non-empty values
				of the template tuple
		Enter: adds or replaces a tuple
		Remove: removes a tuple
		
	
	LocalDB: extends DB<string>, IModel<string>
		implements IModel<string> using DB<string> as a base
		construct with nothing
		
		Size: obtains a count of database tuples
		Search: finds tuples whose words match the non-empty values
				of the template tuple
			DBDelegateFactory.CreateRegexMatcher
			DBDelegateFactory.CreateIndexExtractingReporter
		Enter: adds or replaces a tuple
			DBDelegateFactory.CreateRegexMatcher
		Remove: removes a tuple
			DBDelegateFactory.CreateRegexMatcher
		
		
	DBDelegateFactory:
		abstract class with factory methods for constructing
		delegates for use in DB implementations
		
		CreateSimpleMatcher: creates Predicate matching an exact tuple
		CreateWildCardMatcher: creates a Predicate which matches with 
			non-regex based wildcards
		CreateSingleFieldRegexMatcher: creates a Predicate which 
			matches a tuple's ith word using a regex pattern
		CreateRegexMatcher: creates a Predicate which matches all 
			tuple's words using regex patterns
			
		Identity: reporter which returns the tuple passed into it
		CreateIndexExtractingReporter: returns a tuple's ith word
			
			
	CommandLineDBAccess<T>:
		generic acccess to an IDB from the command line 
		constructs from nothing
		
		CreateDatabase: template factory method to create the db to 
			access and test
		Run: reads in and processes commands for standard in
			ReadUntilBlankOrEnd: grab standard-in commands
			RunCommands: process the commands
		RunCommands: template method for running commands
		
	
	DBTest: extends CommandLineDBAcces<string>
		command line testing for DB
		constructs from nothing
		
		CreateDatabase: creates a DB<sring>
		RunCommands: overridden to support tab delimited words and 
			display debugging to standard out
			ProcessCommand: determines type of command, delegates action
				ProcessAddCommand: process the add command
				ProcessExtractCommand: process the extract command
				ProcessDeleteCommand: process the delete command
				ProcessDebugCommand: process the debug command
		Main: driver program
			contructs DBTest and runs item
	
	
	LocalDBTest: extends DBTest
		command line testing for LocalDB
		constructs from nothing
		
		CreateDatabase: creates a LocalDB<string>
		ProcessCommand: overriden from DBTest to suppose different
			commands
			Enter: process enter command
			Search: process search command
			Remove: process remove command
			DBTest.ProcessDebugCommand: process debug command
		Main: driver program
			constructs LocalDBTest and runs item
	
}