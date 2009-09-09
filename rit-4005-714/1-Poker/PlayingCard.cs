using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1_Poker
{
    /// <summary>
    /// Card holds values associated with a physical playing card.
    /// </summary>
    public class PlayingCard
    {
        /// <summary>
        /// Playing card suits.
        /// </summary>
        public enum Suit { Club, Diamond, Heart, Spade }
        
        /// <summary>
        /// Playing card ranks/values.  The "number" of the card.
        /// </summary>
        public enum Rank { Two=2, Three=3, Four=4, Five=5, Six=6, Seven=7, Eight=8, Nine=9, Ten=10, Jack=11, Queen=12, King=13, Ace=14 }

        private PlayingCard.Suit _suit;
        private PlayingCard.Rank _rank;

        /// <summary>
        /// Default constructor.  Defines the two necessary components of a playing card.
        /// </summary>
        /// <param name="rank">Playing card's rank.</param>
        /// <param name="suit">Playing card's suit.</param>
        public PlayingCard( PlayingCard.Rank rank, PlayingCard.Suit suit ) 
        {
            _rank = rank;
            _suit = suit;
        }
        
        /// <summary>
        /// Override to return the full spoken/written name of a playing card.
        /// </summary>
        /// <returns>The contemporary spoken/written name of a playing card.</returns>
        public override string ToString()
        {
            return String.Format( "{0} of {1}s", _rank, _suit );
        }

    }
}
