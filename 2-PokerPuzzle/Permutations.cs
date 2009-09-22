using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2_PokerPuzzle {

    /// <summary>
    /// Creates all permutations of a specified length for a given list of elements.
    /// </summary>
    /// <typeparam name="T">Type held by the lists to permute.</typeparam>
    class Permutations<T> {

        protected List<List<T>> _permutations;

        public List<List<T>> Perms {
            get { return _permutations; }
        }

        public Permutations(List<T> list, int cnt) {
            _permutations = generatePermutations(list, cnt);
        }

        private List<T> listWithoutElem(List<T> list, T removeElem) {
            List<T> dup = new List<T>();
            foreach (T elem in list) {
                if (!elem.Equals(removeElem)) {
                    dup.Add(elem);
                }
            }
            return dup;
        }

        private List< List<T> > generatePermutations(List<T> list, int cnt) {
            List<List<T>> allPerms = new List<List<T>>();

            // Base Cases - Empty or Don't Want Any More
            if (list.Count == 0 || cnt <= 0) {
                allPerms.Add(new List<T>());
                return allPerms;
            }

            // Base Cases - One Element
            if (list.Count == 1) {
                List<T> simpleList = new List<T>();
                simpleList.Add(list[0]);
                allPerms.Add(simpleList);
                return allPerms;
            }
            
            // Recursive Cases
            foreach (T elem in list) {
                List<List<T>> lowerPermutations = generatePermutations(listWithoutElem(list, elem), (cnt-1));
                foreach (List<T> perm in lowerPermutations) {
                    perm.Add(elem);
                    allPerms.Add(perm);
                }
            }

            return allPerms;
        }
    }
}
