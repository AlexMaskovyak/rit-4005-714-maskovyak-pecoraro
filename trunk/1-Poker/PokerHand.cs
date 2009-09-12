using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1_Poker
{
    /// <summary>
    /// Stores playing cards associated with a 5-card hand of poker.
    /// </summary>
    public class PokerHand
    {
        /// <summary>
        /// Largest quantity of cards allowed in a 5-card hand of poker.
        /// </summary>
        public const int MAX_HAND_SIZE = 5;

        private HashSet<PlayingCard> _playingCards;

        /// <summary>
        /// Default cosntructor for creating a hand of playing cards or a 5-card variant of poker.
        /// </summary>
        /// <param name="playingCards">The playing cards to add to this hand.  0 through 5 cards may be passed in.</param>
        public PokerHand( params PlayingCard[] playingCards )
        {
            // check size issues
            if ( playingCards == null || playingCards.Length > PokerHand.MAX_HAND_SIZE ) 
            {
                throw new System.ArgumentException( "A PokerHand may only be constructed with 0 to 5 playing cards." );
            }

            // create hashset and add cards to it
            _playingCards = new HashSet<PlayingCard>();
            foreach (PlayingCard card in playingCards)
            {
                _playingCards.Add(card);
            }

            // the number of cards in the set should match the number of cards passed in
            // otherwise we have duplicates!
            if( _playingCards.Count != playingCards.Length )
            {
                throw new System.ArgumentException( "A PokerHand may only contain one of any particular card type from a standard 52-card deck!  Cheater!" );
            }
        }

        /// <summary>
        /// Clears the hand of all cards.
        /// </summary>
        public void Clear()
        {
            _playingCards.Clear();
        }
        
        /// <summary>
        /// Removes the specified playingcard from this hand.
        /// </summary>
        /// <param name="card">PlayingCard to remove.</param>
        public void Remove(PlayingCard card)
        {
            _playingCards.Remove(card);
        }

        /// <summary>
        /// Obtains the number of cards in this hand.
        /// </summary>
        /// <returns>Count of the cards in this hand.</returns>
        public int HandSize()
        {
            return _playingCards.Count;
        }

        /// <summary>
        /// Attempts to add the PlayingCards to this PokerHand.  The cards will only be added if the hand is not currently full or if it is
        /// a card that this hand does not already contain.
        /// </summary>
        /// <param name="cards">PlayingCards to be added.</param>
        /// <returns>True if all of the cards were added, false otherwise.</returns>
        public bool Add(params PlayingCard[] cards)
        {
            bool result = true;
            foreach (PlayingCard card in cards)
            {
                if (PokerHand.MAX_HAND_SIZE == HandSize())
                {
                    return false;
                }

                if (_playingCards.Contains(card))
                {
                    throw new System.ArgumentException("A PokerHand may only contain one of any particular card type from a standard 52-card deck!  Cheater!");
                }

                result = result && _playingCards.Add(card);
            }

            return result;
        }
    }
}
