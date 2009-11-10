using System;
using System.Collections.Generic;
using System.Text;

namespace ATS.Database {
  /// <summary> wraps and synchronizes <c>DB</c> as <c>IModel</c>. </summary>
  public class LocalDB<T>: DB<T>, IModel<T> {

    /// <summary> number of tuples, delegate to <c>list</c>. </summary>
    public int Count { get { lock(this) return list.Count; } }

    /// <returns> <c>[i][..]</c> fields found for i'th key. </returns>
    public T[][] Search (T[] keys) {
      // obtain all matched tuples
      object[] report;
      lock (this)
        report = Extract(Matcher(keys), tuple => (object)tuple);

      // repackage per key
      var result = new T[keys.Length][];
      for (int n = 0; n < keys.Length; ++ n)
        result[n] = new T[report.Length];

      for (int r = 0; r < report.Length; ++ r) {
        T[] tuple = (T[])report[r];
        for (int n = 0; n < keys.Length; ++ n)
          result[n][r] = tuple[n];
      }
      return result;
    }

    /// <remarks> Disallows adding records with empty fields. </remarks>
    /// <returns> true if something was added (and not replaced). </returns>
    public bool Enter (T[] tuple) {
      Simplify(tuple);
      foreach (T t in tuple)
        if (t == null) return false;
      lock (this)
        return !Add(Matcher(tuple), tuple);
    }

    /// <returns> true if something was removed. </returns>
    public bool Remove (T[] keys) {
      lock (this)
        return Delete(Matcher(keys)) > 0;
    }

    /// <summary> turn invisible elements into null. </summary>
    /// <remarks> For string tuples, blank and empty fields are set to null. </remarks>
    protected virtual void Simplify (T[] tuple) {
      for (int n = 0; n < tuple.Length; ++n)
        if (tuple[n] as string != null
            && (tuple[n] as string).Trim().Length == 0)
          tuple[n] = default(T);
    }

    /// <summary> match non-empty keys for <c>Equals</c>. </summary>
    /// <remarks> Just match prefix of longer tuples; never match shorter tuples. </remarks>
    protected virtual Predicate<T[]> Matcher (T[] keys) {
      Simplify(keys);
      return tuple => {
        for (int n = 0; n < keys.Length; ++n)
          if (tuple.Length <= n
              || keys[n] != null && !keys[n].Equals(tuple[n]))
            return false;
        return true;
      };
    }
  }
}

