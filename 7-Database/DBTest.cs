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
            bool added = _db.Add(CreateSimpleMatcher(newRecord), newRecord);
        }

        /// <summary> processes a delete command, removing a tuple matching the recordTemplate </summary>
        /// <param name="recordTemplate"> arguments after "delete" </param>
        protected virtual void ProcessDeleteCommand(string[] args) {
            int field = int.Parse(args[0]);
            string regexString = args[1];
            int deleteCount = _db.Delete(CreateSingleFieldRegexMatcher(field, regexString));
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
            string[] extracted = _db.Extract(CreateSingleFieldRegexMatcher(field, regexString), delegate(string[] record) {
                return (desiredFieldIndex < record.Length) ? record[desiredFieldIndex] : null;
            });

            // Output
            Console.WriteLine(String.Join(",", extracted));
        }

// Delegate Factory Methods

        /// <summary> factory method to create a predicate matcher for a string[] record </summary>
        /// <param name="newRecord"> the record to be matching against </param>
        /// <returns> a predicate returning true when a given record matches newRecord </returns>
        protected virtual Predicate<string[]> CreateSimpleMatcher(string[] recordTemplate) {
            int length = recordTemplate.Length;
            return delegate(string[] record) {
                if (length != record.Length)
                    return false;

                for (int i = 0; i < length; ++i) {
                    if (record[i] != recordTemplate[i])   // NOTE: != is okay for strings
                        return false;
                }

                return true;
            };
        }

        /// <summary> factory method to create a predicate matcher for a single field </summary>
        /// <param name="field"> the field index </param>
        /// <param name="str"> the pattern to match against the field value </param>
        /// <returns> true if matched, false otherwise </returns>
        protected virtual Predicate<string[]> CreateSingleFieldRegexMatcher(int field, string str) {
            Regex regex = new Regex(str);
            return delegate(string[] record) {
                return (field < record.Length && regex.IsMatch(record[field]));
            };
        }

// Not Currently Used...

        /// <summary> factory method to create a predicate matcher for a string[] recordTemplate containing regexes </summary>
        /// <param name="newRecord"> the record to be matching against </param>
        /// <returns> a predicate returning true when a given record matches newRecord </returns>
        protected virtual Predicate<string[]> CreateRegexMatcher(string[] recordTemplate) {

            // Close over regexes and length once and use them in the returned closure
            int length = recordTemplate.Length;
            Regex[] regexes = Array.ConvertAll<string, Regex>(recordTemplate, delegate(string str) {
                if (str == null || str.Length == 0)
                    return new Regex(".*");   // wildcard match anything
                return new Regex(str);        // input was a regex
            });

            // Predicate that runs regexes against each string in the records
            return delegate(string[] record) {
                if (length != record.Length)
                    return false;

                for (int i = 0; i < length; ++i) {
                    if (!regexes[i].IsMatch(record[i]))
                        return false;
                }

                return true;
            };

        }

// Driver

        /// <summary> test the db. </summary>
        /// <param name="args"></param>
        public static void Main(string[] args) {
            new DBTest().Run();
        }
    }
}
