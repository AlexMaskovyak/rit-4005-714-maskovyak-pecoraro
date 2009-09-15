using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1_Poker
{
    /// <summary>
    /// Score allows for PokerHands to be ranked and compared with one another.
    /// </summary>
    public class Score : IComparable<Score> {

        /// <summary>
        /// Levels of a PokerHand hand rankings, from Lowest to Highest.
        /// </summary>
        public enum HandRanks {
            HighCard = 1, Pair, TwoPair, ThreeKind, Straight,
            Flush, FullHouse, FourKind, StraightFlush
        }

        // Members
        private HandRanks m_handRank;
        private PlayingCard.Ranks m_highCard;

        /// <summary>
        /// Create a new Score from a PokerHand.
        /// </summary>
        /// <param name="hand">PokerHand whose contents are to be scored.</param>
        public Score(PokerHand hand) {
            PlayingCard[] cards = hand.Cards;
            Array.Sort(cards);

            m_handRank = HandRanks.HighCard;
            m_highCard = PlayingCard.Ranks.NAR;

            bool ignored = IsStraightFlush(cards)
                || IsFourOfAKind(cards)
                || IsFullHouse(cards)
                || IsFlush(cards)
                || IsStraight(cards)
                || isThreeOfAKind(cards)
                || isTwoPair(cards)
                || isPair(cards)
                || isHighCard(cards);
        }

        /// <summary>
        /// Obtains the hand ranking of this Score object.
        /// </summary>
        public HandRanks Level {
            get { return m_handRank; }
        }

        /// <summary>
        /// Obtains the sub-rank of this hand ranking, the high card.
        /// </summary>
        public PlayingCard.Ranks HighCard {
            get { return m_highCard; }
        }

        /// <summary>
        /// Support IComparable, allow for the comparison of this Score with another Score.
        /// </summary>
        /// <param name="other">Score to compare against this one.</param>
        /// <returns>
        ///     -1 if this Score is less than the one specified, 1 if this Score is greater than
        ///     the one specified, and 0 if they are equivalent.  Scores are considered equivalent
        ///     if they have the same hand ranking and sub-ranking (high card).
        /// </returns>
        public int CompareTo(Score other) {
            if (m_handRank < other.Level)
                return -1;
            if (m_handRank > other.Level)
                return 1;
            if (m_highCard < other.HighCard)
                return -1;
            if (m_highCard > other.HighCard)
                return 1;
            return 0;
        }

        /// <summary>
        /// The full spoken name of this hand ranking and the high card contained within it.
        /// </summary>
        /// <returns>The written name of the Poker Score.</returns>
        public override string ToString() {
            switch (m_handRank) {
                case HandRanks.StraightFlush: return "Straight Flush " + m_highCard;
                case HandRanks.FourKind:      return "Four of a Kind " + m_highCard;
                case HandRanks.FullHouse:     return "Full House " + m_highCard;
                case HandRanks.Flush:         return "Flush " + m_highCard;
                case HandRanks.Straight:      return "Straight " + m_highCard;
                case HandRanks.ThreeKind:     return "Three of a Kind " + m_highCard;
                case HandRanks.TwoPair:       return "Two Pair " + m_highCard;
                case HandRanks.Pair:          return "Pair " + m_highCard;
                case HandRanks.HighCard:      return "High Card " + m_highCard;
                default:                      return "Unknown";
            }
        }

        /// <summary>
        /// Determines whether the cards form a straight flush: 5 cards of sequential rank of the same suit.
        /// </summary>
        /// <param name="cards">Cards in a hand.</param>
        /// <returns>True if the cards form a straight flush, false otherwise.</returns>
        private bool IsStraightFlush(PlayingCard[] cards) {
            for (int i = 1; i < cards.Length; ++i) {
                PlayingCard prev = cards[i - 1];
                PlayingCard curr = cards[i];
                if (prev.Suit != curr.Suit)
                    return false;
                if (prev.Rank != (curr.Rank - 1))
                    return false;
            }

            m_handRank = HandRanks.StraightFlush;
            m_highCard = cards[cards.Length - 1].Rank;
            return true;
        }

        // Check for Four of a Kind
        /// <summary>
        /// Determines whether the cards form "four-of-a-kind": 4 cards with the same rank.
        /// </summary>
        /// <param name="cards">Cards in a hand.</param>
        /// <returns>True if the cards form a four-of-a-kind, false otherwise.</returns>
        private bool IsFourOfAKind(PlayingCard[] cards) {
            int count = 0;
            PlayingCard.Ranks val = cards[2].Rank;
            for (int i = 0; i < cards.Length; ++i) {
                if (cards[i].Rank == val) {
                    count++;
                }
            }

            if (count != 4) {
                return false;
            }

            m_handRank = HandRanks.FourKind;
            m_highCard = val;
            return true;
        }

        /// <summary>
        /// Determines whether the cards form a full house: a pair, and three of a kind.
        /// </summary>
        /// <param name="cards">Cards in a hand.</param>
        /// <returns>True if the cards form a full-house, false otherwise.</returns>
        private bool IsFullHouse(PlayingCard[] cards) {
            PlayingCard.Ranks low = cards[0].Rank;
            int lowCount = 0;
            PlayingCard.Ranks high = cards[cards.Length - 1].Rank;
            int highCount = 0;
            for (int i = 0; i < cards.Length; ++i) {
                PlayingCard.Ranks rank = cards[i].Rank;
                if (rank != low && rank != high) {
                    return false;
                }
                if (rank == low) {
                    lowCount++;
                } else {
                    highCount++;
                }
            }

            m_handRank = HandRanks.FullHouse;
            m_highCard = (lowCount == 3) ? low : high;
            return true;
        }

        /// <summary>
        /// Determines whether the cards form a flush: all cards share the same suit.
        /// </summary>
        /// <param name="cards">Cards in a hand.</param>
        /// <returns>True if the cards form a flush, false otherwise.</returns>
        private bool IsFlush(PlayingCard[] cards) {
            PlayingCard.Suits suit = cards[0].Suit;
            for (int i = 0; i < cards.Length; ++i) {
                if (cards[i].Suit != suit) {
                    return false;
                }
            }

            m_handRank = HandRanks.Flush;
            m_highCard = cards[cards.Length - 1].Rank;
            return true;
        }

        /// <summary>
        /// Determines whether the cards form a straight: 5 cards of sequential rank with an Ace as a hig-card (low-card is disallowed), 
        /// around the corner is disallowed.
        /// </summary>
        /// <param name="cards">Cards of a hand.</param>
        /// <returns>True if the cards form a straight, false otherwise.</returns>
        private bool IsStraight(PlayingCard[] cards) {
            for (int i = 1; i < cards.Length; ++i) {
                PlayingCard prev = cards[i - 1];
                PlayingCard curr = cards[i];
                if (prev.Rank != (curr.Rank - 1)) {
                    return false;
                }
            }

            m_handRank = HandRanks.Straight;
            m_highCard = cards[cards.Length - 1].Rank;
            return true;
        }

        /// <summary>
        /// Determines whether the cards form 3-of-a-kind: the hand possesses 3 cards with matching ranks.
        /// </summary>
        /// <param name="cards">Cards of a hand.</param>
        /// <returns>True if the hand form 3-of-a-kind, false otherwise.</returns>
        private bool isThreeOfAKind(PlayingCard[] cards) {
            int count = 0;
            int count2 = 0;
            PlayingCard.Ranks val = cards[2].Rank;
            PlayingCard.Ranks val2 = cards[3].Rank;
            for (int i = 0; i < cards.Length; ++i) {
                if (cards[i].Rank == val) {
                    count++;
                }
                if (cards[i].Rank == val2) {
                    count2++;
                }
            }

            if (count < 3 && count2 < 3) {
                return false;
            }

            m_handRank = HandRanks.ThreeKind;
            m_highCard = (count == 3) ? val : val2;
            return true;
        }

        /// <summary>
        /// Determines whether this hand contains 2-Pair: 2 cards that share a matching rank, and 2 cards that share a different matching
        /// rank.
        /// </summary>
        /// <param name="cards">Cards of a hand.</param>
        /// <returns>True if this hand contain 2-Pair, false otherwise.</returns>
        private bool isTwoPair(PlayingCard[] cards) {
            PlayingCard.Ranks pair = PlayingCard.Ranks.NAR;
            PlayingCard.Ranks pair2 = PlayingCard.Ranks.NAR;
            for (int i = 1; i < cards.Length; ++i) {
                PlayingCard pred = cards[i - 1];
                PlayingCard curr = cards[i];
                if (pred.Rank == curr.Rank) {
                    if (pair == PlayingCard.Ranks.NAR) {
                        pair = curr.Rank;
                        i += 1; // optimization
                    } else {
                        pair2 = curr.Rank;
                    }
                }
            }

            if (pair == PlayingCard.Ranks.NAR || pair2 == PlayingCard.Ranks.NAR) {
                return false;
            }

            m_handRank = HandRanks.TwoPair;
            m_highCard = pair2;
            return true;
        }

        // Check for the first available Pair
        /// <summary>
        /// Determines whether there is a pair in this hand: 2 cards with matching rank.
        /// </summary>
        /// <param name="cards">Cards in a hand.</param>
        /// <returns>True if this hand contains a pair, false otherwise.</returns>
        private bool isPair(PlayingCard[] cards) {
            PlayingCard.Ranks pair = PlayingCard.Ranks.NAR;
            for (int i = 1; i < cards.Length; ++i) {
                PlayingCard pred = cards[i - 1];
                PlayingCard curr = cards[i];
                if (pred.Rank == curr.Rank) {
                    pair = curr.Rank;
                    break;
                }      
            }

            if (pair == PlayingCard.Ranks.NAR) {
                return false;
            }

            m_handRank = HandRanks.Pair;
            m_highCard = pair;
            return true;
        }

        /// <summary>
        /// Determines whether a hand has a high-card, this is always true.
        /// </summary>
        /// <param name="cards">Cards of a hand.</param>
        /// <returns>True, always.</returns>
        private bool isHighCard(PlayingCard[] cards) {
            m_handRank = HandRanks.HighCard;
            m_highCard = cards[cards.Length - 1].Rank;
            return true;
        }
    }
}
