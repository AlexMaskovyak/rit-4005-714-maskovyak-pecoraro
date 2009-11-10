using System;
using System.Threading;
using ATS;
using ATS.Database.remote;

namespace ATS.Database {
  /// <summary> facade to delegate the <c>IModel</c> methods. </summary>
  /// <remarks> Only one call can be pending. All methods will block the caller.
  ///   This asynchronous version is needed for Silverlight.
  ///   It acts like a <c>Cell</c> with the server as producer and caller as consumer. </remarks>
  public class RemoteDB: IModel<string> {
    /// <summary> server handle. </summary>
    protected ServiceSoapClient client = new ServiceSoapClient();
    /// <summary> monitor for single access. </summary>
    protected readonly object mutex = new object();
    /// <summary> place for value exchange. </summary>
    protected readonly Cell<object> cell = new Cell<object>();

    /// <summary> connect event handlers once. </summary>
    public RemoteDB () {
      client.get_CountCompleted += (sender, args) => { cell.Value = args.Result; };
      client.SearchCompleted += (sender, args) => { cell.Value = args.Result; };
      client.EnterCompleted += (sender, args) => { cell.Value = args.Result; };
      client.RemoveCompleted += (sender, args) => { cell.Value = args.Result; };
    }

    /// <summary> delegated to service. </summary>
    public int Count {
      get {
        lock (mutex) {
          // contact service asynchronously
          client.get_CountAsync();
          // consume value
          return (int)cell.Value;
        }
      }
    }

    /// <summary> delegated to service. </summary>
    public string[][] Search (string[] keys) {
      lock (mutex) {
        // contact service asynchronously
        var send = new remote.ArrayOfString();
        send.AddRange(keys);
        client.SearchAsync(send);
        // consume value
        var receive = (ArrayOfString[])cell.Value;
        var result = new string[receive.Length][];
        for (int n = 0; n < receive.Length; ++n)
          result[n] = receive[n].ToArray();
        return result;
      }
    }
    /// <summary> delegated to service. </summary>
    public bool Enter (string[] tuple) {
      lock (mutex) {
        // contact service asynchronously
        var send = new remote.ArrayOfString();
        send.AddRange(tuple);
        client.EnterAsync(send);
        // consume value
        return (bool)cell.Value;
      }
    }

    /// <summary> delegated to service. </summary>
    public bool Remove (string[] keys) {
      lock (mutex) {
        // contact service asynchronously
        var send = new remote.ArrayOfString();
        send.AddRange(keys);
        client.RemoveAsync(send);
        // consume value
        return (bool)cell.Value;
      }
    }
#if !SILVERLIGHT
    /// <summary> process using console i/o. </summary>
    /// <remarks> Can pass an input file name as argument to simplify debugging. </remarks>
    public static void Main (string[] args) {
        new RemoteDB().Test(args);
    }
#endif
  }
}
