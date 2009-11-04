using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using _7_Database;
using _8_DatabaseWebService.service;

using DatabaseWebServiceSoapClient = _8_DatabaseWebService.service.DatabaseWebServiceSoapClient;

namespace _8_DatabaseWebService {
    
    /// <summary> serves as a proxy for calls on a database webservice. </summary>
    /// <remarks> asynchronously communicates with the server; fielding only one request at a time. </remarks>
    public class RemoteDB : LocalDB {

// fields

        /// <summary> reference to the web service. </summary>
        protected DatabaseWebServiceSoapClient _service;

        /// <summary> current result from an event. </summary>
        protected Cell<object> _asyncResult;

// constructors
        
        /// <summary> default constructor. </summary>
        public RemoteDB() {
            // setup service and events
            _service = new DatabaseWebServiceSoapClient();
            _service.get_SizeCompleted += new EventHandler<get_SizeCompletedEventArgs>( (object sender, get_SizeCompletedEventArgs args) => _asyncResult.Value = args.Result );
            _service.SearchCompleted += new EventHandler<SearchCompletedEventArgs>( (object sender, SearchCompletedEventArgs args) => _asyncResult.Value = args.Result );
            _service.EnterCompleted += new EventHandler<EnterCompletedEventArgs>( (object sender, EnterCompletedEventArgs args) => _asyncResult.Value = args.Result );
            _service.RemoveCompleted += new EventHandler<RemoveCompletedEventArgs>( (object sender, RemoveCompletedEventArgs args) => _asyncResult.Value = args.Result );

            // initialize fields
            _asyncResult = new Cell<object>();
            _monitor = new object();
        }

// IModel interface

        /// <summary> entries in database. </summary>
        public override int Size { 
            get {
                // do asynchronous call
                _service.get_SizeAsync();
                // wait for result
                return (int)_asyncResult.Value;
            }  
        }
        
        /// <summary> finds matching tuples. </summary>
        /// <returns> words to be shown in each field. </returns>
        public override string[][] Search(string[] keys) {
            // do asynchronous call
            ArrayOfString corrected = new ArrayOfString();
            corrected.AddRange(keys);
            _service.SearchAsync(corrected);

            // wait for result and transform it
            ArrayOfString[] result = (ArrayOfString[])_asyncResult.Value;
            string[][] correctedResult = new string[result.Length][];
            for (int i = 0; i < result.Length; ++i) {
                correctedResult[i] = result[i].ToArray();
            }

            return correctedResult;
        }
        
        /// <summary> adds (or replaces) a tuple. </summary>
        /// <returns> true if something was added (not replaced). </returns>
        public override bool Enter(string[] tuple) {
            // do asynchronous call
            ArrayOfString corrected = new ArrayOfString();
            corrected.AddRange(tuple);
            _service.EnterAsync(corrected);
            // wait for result and return it
            return (bool)_asyncResult.Value;
        }
        
        /// <summary> removes tuples. </summary>
        /// <returns> returns true if something was removed. </returns>
        public override bool Remove(string[] keys) {
            // do asynchronous call
            ArrayOfString corrected = new ArrayOfString();
            corrected.AddRange(keys);
            _service.RemoveAsync(corrected);
            // wait for result and return it
            return (bool)_asyncResult.Value;
        }
    }
}
