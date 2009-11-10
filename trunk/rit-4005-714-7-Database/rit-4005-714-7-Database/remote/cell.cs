using System;
using System.Threading;

namespace ATS {

  /// <summary> producer/consumer pattern. </summary>
  public class Cell<T> {

    /// <summary> value to be passed. </summary>
    private T content;

    /// <summary> state of <c>content</c>. </summary>
    private bool full;

    /// <summary> thread-safe access to <c>content</c>. </summary>
    public T Value {
      get {
        lock (this) {
          while (!full) Monitor.Wait(this);
          full = false;
          Monitor.PulseAll(this);
          return content;
        }
      }
      set {
        lock (this) {
          while (full) Monitor.Wait(this);
          full = true;
          Monitor.PulseAll(this);
          content = value;
        }
      }
    }
  }
}
