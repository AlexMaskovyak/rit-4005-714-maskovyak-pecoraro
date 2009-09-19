using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using _1_Poker;

namespace _2_PokerPuzzle
{
    /// <summary>
    /// Holds a series of PlayingCards and is capable of determining the best possible PokerHand that can
    /// be created from those hands. 
    /// </summary>
    class Puzzle
    {
        private HashSet<PlayingCard> _playingCards;
        private int _maxSize;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="maxSize">Maximum number of cards for this puzzle to hold.</param>
        public Puzzle(int maxSize) {
            _playingCards = new HashSet<PlayingCard>();
            _maxSize = maxSize;
        }

        /// <summary>
        /// Adds a PlayingCard to this puzzle.
        /// </summary>
        /// <param name="card">PlayingCard to add.</param>
        public void Add(PlayingCard card) {
        }
    }
}
