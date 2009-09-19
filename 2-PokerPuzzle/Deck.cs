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
            foreach( string suit in Enum.GetNames(typeof(PlayingCard.Suits)) ) {
                foreach( string rank in Enum.GetNames(typeof(PlayingCard.Ranks)) ) {
                    playingCards[index++] = new PlayingCard(rank[0], suit[0]);
                }
            }
        }

        /// <summary>
        /// Shuffles the PlayingCard s in this deck using Knuth's algorithm.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<PlayingCard> Shuffle() {
            Random rng = new Random(); // default constructor automatically uses system time as a seed
            for(int i = Deck.DECK_SIZE - 1; i > 0; --i ) {
                int randomNumber = rng.Next(i);
                PlayingCard temp = playingCards[i];
                playingCards[i] = playingCards[randomNumber];
                playingCards[randomNumber] = temp;
            }
            return (IEnumerator<PlayingCard>)playingCards.GetEnumerator();
        }

    }

}
