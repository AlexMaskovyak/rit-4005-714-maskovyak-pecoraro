using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace _7_Database
{
    /// <summary> contains methods for producing delegates to be used with the IDB interface. </summary>
    public abstract class DBDelegateFactory {

        // Delegate Factory Methods

        /// <summary> factory method to create a predicate matcher for a T[] record </summary>
        /// <param name="recordTemplate"> the record to be matching against </param>
        /// <returns> a predicate returning true when a given record matches newRecord </returns>
        public static Predicate<string[]> CreateSimpleMatcher(string[] recordTemplate) {
            int length = recordTemplate.Length;
            return delegate(string[] record) {
                if (length != record.Length)
                    return false;

                for (int i = 0; i < length; ++i) {
                    if (record[i] != recordTemplate[i])   // NOTE: != is okay for strings
                        return false;
                }

                return true;
            };
        }

        /// <summary> factory method to create a predicate matcher for a T[] record with wildcards </summary>
        /// <remarks> wildcards are specified by a null element in the array </remarks>
        /// <param name="recordTemplate"> the record to be matching against </param>
        /// <returns> a prediate returning true when a given record matches newRecord </returns>
        public static Predicate<string[]> CreateWildCardMatcher(string[] recordTemplate) {
            int length = recordTemplate.Length;
            return delegate(string[] record) {
                for (int i = 0; i < length; ++i) {
                    if (record[i] != null && record[i] != recordTemplate[i])
                        return false;
                }

                return true;
            };
        }

        /// <summary> factory method to create a predicate matcher for a single field </summary>
        /// <param name="field"> the field index </param>
        /// <param name="str"> the pattern to match against the field value </param>
        /// <returns> true if matched, false otherwise </returns>
        public static Predicate<string[]> CreateSingleFieldRegexMatcher(int field, string str)
        {
            Regex regex = new Regex(str);
            return delegate(string[] record) {
                return (field < record.Length && regex.IsMatch(record[field]));
            };
        }

        /// <summary> factory method to create a predicate matcher for a string[] recordTemplate containing regexes </summary>
        /// <param name="newRecord"> the record to be matching against </param>
        /// <returns> a predicate returning true when a given record matches newRecord </returns>
        public static Predicate<string[]> CreateRegexMatcher(string[] recordTemplate) {

            // Close over regexes and length once and use them in the returned closure
            int length = recordTemplate.Length;
            Regex[] regexes = Array.ConvertAll<string, Regex>(recordTemplate, delegate(string str) {
                if (str == null || str.Length == 0)
                    return new Regex(".*");   // wildcard match anything
                return new Regex(str);        // input was a regex
            });

            // Predicate that runs regexes against each string in the records
            return delegate(string[] record) {
                if (length != record.Length)
                    return false;

                for (int i = 0; i < length; ++i) {
                    if (!regexes[i].IsMatch(record[i]))
                        return false;
                }

                return true;
            };

        }

        /// <summary> creates a reporter which extracts the specified word from the tuple. </summary>
        /// <param name="desiredFieldIndex"> index of the word to extract. </param>
        /// <returns> a func which extracts a word from a tuple </returns>
        public static Func<string[], string> CreateIndexExtractingReporter(int desiredFieldIndex) {
            return delegate(string[] record) {
                return (desiredFieldIndex < record.Length) ? record[desiredFieldIndex] : null;
            };
        }
    }
}
