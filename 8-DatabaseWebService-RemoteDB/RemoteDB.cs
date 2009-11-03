using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using _7_Database;

using DatabaseWebServiceSoapClient = _8_DatabaseWebService.DatabaseWebServiceSoapClient;

namespace _8_DatabaseWebService {
    
    /// <summary> serves as a proxy for calls on a database webservice. </summary>
    /// <remarks> asynchronously communicates with the server; fielding only one request at a time. </remarks>
    public class RemoteDB : LocalDB {

// fields

        /// <summary> reference to the web service. </summary>
        protected DatabaseWebServiceSoapClient _service;

        /// <summary> current result from an event. </summary>
        protected object _asyncResult;

        /// <summary> controls access via locks. </summary>
        protected object _monitor;

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
            _asyncResult = null;
            _monitor = new object();
        }

// event handlers

        /// <summary> handler for size method call returns. </summary>
        /// <param name="sender"> object which triggered this handler. </param>
        /// <param name="args"> values associated with this completed event. </param>
        protected void Handle_getSizeCompleted(object sender, get_SizeCompletedEventArgs args) {
            lock (_monitor) {
                // get result and notify everyone waiting for a value
                _asyncResult = args.Result;
                Monitor.PulseAll(_monitor);
            }
        }

        /// <summary> handler for search method call returns. </summary>
        /// <param name="sender"> object which triggered this handler. </param>
        /// <param name="args"> values associated with this completed event. </param>
        protected void Handle_SearchCompleted(object sender, SearchCompletedEventArgs args) {
            lock (_monitor) {
                // get result and notify everyone waiting for a value
                _asyncResult = args.Result;
                Monitor.PulseAll(_monitor);
            }
        }

        /// <summary> handler for enter method call returns. </summary>
        /// <param name="sender"> object which triggered this handler. </param>
        /// <param name="args"> values associated with this completed event. </param>
        protected void Handle_EnterCompleted(object sender, EnterCompletedEventArgs args) {
            lock (_monitor) {
                // get result and notify everyone waiting for a value
                _asyncResult = args.Result;
                Monitor.PulseAll(_monitor);
            }
        }

        /// <summary> handler for remove method call returns. </summary>
        /// <param name="sender"> object which triggered this handler. </param>
        /// <param name="args"> values associated with this completed event. </param>
        protected void Handle_RemoveCompeted(object sender, RemoveCompletedEventArgs args) {
            lock (_monitor) {
                // get result and notify everyone waiting for a value
                _asyncResult = args.Result;
                Monitor.PulseAll(_monitor);
            }
        }

// IModel interface

        /// <summary> entries in database. </summary>
        public override int Size { 
            get {
                lock (_monitor) {
                    // do asynchronous call
                    _service.get_SizeAsync();
                    // wait for result
                    Monitor.Wait(_monitor);
                    // grab it
                    int result = (int)_asyncResult;
                    // give someone else access
                    Monitor.Pulse(_monitor);

                    return result;
                }
            }  
        }
        
        /// <summary> finds matching tuples. </summary>
        /// <returns> words to be shown in each field. </returns>
        public override string[][] Search(string[] keys) {
            lock (_monitor) {
                // do asynchronous call
                ArrayOfString corrected = new ArrayOfString();
                corrected.AddRange(keys);
                _service.SearchAsync(corrected);
                // wait for result
                Monitor.Wait(_monitor);
                // grab it
                string[][] result = (string[][])_asyncResult;
                // give someone else access
                Monitor.Pulse(_monitor);

                return result;
            } 
        }
        
        /// <summary> adds (or replaces) a tuple. </summary>
        /// <returns> true if something was added (not replaced). </returns>
        public override bool Enter(string[] tuple) {
            lock (_monitor) {
                // do asynchronous call
                ArrayOfString corrected = new ArrayOfString();
                corrected.AddRange(tuple);
                _service.EnterAsync(corrected);
                // wait for result
                Monitor.Wait(_monitor);
                // grab it
                bool result = (bool)_asyncResult;
                // give someone else access
                Monitor.Pulse(_monitor);

                return result;
            } 
        }
        
        /// <summary> removes tuples. </summary>
        /// <returns> returns true if something was removed. </returns>
        public override bool Remove(string[] keys) {
            lock (_monitor) {
                // do asynchronous call
                ArrayOfString corrected = new ArrayOfString();
                corrected.AddRange(keys);
                _service.RemoveAsync(corrected);
                // wait for result
                Monitor.Wait(_monitor);
                // grab it
                bool result = (bool)_asyncResult;
                // give someone else access
                Monitor.Pulse(_monitor);

                return result;
            }
        }
    }
}
