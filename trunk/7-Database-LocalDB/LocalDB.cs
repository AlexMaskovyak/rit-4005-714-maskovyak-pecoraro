using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _7_Database
{
    /// <summary> facade for an IModel database. </summary>
    public class LocalDB : DB<string>, IModel<string> {

// Fields
        /// <summary> provide item to lock upon for thread-safety. </summary>
        protected object Monitor = new object();

// Constructors

        /// <summary> default constructor. </summary>
        public LocalDB() : base() { }

// IModel interface

        /// <summary> entries in database. </summary>
        public virtual int Size {
            get { return _tuples.Count; }
        }

        /// <summary> finds matching tuples. </summary>
        /// <param name="keys"> search keys. </param>
        /// <returns> words to be shown in each field. </returns>
        public virtual string[][] Search(string[] keys) {
            IList<string[]> result = new List<string[]>();
            lock (Monitor){
                for (int i = 0; i < keys.Length; ++i) {
                    string[] field =
                        base.Extract<string>(
                            DBDelegateFactory.CreateRegexMatcher(keys),
                            DBDelegateFactory.CreateIndexExtractingReporter(i));
                    result.Add(field);
                }
            }

            return result.ToArray<string[]>();
        }

        /// <summary> adds (or replaces) a tuple. </summary>
        /// <param name="tuple"> input keys. </param>
        /// <returns> true if something was added (not replaced). </returns>
        public virtual bool Enter(string[] tuple) {
            lock (Monitor) {
                return !base.Add(DBDelegateFactory.CreateRegexMatcher(tuple), tuple);
            }
        }

        /// <summary> removes tuples. </summary>
        /// <param name="keys"> search keys. </param>
        /// <returns> returns true if something was removed. </returns>
        public virtual bool Remove(string[] keys) {
            lock (Monitor) {
                return (0 < base.Delete(DBDelegateFactory.CreateRegexMatcher(keys)));
            }
        }
    }
}
