using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _7_Database
{
    /// <summary> abstracts <c>DB</c> for a view. </summary>
    public interface IModel<T> {
        /// <summary> entries in database. </summary>
        int Size { get; }
        /// <summary> finds matching tuples. </summary>
        /// <returns> words to be shown in each field. </returns>
        T[][] Search(T[] keys);
        /// <summary> adds (or replaces) a tuple. </summary>
        /// <returns> true if something was added (not replaced). </returns>
        bool Enter(T[] tuple);
        /// <summary> removes tuples. </summary>
        /// <returns> returns true if something was removed. </returns>
        bool Remove(T[] keys);
    }

}
