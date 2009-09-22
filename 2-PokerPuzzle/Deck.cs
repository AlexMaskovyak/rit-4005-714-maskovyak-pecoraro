using System;
using System.Collections;
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
    public class Deck : IEnumerable<PlayingCard> {

        /// <summary>
        /// The number of PlayingCards in a standard Deck of Cards.
        /// </summary>
        public const int DECK_SIZE = 52;

        /// <summary>
        /// The cards in the Deck.
        /// </summary>
        private readonly PlayingCard[] _playingCards;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Deck() {
            _playingCards = new PlayingCard[ Deck.DECK_SIZE ];

            int index = 0;
            // create playingcards
            foreach( PlayingCard.Suits suit in Enum.GetValues(typeof(PlayingCard.Suits)) ) {
                foreach( PlayingCard.Ranks rank in Enum.GetValues(typeof(PlayingCard.Ranks)) ) {
                    if (rank == PlayingCard.Ranks.NAR) { continue;  }
                    _playingCards[index++] = new PlayingCard(rank, suit);
                }
            }
        }

        /// <summary>
        /// Shuffles the PlayingCard s in this deck using Knuth's algorithm.
        /// </summary>
        /// <returns>Enerator for the newly shuffled deck.</returns>
        public IEnumerable<PlayingCard> Shuffle() {
            Random rng = new Random(); // default constructor automatically uses system time as a seed
            for(int i = Deck.DECK_SIZE - 1; i > 0; --i ) {
                int randomNumber = rng.Next(i);
                PlayingCard temp = _playingCards[i];
                _playingCards[i] = _playingCards[randomNumber];
                _playingCards[randomNumber] = temp;
            }
            return (IEnumerable<PlayingCard>)_playingCards;
        }

        /// <summary>
        /// Obtain genericized enumerator for the PlayingCards in this Deck.
        /// </summary>
        /// <returns>IEnumerator of PlayingCards.</returns>
        public IEnumerator<PlayingCard> GetEnumerator() {
            return ((IEnumerable<PlayingCard>)_playingCards).GetEnumerator();
        }

        /// <summary>
        /// Obtain an enumerator for the PlayingCards in this Deck.
        /// </summary>
        /// <returns>IEnumerator of PlayingCards.</returns>
        IEnumerator IEnumerable.GetEnumerator() {
           return this.GetEnumerator();
        }

        /// <summary>
        /// Debug Deck.
        /// </summary>
        public static void Main() {
            Deck deck = new Deck();
            deck.Shuffle();

            // debug to standard out
            foreach(PlayingCard card in deck ) {
                Console.WriteLine(card);
            }
        }
    }
}
