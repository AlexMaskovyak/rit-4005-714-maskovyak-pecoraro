=======================================================================
== Assignment: Distributed Winner ==
== Author: Alex Maskovyak & Joe Pecoraro ==
== Date: 2009/10/21 ==
=======================================================================
========================================
== Table of Contents ==
========================================
= 1. Structure of the Archive ==
= 2. Resources Used for Development ==
= 3. Setup ==
= 4. Executing the Program ==
= 5. Testing the Web Service ==
= 6. Design and CRC ==
= 7. Login Design ==
========================================
===================================
== 1. Structure of the Archive ==
===================================
== Meta ==
readme.txt			-- this file, which describes the contents of
					-- this deliverable and details written
					-- information required by the homework
					-- assignment
== Client Solution ==
/7-Database-DB/*		-- project directory
7-Database.csproj					-- VS project file
7-Database/*						-- Project directory
CommandLineDBAccess.cs				-- base class for testing
DB.cs								-- basic IDB implementation
DBDelegateFactory.cs				-- factory for creating 
										common delegates
DBTest-input.txt					-- test commands for for DB
IDB.cs								-- professors database interface



client.cs -- Client game GUI for use with Service
Remote.cs -- Implements calls to service, acts as
-- a proxy
== Service Solution ==
/rit-4005-714-6-DistributedWinner-Service/* -- Solution directory
rit-4005-714-6-DistributedWinner-Service.sln -- VS solution file
6-DistributedWinner-Service/* -- Project directory
6-DistributedWinner-Service.csproj-- VS service project file
PlayerCellService.asmx -- provides a Service to a 2 player game
PlayerCellService.asmx.cs
Web.config
=======================================================================
== 2. Resources Used for Development ==
=======================================================================
OS: Windows XP Professional VM (on Snow Leopard)
Windows Vista 64-bit
EXE Environment: Powershell
Language: C# 3.5
IDE: Microsoft Visual Studio 9.0
SVN Repository: Googlecode
(http://code.google.com/p/rit-4005-714-maskovyak-pecoraro/)
SVN Plugin: AnkhSVN
SVN Management: TortoiseSVN
External resources:
Alex's Reading list: http://rit-4005-714-alexandermaskovyak.blogspot.com
Joe's Reading list: http://www.cs.rit.edu/~jjp1820/csharp/
=======================================================================
== 3. Setup ==
=======================================================================
The contents of this archive should be unzipped into a directory of your
choosing to which Visual Studio has access. A solution can be viewed from the
Visual Studio IDE by going to File -> Open -> "Project/Solution". This will
add the solution and any projects contained therein. This will have to be
done for each solution you wish to view. Multiple solutions cannot be viewed
at the same time.
The projects for each sollution can be built by right-clicking the project
names and selecting "Build".
=======================================================================
== 4. Executing the Program ==
=======================================================================
= General =
Building a project will create an executable in the "bin/Debug/" directory
which can be run in Powershell for executable projects. Projects like the Web
service will have to be executed from Visual Studio to make use of its builtin
server.
= Service Run =
Select PlayerCellService.asmx and select Debug > "Start Debugging".
This will create a server running PlayerCellService.
= Client Run =
If you are running the Service in Visual Studio, you will have to run the
Client executables from a separate area. Using Powershell, go to the
"bin/Debug" directory and run 6-DistributedWinner-Client.exe. This takes
several command line parameters:
6-DistributedWinner-Client.exe
<number of cards to select from>
[ <imageURI> <seed> ]
It can also be run with default arguments with:
6-DistributedWinner-Client.exe debug
In order to play a game, 2 processes must be started separately.
=======================================================================
== 5. Testing the Web Service ==
=======================================================================
Run the Service as detailed in Section 4. This should prompt your web-browser
to open up a page depicting the methods available from this service. The url
for this service for our local system was:
"http://localhost:50369/PlayerCellService.asmx"
The list of methods is shown below
Selecting the Login link brings you to a page describing the method
We are now going to add Players for our service. Hitting invoke logs a new
player with the service. The service produces an integer player-id value for
the invoker
Hitting the invoke button again logs a second new player with the service.
We can now test communication from Player 0 to Player 1. Proceed back to the
main PlayerCellService area and select the Set link. We can have Player 0
send the value of “5” to Player 1. We do this by specifying that Player 0’s
id (0) and his selection as “5”.
Invoking this will produce a blank page as a result since there is no return
type for this method.
Next we go back to the list of service methods and select Get. To have Player
1 get values sent to him, we enter “1” for the playerId.
Which produces the following result after invocation:
Now, we will demonstrate communication in the opposite direction. Proceeding
back to the service methods list, we select “Set” again. This time we’ll have
Player 1 send the value of 10 to Player 0.
Invoking this produces a blank page.
Going back to the service list and selecting “Get” again, we can this time
retrieve the value that Player 0 now has been sent.
=======================================================================
== 6. Design and CRC ==
=======================================================================
// Unchanged - Not Used Directly - Removed Details to Lessen Clutter
_1_Poker { PlayingCard }
_2_PokerPuzzle { Deck }
_5_SelectingAWinner-UserControlLibrary { CardUserControl }
_5_SelectingAWinner-GUIApplication { PlayingCardCache }
_5_SelectingAWinner-ConsoleLibrary {
SuitPrecedenceDeck, SuitPrecedencePlayingCard }
// Changed to correct issue with seed use
_5_SelectingAWinner-ConsoleLibrary {
Cell:
Value:
wait on get
wait on filling if not already full
IReferee<T>:
Join: incoming player
Leave: leaving player
Players: enumerate the players
Start: trigger game play
IView:
Choose: return a selected index
Tell: tell the view about a selected index and what it was
Winner: find out if a winner or not
Ready: returns once read
AbstractReferee<T>
implements IReferee as expected
GameLoop: template method called from inside Start
Referee: AbstractReferee<IView>
construct with #cards, #players, seed
CreateDeck: factory method for creating decks
Main: test for a single round using cmd line args
uses SimplePlayer IViews
Rounds: setting the number of rounds in a game
positive int value is finite
null is infinite
GameLoop: implement game specific logic
loop over number of rounds if any
for each player ensure Ready
for each player Choose() and Tell() all
determine best card and its player
for each player tell if Winner()
}
// Simplified
_5_SelectingAWinner-GUIApplication {
CardGameViewWindow: Window, IView
construct with #cards, imageUri
// other details ignored
SelectingAWinner
construct with #cards, #players, imageUri, seed
CreateReferee: factory method
CreateView: factory method
Run: Create Referee, Views, and hook them up
Main: driver for the program
construct and run a SelectingAWinner object
}
_6_DistributedWinner {
PlayerCellService : WebService
default constructor
Setup "Next" Player id
Setup a Dictionary of Cells
Login:
if 1st setup Cells
if 2nd do nothing
return and increment "Next" Player id
Get(id): Return the value in the complentary player's cell
Set(id, val): Set a value into your cell
IsFirst: Check if you are Player 1 or 2
GetComplementId(id): return complementary player id from a given id
}
_6_DistributedWinner_Client {
SelectingAWinnerService
PlayerCellService SOAP Client
Remote : IView
construct with nothing
creates SOAP Proxy through WSDL to a Service
Login to the Service
Determine if First Player or Not
IsFirst property
Choose: return value from Service for the other player
Tell: ignore if duplicate, otherwise set value on the Service
Winner: do nothing
Ready: get indication from other player (or do nothing)
Exchange:
take a list of values, and send them to the other player
return an agreed on list of values (the IsFirst player's)
DistributedSelectingAWinner : SelectingAWinner
construct with #cards, imageUri, seed
CreateProxy: factory method
Run:
CreateProxy with factory method and Exchange values
CreateView with factory method
CreateReferee with factory method and hook up Views
Run Referee in Background Thread
Run Event Loop for Views
Main: driver for the program
construct and run a DistributedSelectingAWinner object
}
=======================================================================
== 7. Login Design ==
=======================================================================
= Components =
There are very few components:
* Player 1 (Remove : IView)
* Player 2 (Remote : IView)
* Service (Web Service)
* Cell A (Cell<int>)
* Cell B (Cell<int>)
= Service Interface =
The operations that can be performed on the service are defined in our CRC
and also listed here for convenience:
* Login() -> id
* Get(id) -> value of corresponding player's cell
* Set(id, val) -> set value of current player's cell
* IsFirst(id) -> True if Player 1, False otherwise
= Connection Protocol (includes Data Exchange) =
There are two phases of the Connection Protocol. The first phase is done
individually by each Player to determine their id and whether they are first
or not. The second phase is the exchange of information between both the
connected players.
Phase 1: Setup Individual Players
1. Player => Login()
id <= Service Response
2. Player => IsFirst(id)
bool <= Service Response
3. Player begins Phase 2
Note that in this phase, the actions of each player may be in any order due
to the fact that they are asynchronous. This requires that the Service
itself be threadsafe in issuing player id's and checking IsFirst.
Phase 2: Exchange Information (seed and numCards)
Player 1 is the player who received true from IsFirst(id)
Player 1 writes to Cell A and reads from Cell B
Player 2 is the player who received false from IsFirst(id)
Player 2 writes to Cell B and reads from Cell A
* For Each Value to Send:
Player 1 => Set(value) Player 2 => Get()
Player 1 => Get() [ACK] Player 2 => Set(ACK)
Note that in this phase both Players know how many values they are sending
or receiving. In this case the Protocol claims that Player 1 is sending all
its values to Player 2, but abstractly it can be between any two players.
After each value is sent from Player 1 to Player 2 there is an
acknowledgement. This ensures that each value is sent, received, and would be
appropriate for more advanced agreement algorithms where there was some type
of value negotiation.
= After Connection Protocol =
After the above Protocol, both sides have enough information to continue:
1. seed for Deck shuffling
2. number of Cards in the game
3. who is first and second