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
    /// be created from those hands.  SHOULD THIS INHERIT FROM POKERHAND?
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
            get { return GetBestHandPossibleAlex(); }
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


        /// <summary>
        /// Finds the best scoring PokerHand possible from the cards in this Puzzle.
        /// </summary>
        /// <returns>PokerHand containing the best score possible from the cards in this Puzzle.</returns>
        /*
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

        /// <summary>Finds the best scoring PokerHand possible from the cards in this Puzzle.</summary>
        /// <returns>PokerHand containing the best score possible from the cards in this Puzzle.</returns>
        public virtual PokerHand GetBestHandPossibleAlex() {
            return GetBestHandPossibleAlex( 
                        base.Cards, 
                        0, 
                        PokerHand.StandardHandSize, 
                        new PokerHand(base.Cards[0], base.Cards[1], base.Cards[2], base.Cards[3], base.Cards[4]), 
                        new PlayingCard[0] );
        }


        // TODO: Alex, Fill this in!
        /// <summary>Recursive Algorithm to Find the Best Possible Hand</summary>
        /// <param name="cards"></param>
        /// <param name="arrayPosition"></param>
        /// <param name="cardsToSelect"></param>
        /// <param name="bestHand"></param>
        /// <param name="handInProgress"></param>
        /// <returns></returns>
        private PokerHand GetBestHandPossibleAlex(
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
                GetBestHandPossibleAlex(
                    cards,
                    arrayPosition + 1,
                    cardsToSelect,
                    bestHand,
                    handInProgress);

            // choose to select this card and move on
            PlayingCard[] bestHandOfCards2 = new PlayingCard[handInProgress.Length + 1];
            handInProgress.CopyTo(bestHandOfCards2, 0);
            bestHandOfCards2[handInProgress.Length] = cards[arrayPosition];

            PokerHand resultAdd =
                GetBestHandPossibleAlex(
                    cards,
                    arrayPosition + 1,
                    cardsToSelect,
                    bestHand,
                    bestHandOfCards2);

            // compare the result and return the best one
            if (resultNoAdd.HandSize() == PokerHand.StandardHandSize &&
                resultAdd.HandSize() == PokerHand.StandardHandSize)
            {
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
        [System.STAThreadAttribute()]
        public static void Main(string[] args)
        {
            App app = new App();
            if (args != null && args.Length >= 2)
                app.Run(new Window1(int.Parse(args[0])));
            else
                app.Run(new Window1());
        }
    }
}

