using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _7_Database {

    /// <summary> Generic Acccess to a DB from the command line </summary>
    /// <typeparam name="T"> type of database. </typeparam>
    public abstract class CommandLineDBAccess<T> {

        /// <summary> the database </summary>
        protected IDB<T> _db;

// Constructors

        /// <summary> default constructor. </summary>
        public CommandLineDBAccess() {
            _db = CreateDatabase();
        }

// Factory Methods

        /// <summary> factory for creating the Database </summary>
        /// <returns> a new DB </returns>
        protected abstract IDB<T> CreateDatabase();

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
        protected abstract void RunCommands(List<string> commands);

    }
}
