using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using _7_Database;

using DatabaseWebServiceSoapClient = _8_DatabaseWebService.DatabaseWebServiceSoapClient;

namespace _8_DatabaseWebService {
    
    /// <summary> serves as a proxy for calls on a database webservice. </summary>
    /// <remarks> asynchronously communicates with the server, and fields only one request at a time. </remarks>
    public class RemoteDB : IModel<string> {

// fields
        protected DatabaseWebServiceSoapClient _service;

// constructors

        public RemoteDB()
        {
            _service = new DatabaseWebServiceSoapClient();
            _service.get_SizeCompleted += new EventHandler<get_SizeCompletedEventArgs>(Handle_getSizeCompleted);
            _service.SearchAsync += new EventHandler<SearchCompletedEventArgs>(Handle_SearchCompleted);
            _service.EnterAsync += new EventHandler<EnterCompletedEventArgs>(Handle_EnterCompleted);
            _service.RemoveAsync += new EventHandler<RemoveCompletedEventArgs>(Handle_RemoveCompeted);

            //_service.get_SizeCompleted += new EventHandler<_8_DatabaseWebService_RemoteDB._8_DatabaseWebService.get_SizeCompletedEventArgs
           
        }

// event handlers

        /// <summary> handler for size method call returns. </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void Handle_getSizeCompleted(object sender, get_SizeCompletedEventArgs args) {

        }

        protected void Handle_SearchCompleted(object sender, SearchCompletedEventArgs args) {

        }

        protected void Handle_EnterCompleted(object sender, EnterCompletedEventArgs args) {

        }

        protected void Handle_RemoveCompeted(object sender, RemoveCompletedEventArgs args) {

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
