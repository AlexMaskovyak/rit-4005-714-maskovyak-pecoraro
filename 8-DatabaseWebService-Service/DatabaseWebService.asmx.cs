using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;

using _7_Database;

namespace _8_DatabaseWebService
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class DatabaseWebService : System.Web.Services.WebService, IModel<string>
    {
        /// <summary> key into Application to get the database object. </summary>
        protected const string Database = "Database";

        /// <summary> default constructor. </summary>
        public DatabaseWebService() {
            Application.Lock();
            try {
                if (Application[Database] == null) {
                    Application[Database] = new LocalDB();
                }
            }
            finally {
                Application.UnLock();
            }
        }

        /// <summary> size of entries in database. </summary>
        public int Size {
            [WebMethod]
            get { return ((IModel<string>)Application[Database]).Size; }
        }

        /// <summary> finds matching tuples. </summary>
        /// <returns> words to be shown in each field. </returns>
        [WebMethod]
        public string[][] Search(string[] keys) {
            return ((IModel<string>)Application[Database]).Search(keys);
        }
        
        /// <summary> adds (or replaces) a tuple. </summary>
        /// <returns> true if something was added (not replaced). </returns>
        [WebMethod]
        public bool Enter(string[] tuple) {
            return ((IModel<string>)Application[Database]).Enter(tuple);
        }

        /// <summary> removes tuples. </summary>
        /// <returns> returns true if something was removed. </returns>
        [WebMethod]
        public bool Remove(string[] keys) {
            return ((IModel<string>)Application[Database]).Remove(keys);
        }
    }
}
