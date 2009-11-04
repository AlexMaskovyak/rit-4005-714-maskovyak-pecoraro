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
            _service.get_SizeCompleted += new EventHandler<get_SizeCompletedEventArgs>(Handle_getSizeCompleted);
            _service.SearchCompleted += new EventHandler<SearchCompletedEventArgs>(Handle_SearchCompleted);
            _service.EnterCompleted += new EventHandler<EnterCompletedEventArgs>(Handle_EnterCompleted);
            _service.RemoveCompleted += new EventHandler<RemoveCompletedEventArgs>(Handle_RemoveCompeted);

            // initialize fields
            _asyncResult = new Cell<object>();
            _monitor = new object();
        }

// event handlers

        /// <summary> handler for size method call returns. </summary>
        /// <param name="sender"> object which triggered this handler. </param>
        /// <param name="args"> values associated with this completed event. </param>
        protected void Handle_getSizeCompleted(object sender, get_SizeCompletedEventArgs args) {
                // get result and notify everyone waiting for a value
                _asyncResult.Value = args.Result;
        }

        /// <summary> handler for search method call returns. </summary>
        /// <param name="sender"> object which triggered this handler. </param>
        /// <param name="args"> values associated with this completed event. </param>
        protected void Handle_SearchCompleted(object sender, SearchCompletedEventArgs args) {
                // get result and notify everyone waiting for a value
                _asyncResult.Value = args.Result;
        }

        /// <summary> handler for enter method call returns. </summary>
        /// <param name="sender"> object which triggered this handler. </param>
        /// <param name="args"> values associated with this completed event. </param>
        protected void Handle_EnterCompleted(object sender, EnterCompletedEventArgs args) {
                // get result and notify everyone waiting for a value
                _asyncResult.Value = args.Result;
        }

        /// <summary> handler for remove method call returns. </summary>
        /// <param name="sender"> object which triggered this handler. </param>
        /// <param name="args"> values associated with this completed event. </param>
        protected void Handle_RemoveCompeted(object sender, RemoveCompletedEventArgs args) {
                // get result and notify everyone waiting for a value
                _asyncResult.Value = args.Result;
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
