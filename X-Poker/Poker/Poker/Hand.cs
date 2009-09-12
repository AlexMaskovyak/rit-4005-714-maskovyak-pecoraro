using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poker {
    class Hand {

        // Cards Per Hand
        public const int Limit = 5;

        // Members and Defaults
        private Card[] m_cards = new Card[Limit];
        private int m_count = 0;

        // Add Cards to the Hand
        public void addCard(Card c) {
            if (m_count >= Limit)
                throw new Exception("Too Many Cards");
            m_cards[m_count++] = c;
        }

        // Clear the Hand
        public void Clear() {
            m_cards = new Card[Limit];
            m_count = 0;
        }

        // Check if the Hand is Full
        public bool IsFull() {
            return m_count == Limit;
        }

        // Check if the Hand is Empty
        public bool IsEmpty() {
            return m_count == 0;
        }

        // Get the Hand's Score
        public Score Score {
            get { return new Score(this); }
        }

        // Get the Cards
        public Card[] Cards {
            get { return m_cards; }
        }

    }
}
