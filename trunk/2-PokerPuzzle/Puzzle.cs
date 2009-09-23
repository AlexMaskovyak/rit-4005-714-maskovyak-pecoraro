using System;
using BitArray = System.Collections.BitArray;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using _1_Poker;

namespace _2_PokerPuzzle
{
    /// <summary>
    /// Holds a series of PlayingCards and is capable of determining the best possible PokerHand that can
    /// be created from those hands. 
    /// </summary>
    class Puzzle : PokerHand
    {

        /// <summary>Default constructor.</summary>
        /// <param name="maxSize">Maximum number of cards for this puzzle to hold.</param>
        public Puzzle(int maxSize) : base(maxSize)
        {
            // We Compare Standard Poker Hands, so there must be at least the
            // Standard Poker Hand Size for the game to work
            if (maxSize < 5) {
                throw new System.ArgumentException("Too Few Cards for the Puzzle");
            }
        }

        /// <summary>Simpler Accessor for the Best Hand</summary>
        /// <remarks>This is Not Cached. It is recomputed each access.</remarks>
        public PokerHand BestHand {
            get { return GetBestHandPossible(); }
        }

        /// <summary>Get the Results for some Selections</summary>
        /// <param name="selections">Positions of the Card Selection</param>
        /// <returns>A Result Object containing the Game Results</returns>
        public virtual Result Selected(BitArray selections) {

            // Determine the Selected Hand
            PokerHand selectedHand = new PokerHand(5);
            PlayingCard[] cards = _playingCards.ToArray<PlayingCard>();
            for (int i = 0; i < selections.Length; ++i) {
                if (selections[i]) {
                    selectedHand.Add(cards[i]);
                }
            }

            // Compare to the best
            PokerHand bestHand = BestHand;
            bool isBest = (bestHand.CompareTo(selectedHand) == 0);

            // BitArray for the Best Hand
            BitArray bestHandBits = new BitArray(selections.Length);
            for (int i = 0; i < cards.Length; ++i) {
                bestHandBits[i] = (bestHand.Cards.Contains(cards[i]));
            }

            return new Result(isBest, bestHandBits, bestHand, selectedHand);

        }


        /*
        /// <summary>
        /// Finds the best scoring PokerHand possible from the cards in this Puzzle.
        /// </summary>
        /// <returns>PokerHand containing the best score possible from the cards in this Puzzle.</returns>
        public virtual PokerHand GetBestHandPossible()
        {
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
        */

        /// <summary>
        /// Finds the best scoring PokerHand possible from the cards in this Puzzle.
        /// </summary>
        /// <returns>PokerHand containing the best score possible from the cards in this Puzzle.</returns>
        public virtual PokerHand GetBestHandPossible() {
            return GetBestHandPossible(
                        base.Cards,
                        0,
                        PokerHand.StandardHandSize,
                        // baseline pokerhand is just the first 5 cards
                        new PokerHand(base.Cards[0], base.Cards[1], base.Cards[2], base.Cards[3], base.Cards[4]),
                        // the first array of cards is empty
                        new PlayingCard[0]);
        }

        /// <summary>Finds the best scoring PokerHand possible from the cards specified.</summary>
        /// <remarks>
        /// Uses a recursive algorithm where every run of the function generates the best possible hand
        /// containing the PlayingCard at the array position and the best possible hand that does not
        /// contain the PlayingCard at the array position.
        /// </remarks>
        /// <param name="cards">Array of cards from which to find the best scoring hand.</param>
        /// <param name="arrayPosition">Position indexing into the card's array, specifying the card to include and exclude.</param>
        /// <param name="cardsToSelect">Maximum number of cards out of which to create a PokerHand for scoring.</param>
        /// <param name="bestHand">The best scoring PokerHand found so far.</param>
        /// <param name="handInProgress">Array of cards being built up over recursive calls to this function.</param>
        /// <returns>
        /// The best scoring PokerHand formed from the cards passed into this arry starting
        /// at the index specified.
        /// </returns>
        private PokerHand GetBestHandPossible(
                PlayingCard[] cards,
                int arrayPosition,
                int cardsToSelect,
                PokerHand bestHand,
                PlayingCard[] handInProgress
            )
        {

            // we have the necessary quantity of cards to select for a PokerHand
            // so create and compare the PokerHands
            if (handInProgress.Length == cardsToSelect) {
                return GetBestHand(bestHand, new PokerHand(handInProgress));
            }

            // we have too few cards and have run out of cards to select
            if (arrayPosition == cards.Length) {
                return bestHand;
            }

            // catch whether we could not possibly get enough cards to form a hand from this
            // position of the array and on, we'd rather not waste the cycles on calls that
            // will not produce anything but bestHand as a result
            if (handInProgress.Length + (cards.Length - arrayPosition) < cardsToSelect) {
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
            PlayingCard[] handInProgress2 = new PlayingCard[handInProgress.Length + 1];
            handInProgress.CopyTo(handInProgress2, 0);
            handInProgress2[handInProgress.Length] = cards[arrayPosition];

            PokerHand resultAdd =
                GetBestHandPossible(
                    cards,
                    arrayPosition + 1,
                    cardsToSelect,
                    bestHand,
                    handInProgress2);

            // compare the result and return the best one
            if (resultNoAdd.HandSize() == PokerHand.StandardHandSize &&
                resultAdd.HandSize() == PokerHand.StandardHandSize) {
                return GetBestHand(resultNoAdd, resultAdd);
            }

            return bestHand;
        }
        
        /// <summary>Compares Two Hands and Returns the better</summary>
        /// <param name="hand1">First Hand</param>
        /// <param name="hand2">Second Hand</param>
        /// <returns>The Hand with the Greater Score</returns>
        protected virtual PokerHand GetBestHand( PokerHand hand1, PokerHand hand2 ) {
            if (hand1 == null) { return hand2; }
            if (hand2 == null) { return hand1; }
            return (hand2.CompareTo(hand1) == 1) ? hand2 : hand1; 
        }

        /// <summary>Main Program</summary>
        public static void Main(string[] args)
        {
            // create puzzle
            Puzzle puzzle = new Puzzle(7);

            // create Deck
            Deck deck = new Deck();
            IEnumerable<PlayingCard> shuffledDeck = deck.Shuffle();

            // add cards to puzzle
            foreach (PlayingCard card in shuffledDeck.Take<PlayingCard>(7)) {
                puzzle.Add(card);
            }

            // lets show the puzzle, compuute the best hand, and show that
            Console.WriteLine("Puzzle cards: " + puzzle.ToString());

            PokerHand bestHand = puzzle.GetBestHandPossible();

            Console.WriteLine("Best hand: " + bestHand.ToString() );
            Console.WriteLine("Score: " + bestHand.ScoreHand() );
        }
    }
}

