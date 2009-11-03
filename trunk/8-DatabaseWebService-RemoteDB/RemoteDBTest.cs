using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using _7_Database;

namespace _8_DatabaseWebService
{
    /// <summary> provides tests to remote object via standard-in. </summary>
    public class RemoteDBTest : LocalDBTest {

// Constructor and db factory

        /// <summary> default constructor. </summary>
        public RemoteDBTest() : base() { }

        /// <summary> this tests a RemoteDB database. </summary>
        /// <returns> a new database. </returns>
        protected override IDB<string> CreateDatabase() {
            return (IDB<string>)new RemoteDB();
        }


// Driver

        /// <summary> test the db. </summary>
        /// <param name="args"> command line arguments </param>
        public static new void Main(string[] args) {
            new RemoteDBTest().Run();
        }
    }
}
