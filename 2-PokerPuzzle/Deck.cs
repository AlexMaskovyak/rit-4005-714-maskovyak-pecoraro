using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using _1_Poker;

namespace _2_PokerPuzzle
{
    /// <summary>
    /// Holds a standard 52-card deck.
    /// </summary>
    /// <remarks>
    /// Provides mechanisms to manipulate a Deck of cards.
    /// </remarks>
    public class Deck {

        /// <summary>
        /// The number of PlayingCards in a standard Deck of Cards.
        /// </summary>
        public const int DECK_SIZE = 52;

        private readonly PlayingCard[] playingCards;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Deck() {
            playingCards = new PlayingCard[ Deck.DECK_SIZE ];

            int index = 0;
            // create playingcards
            foreach( PlayingCard.Suits suit in Enum.GetValues(typeof(PlayingCard.Suits)) ) {
                foreach( PlayingCard.Ranks rank in Enum.GetValues(typeof(PlayingCard.Ranks)) ) {
                    if (rank == PlayingCard.Ranks.NAR) { continue;  }
                    playingCards[index++] = new PlayingCard(rank, suit);
                }
            }
        }

        /// <summary>
        /// Shuffles the PlayingCard s in this deck using Knuth's algorithm.
        /// </summary>
        /// <returns>Enerator for the newly shuffled deck.</returns>
        public IEnumerator<PlayingCard> Shuffle() {
            Random rng = new Random(); // default constructor automatically uses system time as a seed
            for(int i = Deck.DECK_SIZE - 1; i > 0; --i ) {
                int randomNumber = rng.Next(i);
                PlayingCard temp = playingCards[i];
                playingCards[i] = playingCards[randomNumber];
                playingCards[randomNumber] = temp;
            }
            return ((IEnumerable<PlayingCard>)playingCards).GetEnumerator();
        }

        /// <summary>
        /// Debug Deck.
        /// </summary>
        public static void Main() {
            Deck deck = new Deck();
            deck.Shuffle();
        }
    }
}
