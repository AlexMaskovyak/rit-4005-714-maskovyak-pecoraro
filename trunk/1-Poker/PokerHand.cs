using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1_Poker
{
    /// <summary>
    /// Stores playing cards associated with a 5-card hand of poker.
    /// </summary>
    public class PokerHand : IEnumerable, IComparable<PokerHand>
    {
        /// <summary>Standard Poker Hand size.</summary>
        public const int StandardHandSize = 5;

        /// <summary>Set storing all playing cards held in this hand.</summary>
        protected HashSet<PlayingCard> _playingCards;
        /// <summary>
        /// Holds the maximum hand size.
        /// </summary>
        protected int _maxHandSize;


        /// <summary>Largest quantity of cards allowed in a 5-card hand of poker.</summary>
        public int MaxHandSize {
            get { return _maxHandSize; }
        }

        /// <summary>Default constructor.</summary>
        /// <remarks>Creates a PokerHand with a maximum hand size set to the PokerHand's standard size.</remarks>
        public PokerHand() 
            : this( PokerHand.StandardHandSize ) 
        { }

        /// <summary>Constructor for creating an empty PokerHand with the specified size restriction.</summary>
        /// <param name="maxHandSize">Maximum number of cards for this PokerHand to contain.</param>
        public PokerHand( int maxHandSize )
            : this(maxHandSize, null)
        { }

        /// <summary>Constructor for creating a hand of playing cards.</summary>
        /// <param name="playingCards">List of playing cards to be added to this hand.</param>
        public PokerHand( List<PlayingCard> playingCards )
            : this(playingCards.ToArray())
        { }

        /// <summary>Constructor for creating a hand of playing cards.</summary>
        /// <param name="playingCards">Enumerable object of PlayingCards.</param>
        public PokerHand( IEnumerable<PlayingCard> playingCards ) 
            : this(playingCards.ToArray())
        { }

        /// <summary>Constructor for creating a hand of playing cards.</summary>
        /// <param name="playingCards">The playing cards to add to this hand.</param>
        public PokerHand( params PlayingCard[] playingCards ) 
            : this( playingCards.Length, playingCards )
        { }

        /// <summary>Constructor for creating a hand of playing cards.</summary>
        /// <remarks>This is the work-horse constructor which fulfills creation functionality for all other constructors.</remarks>
        /// <param name="maxHandSize">Maximum number of cards for this PokerHand to contain.</param>
        /// <param name="playingCards">Playing cards to add to this PokerHand.</param>
        public PokerHand( int maxHandSize, params PlayingCard[] playingCards ) {

            _maxHandSize = maxHandSize;
            
            // create hashset and add cards to it
            _playingCards = new HashSet<PlayingCard>();

            // add the cards
            if (!Add(playingCards)) {
                throw new System.ArgumentException(
                    "An error occurred when attempting to construct this PokerHand with the PlayingCards passed in.");
            }
        }

        /// <summary>Attempts to add the PlayingCards to this PokerHand.</summary>
        /// <remarks>
        /// The card(s) will only be added if the hand is not currently full or if it is a card that this hand does not already contain.
        /// Adds as many cards as possible.
        /// </remarks>
        /// <param name="cards">PlayingCards to be added.</param>
        /// <returns>True if all of the cards were added, false otherwise.</returns>
        public bool Add(params PlayingCard[] cards)
        {
            bool result = true;
            // there is nothing to add so of course "adding" nothing succeeds
            if (cards == null) {
                return true;
            }

            // add each card to the set
            foreach (PlayingCard card in cards) {
                if (MaxHandSize == HandSize()) {
                    throw new System.ArgumentException(
                        String.Format("A PokerHand may only contain {0} cards.", MaxHandSize));
                }

                if (_playingCards.Contains(card)) {
                    throw new System.ArgumentException(
                        "A PokerHand may only contain one of any particular card type from a standard 52-card deck!  Cheater!");
                }

                result = result && _playingCards.Add(card);
            }

            return result;
        }

        /// <summary>
        /// Clears the hand of all cards.
        /// </summary>
        public void Clear() {
            _playingCards.Clear();
        }
        
        /// <summary>
        /// Removes the specified playingcard from this hand.
        /// </summary>
        /// <param name="card">PlayingCard to remove.</param>
        public void Remove(PlayingCard card) {
            _playingCards.Remove(card);
        }

        /// <summary>
        /// Obtains the number of cards in this hand.
        /// </summary>
        /// <returns>Count of the cards in this hand.</returns>
        public int HandSize() {
            return _playingCards.Count;
        }

        /// <summary>Obtains an array containing the cards from this hand.</summary>
        /// <returns>An array of PlayingCards contained within this PokerHand.</returns>
        public PlayingCard[] Cards {
            get {
                PlayingCard[] playingCardsArray = new PlayingCard[ HandSize() ];
                _playingCards.CopyTo( playingCardsArray, 0, HandSize() );
                return playingCardsArray;
            }
        }

        /// <summary>
        /// Scores the PokerHand, returning a Score object which can be used to compare one 
        /// PokerHand against another.
        /// </summary>
        /// <returns>Score object representing the value of this PokerHand.</returns>
        public virtual Score ScoreHand() {
            return new Score(this);
        }

        /// <summary>Obtain an Enumerator for the PlayingCards in this PokerHand.</summary>
        /// <returns>Enumerator for the PlayingCards in this PokerHand.</returns>
        public virtual IEnumerator GetEnumerator() {
            return _playingCards.GetEnumerator();
        }

        /// <summary>Compares this PokerHand's score to another PokerHand's score.</summary>
        /// <param name="pokerHand">PokerHand whose hand score is to be compared to this one.</param>
        /// <returns>
        ///     -1 if this PokerHand's score is lower than the one specified, 1 if this PokerHand is
        ///     higher than the one specified, 0 if they have equivalent scores.
        /// </returns>
        public virtual int CompareTo(PokerHand pokerHand) {
            return ScoreHand().CompareTo(pokerHand.ScoreHand());
        }

        /// <summary>Return a String representation of the Puzzle</summary>
        /// <returns>String of the format "{card_1} {card_2} ... {card_n}"</returns>
        public override string ToString() {
            StringBuilder builder = new StringBuilder();
            foreach (PlayingCard card in this) {
                builder.Append("[" + card + "] ");
            }
            return builder.ToString();
        }
    }
}
