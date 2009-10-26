using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _7_Database {

    // Generic Acccess to a DB from the command line
    public class CommandLineDBAccess<T> {

        /// <summary> the database </summary>
        protected DB<T> _db;

// Constructors

        /// <summary> default constructor. </summary>
        public CommandLineDBAccess() {
            _db = createDatabase();
        }

// Factory Methods

        /// <summary> factory for creating the Database </summary>
        /// <returns> a new DB </returns>
        protected virtual DB<T> createDatabase() {
            return new DB<T>();
        }

// Methods

        /// <summary> driver </summary>
        public virtual void Run() {
            List<string> commands = ReadUntilBlankOrEnd();
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
                } else {
                    rows.Add(input);
                }
            }
            return rows;
        }

// Template Methods

        /// <summary> template method for running commands. </summary>
        /// <param name="commands"> the command lines </param>
        protected virtual void RunCommands(List<string> commands) {
            // meant to be overridden in a subclass
        }

    }
}
