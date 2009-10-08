using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using _1_Poker;

namespace _5_SelectingAWinner_ConsoleApplication
{
    /// <summary>PlayingCard whose value/ordering is influenced by its Suit first, then its rank.</summary>
    public class SuitPrecedencePlayingCard : PlayingCard
    {
        /// <summary>New ordering of Suit values for SuitPrecedencePlayingCard.</summary>
        public new enum Suits { Club = 1, Spade, Heart, Diamond };

        protected new Suits _suit;
        protected new Ranks _rank;

        /// <summary>
        /// Obtains the suit of this card.
        /// </summary>
        public new Suits Suit {
            get { return _suit; }
        }

        /// <summary>
        /// Obtains the face value of this card.
        /// </summary>
        /// <returns>The card's value.</returns>
        public new Ranks Rank {
            get { return _rank; }
        }

        //public SuitPrecedencePlayingCard(Ranks rank, Suits suit) : base(rank, suit) {}

        public SuitPrecedencePlayingCard(string rankSuitPair) : base(rankSuitPair) { }

        public SuitPrecedencePlayingCard(char rank, char suit) : base(rank, suit) { }

        /// <summary>
        /// Support IComparable, allow for the comparison of this PlayingCard with the specified PlayingCard.
        /// </summary>
        /// <param name="card">PlayingCard to compare against this one.</param>
        /// <returns>
        ///     -1 if this PlayingCard is less than the one specified, 1 if this PlayingCard is greater than
        ///     the one specified, and 0 if they are equivalent.  Cards are considered equivalent if they 
        ///     have identical rank and suit values.
        ///     First comparison by suit: (high to low) clubs, spades, hearts, and diamonds
        ///     Second comparison by value: (high to low) king, quen, jack, ten...two.
        /// </returns>
        public virtual int CompareTo(SuitPrecedencePlayingCard card) {
            if (Suit < card.Suit)
                return -1;
            if (Suit > card.Suit)
                return 1;            
            if (Rank < card.Rank)
                return -1;
            if (Rank > card.Rank)
                return 1;
            return 0;
        }
    }
}
