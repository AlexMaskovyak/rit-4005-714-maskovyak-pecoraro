using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _7_Database
{
    /// <summary> test of DB functionality. </summary>
    public class DBTest {

        protected DB<string> _db;

        /// <summary> default constructor. </summary>
        public DBTest() {

            _db = new DB<string>();

            // read commands
            List<string> commands = ReadUntilBlankOrEnd();

            // execute commands
            RunCommands(commands);
        }

        /// <summary>Reads lines from Standard Input until the first blank line</summary>
        /// <returns>The list of lines</returns>
        protected virtual List<string> ReadUntilBlankOrEnd() {
            string input;
            List<string> rows = new List<string>();
            while ((input = Console.ReadLine()) != null) {
                input = input.Trim();
                if (input.Equals("")) {
                    break;
                }
                else {
                    rows.Add(input);
                }
            }
            return rows;
        }

        /// <summary>Process commands.</summary>
        /// <param name="commands">The command lines.</param>
        protected virtual void RunCommands(List<string> commands) {
            foreach (string line in commands) {

                // Split the Arguments to determine the action
                string[] arguments = line.Split('\t');
                
                // try to process this through the commands that are known
                ProcessAddCommand(arguments);
            }
        }

        /// <summary> processes an add command, adding a tuple of 0 or more words to the database. </summary>
        /// <param name="arguments"> 
        /// has the form: "add", word*
        /// </param>
        protected virtual void ProcessAddCommand(string[] arguments) {
            // check to ensure we know what to do with this
            if( !arguments[0].Equals("add") ) {
                return;
            }


            _db.Add(
                new Predicate<string[]>(
                    delegate(string[] dbtuple, string tupleToAdd) {
                        foreach (string word in tuple) {

                        }
                        return true; 
                    } 
                ),
                arguments.Skip(1).ToArray<string>());
        }


        /// <summary> 
        /// processes an extract command on the database, dispalying a value field from all 
        /// tuples with a key matching a regular expression; fields are numbered from 0. 
        /// </summary>
        /// <param name="arguments">  
        /// has the form: "extract" key-position pattern value-position
        /// </param>
        protected virtual void ProcessExtractCommand(string[] arguments) {
            // check to ensure we know what to do
            if( !arguments[0].Equals("extract") ) {
                return;
            }

            _db.Extract(
                new Predicate<string[]>( delegate(string[] tuple) { return true; } ),
                new Func<string[], string>( 
                    delegate(string[] tuple) { return ""; } ) );
            //new delegate Func<> {}
        }

        /// <summary> test the db. </summary>
        /// <param name="args"></param>
        public static void Main(string[] args) {


        }
    }
}
