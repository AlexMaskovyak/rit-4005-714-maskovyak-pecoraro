using System;
using System.Collections.Generic;

namespace ATS.Database {
  /// <summary> implements a flat database with T arrays. </summary>
  public class DB<T> {
    /// <summary> delegate, contains the tuples. </summary>
    protected readonly List<T[]> list = new List<T[]>();

    /// <summary> add (or replace) a tuple. </summary>
    /// <returns> true if a tuple with matching content is replaced. </returns>
    public virtual bool Add (Predicate<T[]> match, T[] entry) {
      // remove any equal tuples if necessary
      var result = 0 < Delete(match);

      // add the new tuple
      list.Add(entry);
      return result;
    }

    /// <summary> report information from matched tuples. </summary>
    public virtual R[] Extract<R> (Predicate<T[]> match, Func<T[], R> report) {
      var result = new List<R>();
      foreach (var tuple in list)
        if (match(tuple)) result.Add(report(tuple));
      return result.ToArray();
    }

    /// <summary> remove matching tuples. </summary>
    public virtual int Delete (Predicate<T[]> match) {
#if SILVERLIGHT
      var clone = new List<T[]>();
      foreach (var tuple in list)
      if (!match(tuple)) clone.Add(tuple);
      int result = list.Count - clone.Count;
      if (result > 0) {
        list.Clear();
        foreach (var tuple in clone) list.Add(tuple);
      }
      return result;
#else
      return list.RemoveAll(match);
#endif
    }
  }
}
