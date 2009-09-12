using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poker {

    /// <summary>
    ///   The <c>Card</c> type represents an individual Playing Card.
    /// </summary>
    /// <remarks>
    ///   <para>
    ///     A <c>Card</c> type consists of a <c>Card.Suit</c> <c>Suit</c>
    ///     and <c>int</c> <c>Rank</c> that are readonly once created.
    ///     Static methods are available to determine the suit or rank
    ///     from a <c>char</c>.
    ///   </para>
    /// </remarks>
    class Card : IComparable<Card> {

        /// <summary>
        ///   Acceptable Card Suits
        /// </summary>
        public enum Suits { Club, Diamond, Heart, Spade };

        // Members
        readonly Suits m_suit;
        readonly int m_rank;

        /// <summary>
        ///   Constructor from a two character string.
        /// </summary>
        /// <param name="value">
        ///   A two character string representing the Card.
        /// </param>
        public Card(string value) {
            m_rank = getRankFromChar(value[0]);
            m_suit = getSuitFromChar(value[1]);
        }


        public Card.Suits Suit {
            get { return m_suit; }
        }

        public int Rank {
            get { return m_rank; }
        }

        public override string ToString() {
            switch (m_suit) {
                case Suits.Club:    return m_rank + " of Clubs";
                case Suits.Diamond: return m_rank + " of Diamonds";
                case Suits.Heart:   return m_rank + " of Hearts";
                case Suits.Spade:   return m_rank + " of Spades";
                default:            return "Unknown";
            }
        }

        /// <summary>
        ///   The appropriate rank value for a <c>char</c>.
        /// </summary>
        /// <param name="rank">character representation of a rank</param>
        /// <returns>the numeric value for that rank</returns>
        static int getRankFromChar(char rank) {
            switch (rank) {
                case '1': return 1;
                case '2': return 2;
                case '3': return 3;
                case '4': return 4;
                case '5': return 5;
                case '6': return 6;
                case '7': return 7;
                case '8': return 8;
                case '9': return 9;
                case 'T': return 10;
                case 'J': return 11;
                case 'Q': return 12;
                case 'K': return 13;
                case 'A': return 14;
                default: throw new ArgumentException();
            }
        }

        /// <summary>
        ///   The appropriate suit for a <c>char</c>.
        /// </summary>
        /// <param name="suit">character representation of a suit</param>
        /// <returns>the <c>Card.Suit</c> value for that suit</returns>
        static Suits getSuitFromChar(char suit) {
            switch (suit) {
                case 'C': return Suits.Club;
                case 'D': return Suits.Diamond;
                case 'H': return Suits.Heart;
                case 'S': return Suits.Spade;
                default: throw new ArgumentException();
            }
        }

        public int CompareTo(Card other) {
            if (m_rank < other.Rank)
                return -1;
            if (m_rank > other.Rank)
                return 1;
            if (m_suit < other.Suit)
                return -1;
            if (m_suit > other.Suit)
                return 1;
            return 0;
        }

    }
}
