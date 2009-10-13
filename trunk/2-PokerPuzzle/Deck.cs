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
            return Shuffle((int)DateTime.Now.Ticks);
        }

        /// <summary> shuffles the PlayingCard s in this deck using Knuth's algorithm.</summary>
        /// <param name="seed"> seed to use for predictable testing of shuffle. </param>
        /// <returns> enumerator for the newly shuffled deck.</returns>
        public IEnumerable<PlayingCard> Shuffle(int seed)
        {
            Random rng = new Random(seed); // default constructor automatically uses system time as a seed
            for (int i = Deck.DECK_SIZE - 1; i > 0; --i)
            {
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

        /// <summary>Debug Deck.</summary>
        /// <remarks>Creates a deck, outputs the deck, shuffles the deck, and then outputs the first 5 shuffled cards.</remarks>
        public static void Main() {

            // debug information
            Console.WriteLine( "==Creating Deck...==" );
            Deck deck = new Deck();

            Console.WriteLine("==Outputting Deck...==");
            foreach (PlayingCard card in deck) {
                Console.WriteLine( String.Format("  {0}", card ) );
            }
            
            Console.WriteLine("==Shuffling Deck...==");
            IEnumerable<PlayingCard> deckEnumerable = deck.Shuffle();

            Console.WriteLine("==Outputting 5 cards from shuffled deck...==");

            foreach (PlayingCard card in deckEnumerable.Take<PlayingCard>(PokerHand.StandardHandSize)) {
                Console.WriteLine( String.Format( " {0}", card ) );
            }
            
        }
    }
}
