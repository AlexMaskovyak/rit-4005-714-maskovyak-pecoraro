using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _7_Database
{
    /// <summary> implements a simplistic database interface. </summary>
    /// <typeparam name="T"> type of a word in a tuple. </typeparam>
    public class DB<T> : IDB<T> {

// Fields

        /// <summary> holds tuples. </summary>
        protected ICollection<T[]> _tuples;

// Constructor
        
        /// <summary> default constructor. </summary>
        public DB() {
            _tuples = CreateTupleCollection();
        }

// Factory methods

        protected virtual ICollection<T[]> CreateTupleCollection() {
            return new List<T[]>();
        }

// IDB Interface

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
        public virtual bool Add(Predicate<T[]> match, T[] tuple) {
            int removed = Delete(match); // may delete multiples
            _tuples.Add(tuple);          // always add
            return (removed > 0);        // indicate if we replaced something
        }

        /// <summary> 
        ///   produces an array of values by taking all the tuples that
        ///   match the provided "match" predicate and running them through
        ///   the "report" function to tranform the tuple to a desired representation.
        /// </summary>
        /// <param name="match"> delegate specifying if a tuple matches. </param>
        /// <param name="result"> delegate describing how to represent an extracted tuple. </param>
        /// <returns> an array, possibly empty, of transformed tuples. </returns>
        public virtual R[] Extract<R>(Predicate<T[]> match, Func<T[], R> result) {
            ICollection<R> values = new List<R>();
            foreach( T[] tuple in _tuples ) {
                if (match.Invoke(tuple)) {
                    values.Add(result.Invoke(tuple));
                }
            }
            return values.ToArray<R>();
        }

        /// <summary> removes all tuples selected by the match argument. </summary>
        /// <param name="match"> delegate specifying if a tuple matches. </param>
        /// <returns> number of tuples that were removed. </returns>
        public virtual int Delete( Predicate<T[]> match) {
            List<T[]> toRemove = new List<T[]>();
            foreach( T[] tuple in _tuples ) {
                if (match.Invoke(tuple)) {
                    toRemove.Add(tuple);
                }
            }

            foreach (T[] tuple in toRemove) {
                _tuples.Remove(tuple);
            }

            return toRemove.Count;
        }

// Debug

        /// <summary> useful for debugging </summary>
        /// <returns> print out the database </returns>
        public override string ToString() {
            return DB<T>.TuplesAsString(_tuples);
        }

        /// <summary> useful for debugging </summary>
        /// <param name="tuples"> prettyprint a list of tuples </param>
        /// <returns></returns>
        public static string TuplesAsString(ICollection<T[]> tuples) {
            StringBuilder str = new StringBuilder();
            str.Append("[ ");
            bool first = true;
            foreach (T[] tuple in tuples) {
                if (!first) str.Append(",\n  ");
                str.Append(DB<T>.TupleAsString(tuple));
                first = false;
            }
            str.Append(" ]");
            return str.ToString();
        }

        /// <summary> useful for debugging </summary>
        /// <param name="tuple"> pretty-print a tuple </param>
        /// <returns> a string representation of a tuple </returns>
        public static string TupleAsString(T[] tuple) {
            StringBuilder str = new StringBuilder();
            str.Append("{");
            str.Append(String.Join(",", Array.ConvertAll<T, string>(tuple, delegate(T e) { return e.ToString(); })));
            str.Append("}");
            return str.ToString();
        }

    }
}
