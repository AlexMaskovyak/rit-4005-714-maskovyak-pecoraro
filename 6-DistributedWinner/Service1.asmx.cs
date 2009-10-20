using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;

using Axel.Conversions;

namespace _6_DistributedWinner
{
    /// <summary>
    /// Summary description for Service
    /// </summary>
[WebService(Namespace = "http://www.cs.rit.edu/axel/conversions/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Service1 : System.Web.Services.WebService
    {
        IReversibleFunction c2f;
        IFunction f2c;


        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        public Service1 () {
            c2f = new ReversibleLinearFunction(9.0 / 5.0, 32.0);
            f2c = c2f.inverse();
        }

        [WebMethod]
        public double Celsius (double fahrenheit) {
            return f2c.Y(fahrenheit);
        }
        [WebMethod]
        public double Fahrenheit (double celsius) {
            return c2f.Y(celsius);
        }
    }
}
