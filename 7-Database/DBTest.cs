using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace _7_Database
{
    /// <summary> test of DB functionality. </summary>
    public class DBTest : CommandLineDBAccess<string> {

// Constructor

        /// <summary> default constructor. </summary>
        public DBTest() : base() { }

// Protected Methods

        /// <summary>Read all commands and process each individually.</summary>
        /// <param name="commands">The command lines.</param>
        protected override void RunCommands(List<string> commands) {
            foreach (string line in commands) {

                // Split the Arguments to determine the action and args
                string[] arguments = line.Split('\t');
                string action = arguments[0];
                string[] actionArguments = arguments.Skip(1).ToArray<string>();
                
                // try to process this through the commands that are known
                Console.WriteLine("> " + line);
                ProcessCommand(action, actionArguments);
                Console.WriteLine();

            }
        }

        /// <summary> process a single command, template method </summary>
        /// <param name="action"> the command's action </param>
        /// <param name="args"> the command's arguments </param>
        protected virtual void ProcessCommand(string action, string[] args) {
            switch (action) {
                case "add":
                    ProcessAddCommand(args);
                    break;
                case "extract":
                    string[] extracted = ProcessExtractCommand(args);
                    Console.WriteLine(String.Join(",", extracted));
                    break;
                case "delete":
                    ProcessDeleteCommand(args);
                    break;
                case "debug": // custom action
                    ProcessDebugCommand(args);
                    break;
                default:
                    Console.Error.WriteLine("Unknown Command: {0}", action);
                    break;
            }
        }

        /// <summary> summary of the db </summary>
        /// <param name="args"> ignored arguments </param>
        protected virtual void ProcessDebugCommand(string[] args) {
            Console.WriteLine(_db.ToString());
        }

        /// <summary> processes an add command, adding a tuple of 0 or more words to the database. </summary>
        /// <param name="newRecord"> arguments after "add" </param>
        protected virtual bool ProcessAddCommand(string[] newRecord) {
            return _db.Add(DBDelegateFactory.CreateSimpleMatcher(newRecord), newRecord);
        }

        /// <summary> processes a delete command, removing a tuple matching the recordTemplate </summary>
        /// <param name="args"> arguments after "delete" </param>
        protected virtual int ProcessDeleteCommand(string[] args) {
            int field = int.Parse(args[0]);
            string regexString = args[1];
            return _db.Delete(DBDelegateFactory.CreateSingleFieldRegexMatcher(field, regexString));
        }


        /// <summary> 
        ///   processes an extract command on the database, dispalying a value field from all 
        ///   tuples with a key matching a regular expression; fields are numbered from 0. 
        /// </summary>
        /// <param name="args">
        ///   arguments after "extract"
        ///   has the form: key-position pattern value-position
        /// </param>
        protected virtual string[] ProcessExtractCommand(string[] args) {
            int field = int.Parse(args[0]);
            string regexString = args[1];
            int desiredFieldIndex = int.Parse(args[2]);
            return _db.Extract(
                DBDelegateFactory.CreateSingleFieldRegexMatcher(field, regexString), 
                DBDelegateFactory.CreateIndexExtractingReporter(desiredFieldIndex));
        }

// Driver

        /// <summary> test the db. </summary>
        /// <param name="args"> command line arguments. </param>
        public static void Main(string[] args) {
            new DBTest().Run();
        }
    }
}
