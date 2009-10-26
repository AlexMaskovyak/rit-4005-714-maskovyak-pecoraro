using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace _7_Database
{
    /// <summary> test of DB functionality. </summary>
    public class DBTest {

// Fields

        /// <summary> the database </summary>
        protected DB<string> _db;

// Constructors

        /// <summary> default constructor. </summary>
        public DBTest() {
            _db = new DB<string>();
        }

// Public Methods

        /// <summary> driver </summary>
        public virtual void Run() {
            List<string> commands = ReadUntilBlankOrEnd();
            RunCommands(commands);
        }

// Protected Methods

        /// <summary>Reads lines from Standard Input until the first blank line</summary>
        /// <returns>The list of lines</returns>
        protected virtual List<string> ReadUntilBlankOrEnd() {
            string input;
            List<string> rows = new List<string>();
            while ((input = Console.ReadLine()) != null) {
                input = input.Trim();
                if (input.Equals("")) {
                    break;
                } else {
                    rows.Add(input);
                }
            }
            return rows;
        }

        /// <summary>Read all commands and process each individually.</summary>
        /// <param name="commands">The command lines.</param>
        protected virtual void RunCommands(List<string> commands) {
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
                    ProcessExtractCommand(args);
                    break;
                case "delete":
                    ProcessDeleteCommand(args);
                    break;
                case "debug":
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
        /// <param name="arguments"> arguments after "add" </param>
        protected virtual void ProcessAddCommand(string[] newRecord) {
            bool added = _db.Add(DBDelegateFactory.CreateSimpleMatcher(newRecord), newRecord);
        }

        /// <summary> processes a delete command, removing a tuple matching the recordTemplate </summary>
        /// <param name="recordTemplate"> arguments after "delete" </param>
        protected virtual void ProcessDeleteCommand(string[] args) {
            int field = int.Parse(args[0]);
            string regexString = args[1];
            int deleteCount = _db.Delete(DBDelegateFactory.CreateSingleFieldRegexMatcher(field, regexString));
        }


        /// <summary> 
        ///   processes an extract command on the database, dispalying a value field from all 
        ///   tuples with a key matching a regular expression; fields are numbered from 0. 
        /// </summary>
        /// <param name="arguments">
        ///   arguments after "extract"
        ///   has the form: key-position pattern value-position
        /// </param>
        protected virtual void ProcessExtractCommand(string[] args) {
            int field = int.Parse(args[0]);
            string regexString = args[1];
            int desiredFieldIndex = int.Parse(args[2]);
            string[] extracted = _db.Extract(
                DBDelegateFactory.CreateSingleFieldRegexMatcher(field, regexString), 
                DBDelegateFactory.CreateIndexExtractingReporter(desiredFieldIndex));

            // Output
            Console.WriteLine(String.Join(",", extracted));
        }


// Driver

        /// <summary> test the db. </summary>
        /// <param name="args"></param>
        public static void Main(string[] args) {
            new DBTest().Run();
        }
    }
}
