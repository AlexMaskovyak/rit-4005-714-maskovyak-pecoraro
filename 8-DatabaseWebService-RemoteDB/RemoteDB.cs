using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using _7_Database;

using DatabaseWebServiceSoap = _8_DatabaseWebService_RemoteDB._8_DatabaseWebService.DatabaseWebServiceSoap;

namespace _8_DatabaseWebService {
    
    /// <summary> serves as a proxy for calls on a database webservice. </summary>
    public class RemoteDB : IModel<string> {

// fields
        DatabaseWebServiceSoap _service;

// constructors

        public RemoteDB()
        {
            _service = new DatabaseWebServiceSoap();
           
        }

// IModel interface

        /// <summary> entries in database. </summary>
        public int Size { 
            get { throw new NotImplementedException(); }  
        }
        
        /// <summary> finds matching tuples. </summary>
        /// <returns> words to be shown in each field. </returns>
        public string[][] Search(string[] keys) {
            throw new NotImplementedException(); 
        }
        
        /// <summary> adds (or replaces) a tuple. </summary>
        /// <returns> true if something was added (not replaced). </returns>
        public bool Enter(string[] tuple) {
            throw new NotImplementedException(); 
        }
        
        /// <summary> removes tuples. </summary>
        /// <returns> returns true if something was removed. </returns>
        public bool Remove(string[] keys) {
            throw new NotImplementedException(); 
        }

        public static void Main(string[] args) {
        }
    }
}
