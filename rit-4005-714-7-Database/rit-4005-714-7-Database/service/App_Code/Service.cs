using System.Web.Services;
using ATS.Database;

[WebService(Namespace = "http://www.cs.rit.edu/~ats/db")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Service: System.Web.Services.WebService, IModel<string> {

  /// <summary> self-initialize: allocate a <c>LocalDB</c> as <c>Application["db"]</c>. </summary>
  public Service () {
    Application.Lock();
    try {
      if (Application["db"] == null) Application["db"] = new LocalDB<string>();
    }
    finally {
      Application.UnLock();
    }
  }

  public int Count {
    [WebMethod]
    get { return ((IModel<string>)Application["db"]).Count; }
  }

  [WebMethod]
  public string[][] Search (string[] keys) {
    return ((IModel<string>)Application["db"]).Search(keys);
  }

  [WebMethod]
  public bool Enter (string[] tuple) {
    return ((IModel<string>)Application["db"]).Enter(tuple);
  }

  [WebMethod]
  public bool Remove (string[] keys) {
    return ((IModel<string>)Application["db"]).Remove(keys);
  }
}