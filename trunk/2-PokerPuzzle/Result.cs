using System;
using BitArray = System.Collections.BitArray;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using _1_Poker;

namespace _2_PokerPuzzle {

    /// <summary>Holds the Game Result for a Puzzle</summary>
    public class Result {

        /// <summary>Was the players Selection the best?</summary>
        private bool _best;

        /// <summary>Determined Best Hand</summary>
        private PokerHand _bestHand;

        /// <summary>Determined Best Hand</summary>
        private BitArray _bestHandBits;

        /// <summary>Selected Hand</summary>
        private PokerHand _selectedHand;

        /// <summary>Constructor</summary>
        public Result(bool best, BitArray bestHandBits, PokerHand bestHand, PokerHand selectedHand) {
            _best = best;
            _bestHandBits = bestHandBits;
            _bestHand = bestHand;
            _selectedHand = selectedHand;
        }

        /// <summary>Access Result</summary>
        public bool Best {
            get { return _best; }
        }

        /// <summary>Access the Best Hand</summary>
        public PokerHand BestHand {
            get { return _bestHand; }
        }

        /// <summary>Access the Best Hand Bits</summary>
        public BitArray BestHandIndexes {
            get { return _bestHandBits; }
        }

        /// <summary>Access Selected Hand</summary>
        public PokerHand SelectedHand {
            get { return _selectedHand; }
        }

        /// <summary>Debug Helper</summary>
        public override string ToString() {
            string str = "Best: " + _best.ToString();
            str += "\nBest Hand:\n";
            foreach (PlayingCard c in _bestHand.Cards) {
                str += c.ToString() + "\n";
            }
            str += "\nSelected Hand:\n";
            foreach (PlayingCard c in _selectedHand.Cards) {
                str += c.ToString() + "\n";
            }
            return str;
        }

    }
}
