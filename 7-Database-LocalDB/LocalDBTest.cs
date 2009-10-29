using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _7_Database {

    /// <summary> test class for LocalDB. </summary>
    public class LocalDBTest : DBTest {

// Constructor and Factory for DB

        /// <summary> default constructor. </summary>
        public LocalDBTest() : base() { }

        /// <summary> this tests a LocalDB database. </summary>
        /// <returns> a new database. </returns>
        protected override IDB<string> CreateDatabase() {
            return new LocalDB();
        }


// Override Command Processing

        /// <summary> process a single command, template method </summary>
        /// <param name="action"> the command's action </param>
        /// <param name="args"> the command's arguments </param>
        protected override void ProcessCommand(string action, string[] args) {
            switch (action) {
                case "enter":
                    bool didRemoveSomething = Enter(args);
                    Console.WriteLine(didRemoveSomething);
                    break;
                case "search":
                    string[][] found = Search(args);
                    Console.WriteLine(DB<string>.TuplesAsString(found));
                    break;
                case "remove":
                    bool didRemove = Remove(args);
                    Console.WriteLine(didRemove);
                    break;
                case "size":  // custom action
                    Console.WriteLine(Size);
                    break;
                case "debug": // custom action
                    ProcessDebugCommand(args);
                    break;
                default:
                    Console.Error.WriteLine("Unknown Command: {0}", action);
                    break;
            }
        }

        /// <summary> entries in database. </summary>
        protected virtual int Size {
            get { return ((IModel<string>) _db).Size; }
        }

        /// <summary> finds matching tuples. </summary>
        /// <returns> words to be shown in each field. </returns>
        protected virtual string[][] Search(string[] keys) {
            return ((IModel<string>)_db).Search(keys);
        }

        /// <summary> adds (or replaces) a tuple. </summary>
        /// <returns> true if something was removed. </returns>
        protected virtual bool Enter(string[] tuple) {
            return ((IModel<string>)_db).Enter(tuple);
        }

        /// <summary> removes tuples. </summary>
        /// <returns> returns true if something was removed. </returns>
        protected virtual bool Remove(string[] keys) {
            return ((IModel<string>)_db).Remove(keys);
        }

// Driver

        /// <summary> test the db. </summary>
        /// <param name="args"> command line arguments </param>
        public static new void Main(string[] args) {
            new LocalDBTest().Run();
        }

    }

}
