using System;
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
        private PlayingCard[] _playingCards;

        /// <summary>
        /// Default cosntructor for creating a hand of playing cards or a 5-card variant of poker.
        /// </summary>
        /// <param name="playingCards">The playing cards to add to this hand.  0 through 5 cards may be passed in.</param>
        public PokerHand( params PlayingCard[] playingCards )
        {
            if ( playingCards.Length > 5 ) 
            {
                throw new System.ArgumentException( "A PokerHand may only be constructed with 0 to 5 playing cards." );
            }

            _playingCards = playingCards;
        }
    }
}
