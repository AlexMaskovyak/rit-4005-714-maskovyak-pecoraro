using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace ATS.Database {
  /// <summary> conceal access to a control. </summary>
  public interface IAccess {
    /// <summary> field to input/output text. </summary>
    string Text { get; set; }
  }
  /// <summary> return text from control. </summary>
  public delegate string GetText ();
  /// <summary> set text into control. </summary>
  public delegate void SetText (string text);
  /// <summary> helper with convenience constructor. </summary>
  public class Access: IAccess {
    /// <summary> accessor. </summary>
    protected readonly GetText get;
    /// <summary> mutator. </summary>
    protected readonly SetText set;
    /// <summary> convenience constructor. </summary>
    public Access (GetText get, SetText set) {
      this.get = get; this.set = set;
    }
    /// <summary> provide field access. </summary>
    public virtual string Text {
      get { return get(); }
      set { set(value); }
    }
  }
  /// <summary> called to control interaction. </summary>
  public delegate void Enable (bool isEnabled);
  /// <summary> non-blocking controller for a database. </summary>
  /// <remarks> Basic idea is to access the database with a <c>BackgroundWorker</c>
  ///   and let the event thread redraw upon completion. However, when the size of the
  ///   database changes there is more interaction. The <c>WorkQueue</c> is designed
  ///   to run a sequence of jobs, first completing the backgrounds, then the
  ///   event threads. This simplifies creating the tasks in the controller. </remarks> 
  public class Controller {
    /// <summary> represents database. </summary>
    protected readonly IModel<string> db;
    /// <summary> sequential access to a <c>BackgroundWorker</c>. </summary>
    protected readonly WorkQueue bg;
    /// <summary> controls user interaction. </summary>
    protected Enable enable;
    /// <summary> get query fields. </summary>
    protected IAccess[] io;
    /// <summary> connect to database and view, post current count. </summary>
    /// <param name="db"> database. </param>
    /// <param name="bg"> for sequential background execution. </param>
    /// <param name="enable"> controls user interaction in view. </param>
    /// <param name="io"> access to current/size/search/enter/remove fields (can be null). </param>
    public Controller (IModel<string> db, WorkQueue bg, Enable enable, params IAccess[] io)
      : this(db, bg) {
      Connect(enable, io);
    }
    /// <summary> connect to database, start background thread. </summary>
    /// <param name="db"> database. </param>
    /// <param name="bg"> for sequential background execution. </param>
    public Controller (IModel<string> db, WorkQueue bg) {
      this.db = db; this.bg = bg;
    }
    /// <summary> connect to (current) view. </summary>
    /// <param name="enable"> controls user interaction in view. </param>
    /// <param name="io"> access to current/size/search/enter/remove fields (can be null). </param>
    public virtual void Connect (Enable enable, params IAccess[] io) {
      this.enable = enable; this.io = io;
    }
    /// <summary> asynchronously handle update count request. </summary>
    /// <remarks> Arguments are ignored. BUG: this should be done with an observer. </remarks>
    public virtual void doSize (object sender, EventArgs e) {
      // disable user interface
      enable(false);
      // set up work in database
      int count = 0;
      bg.Enqueue(new ThreadStart[] {
        () => { // db access
          count = db.Count;
        },
        () => { // clean up after work is completed
          if (io[1] != null) io[1].Text = count.ToString();
          enable(true);
        }});
    }
    /// <summary> asynchronously handle search request. </summary>
    public virtual void doSearch (object sender, EventArgs e) {
      // disable user interface
      enable(false);
      // set up work in database
      string[] send = first(io, 2); // in event thread
      string[][] receive = null;
      bg.Enqueue(new ThreadStart[] {
        () => { receive = db.Search(send); }, // db access
        () => { // clean up after work is completed
          for (int n = 2; n < io.Length; ++n)
            if (io[n] != null) io[n].Text = string.Join("\n", receive[n - 2]);
          enable(true);
        }});
    }
    /// <summary> asynchronously handle enter request. </summary>
    public virtual void doEnter (object sender, EventArgs e) {
      // disable user interface
      enable(false);
      // set up work in database
      string[] send = first(io, 2); // in event thread
      bool added = false;
      bg.Enqueue(new ThreadStart[] {
        () => { // db access
          added = db.Enter(send);
        },
        () => { // clean up after work is completed
          if (added) doSize(null, null);
          else enable(true);
        }});
    }
    /// <summary> handle remove request. </summary>
    public void doRemove (object sender, EventArgs e) {
      // disable user interface
      enable(false);
      // set up work in database
      string[] send = first(io, 2); // in event thread
      bool removed = false;
      bg.Enqueue(new ThreadStart[] {
        () => { // db access
          removed = db.Remove(send);
        },
        () => { // clean up after work is completed
          if (removed) {
            for (int n = 2; n < io.Length; ++n)
              if (io[n] != null) io[n].Text = "";
            doSize(null, null);
          } else enable(true);
        }});
    }
    /// <summary> safely return the first line of a <c>IAccess</c>. </summary>
    protected virtual string[] first (IAccess[] io, int offset) {
      string[] result = new string[io.Length - offset];
      for (int n = offset; n < io.Length; ++n)
        if (io[n] != null && io[n].Text != null) { // map null to null
          string s = io[n].Text.Trim();
          if (s.Length != 0) // map empty to null
            result[n - offset] = s.Contains("\n")
              ? s.Substring(0, s.IndexOf('\n')).Trim() // first line
              : s; // entire line
        }
      return result;
    }
  }
}
