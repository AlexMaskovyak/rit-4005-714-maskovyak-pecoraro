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

        /// <summary>
        /// A set of cards from which to find the optimal card.
        /// </summary>
        protected HashSet<PlayingCard> _selectedCards;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="maxSize">Maximum number of cards for this puzzle to hold.</param>
        public Puzzle(int maxSize)
            : base(maxSize)
        {

            // We Compare Standard Poker Hands, so there must be at least the
            // Standard Poker Hand Size for the game to work
            if (maxSize < 5)
            {
                throw new System.ArgumentException("Too Few Cards for the Puzzle");
            }

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
        public virtual bool IsBestHand(PokerHand hand)
        {
            // TODO: Stub
            return true;
        }

        /// <summary>
        /// Finds the best scoring PokerHand possible from the cards in this Puzzle.
        /// </summary>
        /// <returns>PokerHand containing the best score possible from the cards in this Puzzle.</returns>
        public virtual PokerHand GetBestHandPossible()
        {

            Console.WriteLine(_playingCards.Count);
            foreach (PlayingCard c in _playingCards)
            {
                Console.WriteLine(c);
            }

            List<PlayingCard> cardList = _playingCards.ToList();
            Permutations<PlayingCard> hands = new Permutations<PlayingCard>(cardList, 5);
            Console.WriteLine(hands.Perms.Count);

            // Initial Value
            List<PlayingCard> l = hands.Perms[0];
            PokerHand max = new PokerHand(5);
            max.Add(l[0], l[1], l[2], l[3], l[4]);

            // Find Max
            foreach (List<PlayingCard> lst in hands.Perms)
            {
                PokerHand curr = new PokerHand(5);
                curr.Add(lst[0], lst[1], lst[2], lst[3], lst[4]);
                if (curr.CompareTo(max) > 0)
                {
                    max = curr;
                }
            }

            Console.WriteLine(max.ScoreHand());
            return max;
        }

        /*/// <summary>
        /// Finds the best scoring PokerHand possible from the cards in this Puzzle.
        /// </summary>
        /// <returns>PokerHand containing the best score possible from the cards in this Puzzle.</returns>
        public virtual PokerHand GetBestHandPossible() {
            return GetBestHandPossible( 
                        base.Cards, 
                        0, 
                        PokerHand.StandardHandSize, 
                        null, 
                        new PlayingCard[0] );
        }



        private PokerHand GetBestHandPossible(
                PlayingCard[] cards, 
                int arrayPosition, 
                int cardsToSelect,
                PokerHand bestHand,
                PlayingCard[] handInProgress
            ) {

            // we have the necessary quantity of cards to select for a PokerHand
            if( handInProgress.Length == cardsToSelect ) {
                return GetBestHand( bestHand, new PokerHand( handInProgress ) );
            }
            
            // we have too few cards and have run out of cards to select
            if( arrayPosition == cards.Length ) {
                return bestHand;
            }

            // choose to not select this card and move on
            PokerHand resultNoAdd = 
                GetBestHandPossible(
                    cards,
                    arrayPosition + 1,
                    cardsToSelect,
                    bestHand,
                    handInProgress);

            // choose to select this card and move on
            PlayingCard[] bestHand2 = new PlayingCard[handInProgress.Length + 1];
            handInProgress.CopyTo(bestHand2, 0);
            bestHand2[handInProgress.Length] = cards[arrayPosition];

            PokerHand resultAdd =
                GetBestHandPossible(
                    cards,
                    arrayPosition + 1,
                    cardsToSelect,
                    bestHand,
                    handInProgress);

            // compare the result and return the best one
            return GetBestHand(resultNoAdd, resultAdd);
        }
        
        public PokerHand GetBestHand( PokerHand hand1, PokerHand hand2 ) {
            return (hand1.CompareTo(hand2) == 1) ? hand1 : hand2; 
        }
         
         * 
        */

        /// <summary>
        /// Used for testing and project requirements.
        /// </summary>
        public static void Main()
        {
            int cardMax = 7;

            Puzzle puzzle = new Puzzle(cardMax);
            Deck deck = new Deck();
            deck.Shuffle();

            // add cards to the puzzle
            int cardNum = 0;
            foreach (PlayingCard card in deck)
            {
                if (cardNum == cardMax) { break; }
                puzzle.Add(card);
                ++cardNum;
            }

            PokerHand bestHand = puzzle.GetBestHandPossible();
            if (bestHand == null)
            {
                Console.WriteLine("null");
            }
            else
            {
                foreach (PlayingCard c in bestHand)
                {
                    Console.WriteLine(c);
                }
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

