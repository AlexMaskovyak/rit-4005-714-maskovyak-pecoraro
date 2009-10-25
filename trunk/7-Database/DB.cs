using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _7_Database
{
    /// <summary> implements a simplistic database interface. </summary>
    /// <typeparam name="T"> type of a word in a tuple. </typeparam>
    public class DB<T> : IDB<T> {

        /// <summary> holds tuples. </summary>
        protected ICollection<T[]> _tuples;
        
        /// <summary> default constructor. </summary>
        public DB() {
            _tuples = new List<T[]>();
        }

        /// <summary> adds a tuple. </summary>
        /// <param name="match"> delegate which specifies how a tuple can match. </param>
        /// <param name="tuple"> tuple to add. </param>
        /// <returns> 
        /// true if it adds and replaces a tuple with equal content 
        /// as determined by the match argument, false otherwise. 
        /// </returns>
        public virtual bool Add(Predicate<T[]> match, T[] tuple) {
            foreach (T[] t in _tuples) {
                if (match.Invoke(t)) {
                    return false;
                }
            }

            _tuples.Add(tuple);
            
            return true;
        }

        /// <summary> 
        /// extracts an array of values where each value is constructed 
        /// by the report argument from one tuple selected by the 
        /// match argument. 
        /// </summary>
        /// <param name="match"> delegate specifying if a tuple matches. </param>
        /// <param name="result"> delegate describing how to construct an extracted tuple </param>
        /// <returns> an aray, possibly empty, of value. </returns>
        public virtual T[] Extract(Predicate<T[]> match, Func<T[], T> result) {
            ICollection<T> values = new List<T>();
            foreach( T[] tuple in _tuples ) {
                if (match.Invoke(tuple)) {
                    values.Add(result.Invoke(tuple));
                }
            }
            return values.ToArray<T>();
        }

        /// <summary> removes all tuples selected by the match argument. </summary>
        /// <param name="match"> delegate specifying if a tuple matches. </param>
        /// <returns> number of tuples that were removed. </returns>
        public virtual int Delete( Predicate<T[]> match) {
            int removed = 0;
            foreach( T[] tuple in _tuples ) {
                if (match.Invoke(tuple)) {
                    _tuples.Remove(tuple);
                    ++removed;
                }
            }
            return removed;
        }
    }
}
