using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using _1_Poker;

namespace _2_PokerPuzzle
{
    /// <summary>
    /// Holds a series of PlayingCards and is capable of determining the best possible PokerHand that can
    /// be created from those hands.  SHOULD THIS INHERIT FROM POKERHAND?
    /// </summary>
    class Puzzle : PokerHand
    {
        
        protected HashSet<PlayingCard> _selectedCards;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="maxSize">Maximum number of cards for this puzzle to hold.</param>
        public Puzzle(int maxSize) : base(maxSize) {
            _selectedCards = new HashSet<PlayingCard>();
        }

        /*/// <summary>
        /// Selects a PlayingCard contained in this Puzzle.
        /// </summary>
        /// <remarks>
        /// The number of PlayingCards that can be selected is limited to the standard poker hand size.  Once this quantity
        /// of cards has been selected this Puzzle object may be used to determine if the best possible hand has been selected.
        /// </remarks>
        /// <param name="card">PlayingCard in this Puzzle to select.</param>
        public virtual void Select(PlayingCard card) {
            // see if this card 
            if( !base._playingCards.Contains( card ) ) {
                throw new ArgumentException( "Only cards contained in this Puzzle may be selected." );
            }

            // we can only select up to x number of cards, where x is the standard poker hand size
            if( _selectedCards.Count == PokerHand.StandardHandSize ) {
                throw new ArgumentException( String.Format("Only {0} cards may be selected at once.", PokerHand.StandardHandSize ) );
            }

            _selectedCards.Add(card);
        }*/

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        public virtual bool IsBestHand(PokerHand hand) {

        }

        /// <summary>
        /// Finds the best scoring PokerHand possible from the cards in this Puzzle.
        /// </summary>
        /// <returns>PokerHand containing the best score possible from the cards in this Puzzle.</returns>
        public virtual PokerHand GetBestHandPossible() {

        }

        /// <summary>
        /// Used for testing and project requirements.
        /// </summary>
        public static void Main() {
            int cardMax = 7;

            Puzzle puzzle = new Puzzle(cardMax);
            Deck deck = new Deck();
            deck.Shuffle();

            // add cards to the puzzle
            int cardNum = 1;
            foreach (PlayingCard card in deck) {
                if (cardNum == cardMax) { break; }
                puzzle.Add(card);
                ++cardNum;
            }

            // select the first five cards
            /*cardNum = 1;
            foreach (PlayingCard card in deck) {
                if (cardNum == PokerHand.StandardHandSize) { break; }
                puzzle.Select(card);
                ++cardNum;
            }*/
        }
    }
}
