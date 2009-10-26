using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _7_Database
{
    /// <summary> interface for a tuple repository. </summary>
    public interface IDB<T> {

        /// <summary> adds a tuple. </summary>
        /// <remarks>
        ///   the "match" predicate function may match multiple rows. this
        ///   performs a <c>Delete(match)</c> first, and then adds the new tuple.
        /// </remarks>
        /// <param name="match"> delegate which specifies how a tuple can match. </param>
        /// <param name="tuple"> tuple to add. </param>
        /// <returns> 
        ///   true if there was a tuple determined to be equal by the "match" predicate
        ///   which the new tuple replaced. false if no tuples were replaced.
        /// </returns>
        bool Add( Predicate<T[]> match, T[] tuple );
 
        /// <summary> 
        ///   produces an array of values by taking all the tuples that
        ///   match the provided "match" predicate and running them through
        ///   the "report" function to tranform the tuple to a desired representation.
        /// </summary>
        /// <param name="match"> delegate specifying if a tuple matches. </param>
        /// <param name="result"> delegate describing how to represent an extracted tuple. </param>
        /// <returns> an array, possibly empty, of transformed tuples. </returns>
        R[] Extract<R>( Predicate<T[]> match, Func<T[], R> report );

        /// <summary> removes all tuples selected by the match argument. </summary>
        /// <param name="match"> delegate specifying if a tuple matches. </param>
        /// <returns> number of tuples that were removed. </returns>
        int Delete( Predicate<T[]> match );
    }
}
