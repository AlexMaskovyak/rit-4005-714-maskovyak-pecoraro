using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _7_Database
{
    /// <summary> interface for a tuple repository. </summary>
    public interface IDB<T> {
        /// <summary> adds a tuple. </summary>
        /// <param name="match"> delegate which specifies how a tuple can match. </param>
        /// <param name="tuple"> tuple to add. </param>
        /// <returns> 
        /// true if it adds but does not replace tuple with equal 
        /// content as determined by the match argument, false otherwise. 
        /// </returns>
        bool Add( Predicate<T[]> match, T[] tuple );
 
        /// <summary> 
        /// extracts an array of values where each value is constructed 
        /// by the report argument from one tuple selected by the 
        /// match argument. 
        /// </summary>
        /// <param name="match"> delegate specifying if a tuple matches. </param>
        /// <param name="result"> delegate describing how to construct an extracted tuple </param>
        /// <returns> an aray, possibly empty, of value. </returns>
        T[] Extract( Predicate<T[]> match, Func<T[], T> result );

        /// <summary> removes all tuples selected by the match argument. </summary>
        /// <param name="match"> delegate specifying if a tuple matches. </param>
        /// <returns> number of tuples that were removed. </returns>
        int Delete( Predicate<T[]> match);
    }
}
