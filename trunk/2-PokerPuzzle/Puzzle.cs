using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using _1_Poker;

namespace _2_PokerPuzzle
{
    /// <summary>
    /// Holds a series of PlayingCards and is capable of determining the best possible PokerHand that can
    /// be created from those hands.  SHOULD THIS INHERIT FROM POKERHAND?
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
            //_playingCards.Count
        }

        /// <summary>
        /// Attempts to add the PlayingCards to this Puzzle.  The cards will only be added if the hand is not currently full or if it is
        /// a card that this hand does not already contain.
        /// </summary>
        /// <param name="cards">PlayingCards to be added.</param>
        /// <returns>True if all of the cards were added, false otherwise.</returns>
        public bool Add(params PlayingCard[] cards)
        {
            bool result = true;
            foreach (PlayingCard card in cards)
            {
                if (_maxSize == _playingCards.Count) {
                    throw new System.ArgumentException(String.Format("This Puzzle may only contain {0} cards.", _maxSize));
                }

                if (_playingCards.Contains(card)) {
                    throw new System.ArgumentException("A Puzzle may only contain unique cards.");
                }

                result = result && _playingCards.Add(card);
            }

            return result;
        }

        /// <summary>
        /// Used for testing and project requirements.
        /// </summary>
        public void Main() {
            int cardMax = 7;

            Puzzle puzzle = new Puzzle(cardMax);
            Deck deck = new Deck();
            deck.Shuffle();

            int cardNum = 1;
            foreach (PlayingCard card in deck) {

            }
        }
    }
}
