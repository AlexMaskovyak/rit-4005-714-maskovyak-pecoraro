=======================================================================
== Assignment: Database Week 8 ==
== Author: Alex Maskovyak & Joe Pecoraro ==
== Date: 2009/11/04 ==
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

== RemoteDB Project ==
/8-DatabaseWebService-RemoteDB/*	-- project directory
RemoteDB.cs							-- extension of LocalDB 
										implementing IModel<string>
RemoteDBTest-input.txt				-- test commands for RemoteDB
RemoteDBDBTest.cs					-- test program for RemoteDB


== Web Service ==
/8-DatabaseWebService-Service/*
DatabaseWebService.asmx	
DatabaseWebService.asmx.cs		-- service logic


== WPF Project ==
/8-DatabaseService-WPFApplication/*
TupleFieldsUserControl.xaml		-- layout for field manipulating 
									control
TupleFieldsUserControl.xaml.cs	-- code-behind for said control
Window1.xaml					-- layout for db access
Window1.xaml.cs					-- code-behind for db gui


== DocProject ==
/8-DatabaseWebService-DocProject/*	-- DocProject directory for all
										non-service projects


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

= Service Start =
Select DatabaseWebService.asmx as the project to view and select 
ctrl+F5 "start without debugging".  This will start the service in the
background allowing for testing of RemoteDB console application and
the GUI's RemoteDB option.


= RemoteDB Run =
Ensure the the Web Service is running.

shell> cat RemoteDBTest-input.txt | 8-DatabaseWebService-RemoteDB.exe


= WPF Application Run =
Ensure that the Web Service is running.

Select Window1.xaml.cs and select ctrl+F5 "start without debuggin". 



=======================================================================
== 5. Design Decisions ==
=======================================================================
= RemoteDB =
Currently services only one asynchronous request to the server at any
given time.  The GUI application is responsible for making only one
such request at a time and for disabling the user's ability to create
additional requests.


= LocalDB ==
Fixed issue with trim truncating tabs from standard-in.
Added locks and a monitoring object to support mult-threaded access.


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

_8_DatabaseWebService {

Client Side Proxy:

  RemoteDB: extends _7_Database.LocalDB (which is IModel<string>)
    a proxy for an IModel<string> using the LocalDB Facade on a web service.
    constructor: create proxy for web service

    Size: call web service for Size
    Enter: serialize tuple and call web service
    Remove: serialize tuple and call web service
    Search: serialize tuple and call web service, reformat the response
    AsyncEventHandlers: the web services support events
        original web service requests are sent asynchronously
        callbacks are called with the response
        handle the response and trigger the appropriate behavior


  RemoteDBTest: extends _7_Database.LocalDBTest (which is CommandLineDBAccess<string>)
    a test class for the RemoteDB proxy

    CreateDatabase: factory method, create a new RemoteDB
    Main: run the test using the same commands as LocalDB


Server Side:

  DatabaseWebService: extends System.Web.Services.WebService, wraps IModel<string>
    a threadsafe web service for a database facade IModel<string>
    constructor: create an internal database in the global space
        use CreateDatabase factory method

    CreateDatabase: factory method to create an IModel<string> database
        create a new LocalDB
    Size:   delegate to the internal database
    Enter:  delegate to the internal database
    Remove: delegate to the internal database
    Search: delegate to the internal database


Graphical View:

  TupleFieldsUserControl: extends System.Windows.Controls.UserControl, implements IEnumerable<string>
    variable size user control containing input textboxes for tuple values
    constructor: provide fields to start with

    AddField: create new fields
        provide an int to create blank fields
        provide names to create named fields
    Clear: clear textboxes
    Set: set the textboxes with given strings (as many as possible)
    Enumerate: return the text contents of the textboxs


  Window1: extends Window
    constructor: initialize view with 3 known tuples
        known tuples are "Name", "Phone", and "Room"

    CreateLocalDatabase:  factory method for the local database
    CreateRemoteDatabase: factory method for the remote database
    SwitchToLocal:  switch the UI to the local database
    SwitchToRemote: switch the UI to the remote database
    SwitchToDatabase: handle common UI tasks
        set the active database
        clear the tuple fields
        update the "size" to reflect the current database

    GetFirstLines: reflection of the specification
        use the Tuple Enumerator to get the first line of each textbox
    ToggleClick: switch between databases
        use SwitchTo* methods
    EnterClick: create a new tuple
        use GetFirstLines to build tuple
        run Enter on the active database
        Update UI to show new Size
    RemoveClick: remove a tuple
        use GetFirstLines to build tuple keys
        run Remove on the active database
        Update UI to show new Size
    SearchClick: search the database
        use GetFirstLines to build tuple keys
        run Search on the active database
        Update UI to show found results

}


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