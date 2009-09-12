using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poker {
    class Score : IComparable<Score> {

        // Levels of Hands from Lowers to Highest
        public enum Levels {
            HighCard, Pair, TwoPair, ThreeKind, Straight,
            Flush, FullHouse, FourKind, StraightFlush
        }

        // Members
        private Levels m_level;
        private int m_value;

        // Determine the Score from a Hand
        public Score(Hand hand) {
            Card[] cards = hand.Cards;
            Array.Sort(cards);

            for (int i = 0; i < cards.Length; ++i) {
                Console.WriteLine(cards[i]);
            }
            Console.WriteLine();

            m_level = Levels.HighCard;
            m_value = -1;

            bool ignored = isStraightFlush(cards)
                || isFourOfAKind(cards)
                || isFullHouse(cards)
                || isFlush(cards)
                || isStraight(cards)
                || isThreeOfAKind(cards)
                || isTwoPair(cards)
                || isPair(cards)
                || isHighCard(cards);

        }

        public Levels Level {
            get { return m_level; }
        }

        public int Value {
            get { return m_value; }
        }

        public int CompareTo(Score other) {
            if (m_level < other.Level)
                return -1;
            if (m_level > other.Level)
                return 1;
            if (m_value < other.Value)
                return -1;
            if (m_value > other.Value)
                return 1;
            return 0;
        }

        public override string ToString() {
            switch (m_level) {
                case Levels.StraightFlush: return "Straight Flush " + m_value;
                case Levels.FourKind:      return "Four of a Kind " + m_value;
                case Levels.FullHouse:     return "Full House " + m_value;
                case Levels.Flush:         return "Flush " + m_value;
                case Levels.Straight:      return "Straight " + m_value;
                case Levels.ThreeKind:     return "Three of a Kind " + m_value;
                case Levels.TwoPair:       return "Two Pair " + m_value;
                case Levels.Pair:          return "Pair " + m_value;
                case Levels.HighCard:      return "High Card " + m_value;
                default:                   return "Unknown";
            }
        }

        // Check for a Straight Flush
        private bool isStraightFlush(Card[] cards) {
            for (int i = 1; i < cards.Length; ++i) {
                Card prev = cards[i - 1];
                Card curr = cards[i];
                if (prev.Suit != curr.Suit)
                    return false;
                if (prev.Rank != (curr.Rank - 1))
                    return false;
            }

            m_level = Levels.StraightFlush;
            m_value = cards[cards.Length - 1].Rank;
            return true;
        }

        // Check for Four of a Kind
        private bool isFourOfAKind(Card[] cards) {
            int count = 0;
            int val = cards[2].Rank;
            for (int i = 0; i < cards.Length; ++i) {
                if (cards[i].Rank == val) {
                    count++;
                }
            }

            if (count != 4) {
                return false;
            }

            m_level = Levels.FourKind;
            m_value = val;
            return true;
        }

        // Should be only two types
        private bool isFullHouse(Card[] cards) {
            int low = cards[0].Rank;
            int lowCount = 0;
            int high = cards[cards.Length - 1].Rank;
            int highCount = 0;
            for (int i = 0; i < cards.Length; ++i) {
                int rank = cards[i].Rank;
                if (rank != low && rank != high) {
                    return false;
                }
                if (rank == low) {
                    lowCount++;
                } else {
                    highCount++;
                }
            }

            m_level = Levels.FullHouse;
            m_value = (lowCount == 3) ? low : high;
            return true;
        }

        // Check if all have the same suit
        private bool isFlush(Card[] cards) {
            Card.Suits suit = cards[0].Suit;
            for (int i = 0; i < cards.Length; ++i) {
                if (cards[i].Suit != suit) {
                    return false;
                }
            }

            m_level = Levels.Flush;
            m_value = cards[cards.Length - 1].Rank;
            return true;
        }

        // Check for a Straight
        private bool isStraight(Card[] cards) {
            for (int i = 1; i < cards.Length; ++i) {
                Card prev = cards[i - 1];
                Card curr = cards[i];
                if (prev.Rank != (curr.Rank - 1)) {
                    return false;
                }
            }

            m_level = Levels.Straight;
            m_value = cards[cards.Length - 1].Rank;
            return true;
        }

        // Check for Three of a Kind
        private bool isThreeOfAKind(Card[] cards) {
            int count = 0;
            int count2 = 0;
            int val = cards[2].Rank;
            int val2 = cards[3].Rank;
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

            m_level = Levels.ThreeKind;
            m_value = (count == 3) ? val : val2;
            return true;
        }

        // Look for Two Pairs
        private bool isTwoPair(Card[] cards) {
            int pair = -1;
            int pair2 = -1;
            for (int i = 1; i < cards.Length; ++i) {
                Card pred = cards[i - 1];
                Card curr = cards[i];
                if (pred.Rank == curr.Rank) {
                    if (pair == -1) {
                        pair = curr.Rank;
                        i += 1; // optimization
                    } else {
                        pair2 = curr.Rank;
                    }
                }
            }

            if (pair == -1 || pair2 == -1) {
                return false;
            }

            m_level = Levels.TwoPair;
            m_value = pair2;
            return true;
        }

        // Check for the first available Pair
        private bool isPair(Card[] cards) {
            int pair = -1;
            for (int i = 1; i < cards.Length; ++i) {
                Card pred = cards[i - 1];
                Card curr = cards[i];
                if (pred.Rank == curr.Rank) {
                    pair = curr.Rank;
                    break;
                }      
            }

            if (pair == -1) {
                return false;
            }

            m_level = Levels.Pair;
            m_value = pair;
            return true;
        }

        // High Card is Always True
        private bool isHighCard(Card[] cards) {
            m_level = Levels.HighCard;
            m_value = cards[cards.Length - 1].Rank;
            return true;
        }
    }
}
