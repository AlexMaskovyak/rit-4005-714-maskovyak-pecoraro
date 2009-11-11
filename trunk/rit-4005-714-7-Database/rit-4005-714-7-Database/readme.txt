=======================================================================
== Assignment: Database Assignment 9, Week 10 ==
== Author: Alex Maskovyak & Joe Pecoraro ==
== Date: 2009/11/11 ==
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

== WPF Application Project ==
/app/*					-- wpf application project
app.config				-- service configuration info 
Window1.xaml				-- wpf view
Window1.xaml.cs				-- wpf code-behind

== Controller Project ==
/controller/*				-- mvc controller logic
Controller.cs				-- connection for model and view
Switcher.cs				-- allows for the toggling
						between several controllers
WorkQueue.cs				-- runs pairs of jobs

== Database Project ==
/db/*					-- basic database model
db.cs					-- conforms to the database spec
						from assignment 7

== DocProject ==
/Doc/*					-- DocProject directory for all
						non-service projects

== IModel Project ==
/IModel/*				-- holds facade interface
IModel.cs				-- facade interface for a view

== LocalDB Project ==
/local/*				-- implementation of 
						IModel<string>
										
== Web Site ==
/page/*					-- asp application
Default.aspx				-- web site view
Default.aspx.cs				-- code behind

== RemoteDB Project ==
/remote/*				-- holds remote proxy which 
						accesses a web service db
cell.cs					-- thread-safe container
RemoteDB.cs				-- implements IModel<string>
						accessing a web service
										
== Service Project ==
/service/*				-- holds database web service
clientaccesspolicy.xml			-- permissions/access config
Service.asmx				-- service code

== Silverlight Project ==
/silverlight/*				-- silverlight application
MainPage.xaml				-- silverlight view
MainPage.xaml.cs			-- code behind



=======================================================================
== 2. Resources Used for Development ==
=======================================================================
OS:			Windows XP Professional VM (on Snow Leopard)
			Windows Vista 64-bit
EXE Environment:	Powershell
Language:		C# 3.5
IDE:			Microsoft Visual Studio 9.0
SVN Repository:		Googlecode
	(http://code.google.com/p/rit-4005-714-maskovyak-pecoraro/)
SVN Plugin:		AnkhSVN
SVN Management:		TortoiseSVN

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

This will add the projects contained therein.  

The projects can be built by right-clicking the project names and 
selecting "Build".


=======================================================================
== 4. Executing the Program ==
=======================================================================
= General =
Building a project will create an executable in the "bin/Debug/".  The
nature of the web applications requires running through the Visual
Studio debugger.
 

= Service Start =
Select the service project and select Service.asmx as the file to 
view and select ctrl+F5 "start without debugging".  This will start the
service in the background allowing for testing of RemoteDB in both the
web application and the silverlight page.


= Web App Run =
Ensure the the Web Service is running.

Go to the "page" project and select Default.aspx.  Select ctrl+F5
"start without debugging".  This will bring up a web browser containing
the web-form based application.


= Silverlight run =
Ensure that the Web Service is running.

Go to "silverlight" project and select MainPage.xaml.  Select ctrl+F5
"start without debugging".  This will bring up a web browser containing
the silverlight based application.



=======================================================================
== 5. Design Decisions ==
=======================================================================
We decided to embrace the Professor's solution and abandon our own due
to the ease with which new views could be connected to databases via
the Switcher and Controller objects.  

Initially, we had attempted to integrate the Switcher and Controller 
with our own DB code.  This proved to be quite problematic and required 
an inordinate amount of code changes to accomplish.  The separation of
the controller from the view logic made the creation of the web site
and the Silverlight application to go very smoothly aside from issues
associated with permissions.



=======================================================================
== 6. CRC ==
=======================================================================

namespace ATS.Database {

  class DB<T>                   flat database with T arrays as records

        Add(match, entry)       replace matching records by new one
                DB.Delete

        Delete(match)           remove matching records

        Extract(match, report)  report on matching records


  class LocalDB<T>:IModel       wrap and sync DB as IModel

        Count.get               current number of records
                DB.list.Count
        
        Enter(tuple)            add (non-blank) record
                LocalDB.Simplify
                LocalDB.Matcher
                DB.Add
        
        Remove(keys)            remove matching records
                LocalDB.Matcher
                DB.Delete
                
        Search(keys)            return matching records (fields per key)
                DB.Extract
                LocalDB.Matcher 

        Simplify(tuple)         turn invisible strings to null

        Matcher(keys)           create lambda to match exactly, treat null/blank as wildcard
                LocalDB.Simplify

  class Program                 test both, DB and IModel
  
        Main(args)
                Program.Test
        
        Test(imodel:db, args)   connect to console or args[0]
        
        Test(imodel:db, input, output)
                DB
                IModel
}
                
---------------------------------------------------------------------------------

class Service                           web service, thread-safe delegating to LocalDB

        Service                         first time: create LocalDB
        
        Count.get                       delegate to LocalDB instance
        Enter(tuple)
        Remove(keys)
        Search(keys)
                LocalDB

namespace ATS.Database {

  class RemoteDB: IModel<string>
        
        RemoteDB                connect handlers for completion events
        Count.get               delegated to service
        Enter(tuple)            delegated to service
        Remove(keys)            delegated to service
        Search(keys)            delegated to service
                remote.ServiceSoapClient
                                for service connection
                Cell            for asynchronous value transfer
        
        Main(args)              test program for remote connection
                Program.Test

  class Window1                 view
  
        Window1                 set up GUI, setup local and remote DB, connect to switcher
                Switcher
                WorkQueue
                LocalDB
                RemoteDB
  
  class Access: IAccess         wraps control for text access
  
        Access(get,set)         convenience constructor, store get/set handlers
        Text.get                get text from control
        Text.set                put text into control

  class Controller              non-blocking controller for a IModel
                                start interaction in bg, complete in event thread
                                use WorkQueue to handle chains of interactions
        
        Controller(imodel, workQueue)
        Controller(imodel, workQueue, enable, io)
                Connect
        
        Connect(enable, io)     remember enable and io access functions
                Access
                
        doSize                  event handler, executes request asynchronously
        doSearch
        doEnter
        doRemove
        first                   retrieve keys (first lines) via access functions
                Access
                WorkQueue

  class Switcher                        control one or more databases
        
        Switcher(workQueue, name, imodel, ...)
        Switcher(enable, io, setClick, workQueue, name, imodel, ...)
                                        construct with handlers to enable,
                                        transfer, connect events, threading,
                                        and a list of models
                Connect
                Controller
        
        Connect(enable, io, setClick)   connect controllers, delegate event handlers
		Current							gets the current connetor index
			get
			set
		
  class WorkQueue                       runs pairs of jobs in bg/event thread

        Enqueue(threadStart[])          add one or more pairs to queue, start
                Run

        Run                             execute one burst of jobs,
                                        first all bg, then all event thread work

  class Cell<T>                         thread-safe producer/consumer pattern

        Value.get                       consumer
        Value.set                       producer
}


---------------------------------------------------------------------------------

namespace ATS.Database {

   class _Default: System.Web.UI.Page 	asp web application
	
		Page_Load						sets the state of the switcher for session
				Switcher
			
		Page_Unload						stores switcher state for current session
				Switcher
				
		Page_Init						setup initial session and application dbs
				Switcher
				RemoteDB
				LocalDB
				
   class MainPage: UserControl			silverlight application
		
		MainPage()
				Switcher
				RemoteDB
				LocalDB
}