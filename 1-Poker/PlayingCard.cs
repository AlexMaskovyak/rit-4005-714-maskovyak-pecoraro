using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1_Poker
{
    /// <summary>
    /// Card holds values associated with a physical playing card.
    /// </summary>
    public class PlayingCard : IComparable<PlayingCard>
    {
        /// <summary>
        /// Playing card suits.
        /// </summary>
        public enum Suits { Club, Diamond, Heart, Spade }
        
        /// <summary>
        /// Playing card ranks/values.  The "number" of the card.
        /// </summary>
        public enum Ranks { NAR=-1, Two=2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }

        private readonly PlayingCard.Suits _suit;
        private readonly PlayingCard.Ranks _rank;

        /// <summary>
        /// Obtains the suit of this card.
        /// </summary>
        public PlayingCard.Suits Suit {
            get { return _suit; }
        }

        /// <summary>
        /// Obtains the face value of this card.
        /// </summary>
        /// <returns>The card's value.</returns>
        public PlayingCard.Ranks Rank {
            get { return _rank; }
        }

        /// <summary>
        /// Default constructor.  Defines the two necessary components of a playing card.
        /// </summary>
        /// <param name="rank">Playing card's value.</param>
        /// <param name="suit">Playing card's suit.</param>
        public PlayingCard( PlayingCard.Ranks rank, PlayingCard.Suits suit ) {
            _rank = rank;
            _suit = suit;
        }

        /// <summary>
        /// Overload of constructor.  Allows for construction from a string containing two char values.
        /// </summary>
        /// <param name="rankSuitPair">String of two characters representing Value|Suit.</param>
        public PlayingCard(string rankSuitPair) :
            this(rankSuitPair[0], rankSuitPair[1])
        {}

        /// <summary>
        /// Overload of constructor.  Allows for construction from char values.
        /// </summary>
        /// <param name="rank">Character representing a playing card's rank.</param>
        /// <param name="suit">Character representing a playing card's suit.</param>
        public PlayingCard(char rank, char suit) : 
            this(PlayingCard.getPlayingCardRankForChar(rank), PlayingCard.getPlayingCardSuitForChar(suit) )
        {}
            

        /// <summary>
        /// Converts a character representation of a playing card value into a PlayingCard.Value.
        /// </summary>
        /// <param name="rankChar">Character representing a playing card rank.</param>
        /// <returns>PlayingCard.Value that the character corresponds to.</returns>
        public static PlayingCard.Ranks getPlayingCardRankForChar(char rankChar) {
            switch( rankChar ) {
                case '2' : return PlayingCard.Ranks.Two;
                case '3' : return PlayingCard.Ranks.Three;
                case '4' : return PlayingCard.Ranks.Four;
                case '5' : return PlayingCard.Ranks.Five;
                case '6' : return PlayingCard.Ranks.Six;
                case '7' : return PlayingCard.Ranks.Seven;
                case '8' : return PlayingCard.Ranks.Eight;
                case '9' : return PlayingCard.Ranks.Nine;
                case 'T' : return PlayingCard.Ranks.Ten;
                case 'J' : return PlayingCard.Ranks.Jack;
                case 'Q' : return PlayingCard.Ranks.Queen;
                case 'K' : return PlayingCard.Ranks.King;
                case 'A' : return PlayingCard.Ranks.Ace;
                default : throw new ArgumentException( "Character could not be converted into a PlayingCard face value." );
            }
        }

        /// <summary>
        /// Converts a character representation of a playing card value into a PlayingCard.Suit.
        /// </summary>
        /// <param name="suitChar">Character representing a playing card suit.</param>
        /// <returns>PlayingCard.Suit that the character corresponds to.</returns>
        public static PlayingCard.Suits getPlayingCardSuitForChar(char suitChar) {
            switch (suitChar) {
                case 'C': return PlayingCard.Suits.Club;
                case 'D': return PlayingCard.Suits.Diamond;
                case 'H': return PlayingCard.Suits.Heart;
                case 'S': return PlayingCard.Suits.Spade;
                default: throw new ArgumentException( "Character could not be converted into a PlayCard suit." );
            }
        }

        /// <summary>
        /// Override to return the full spoken/written name of a playing card.
        /// </summary>
        /// <returns>The contemporary spoken/written name of a playing card.</returns>
        public override string ToString() {
            return String.Format( "{0} of {1}s", _rank, _suit );
        }

        /// <summary>
        /// Checks for value equivalence between two PlayingCards.
        /// </summary>
        /// <param name="obj">Object whose values are to be inspected against the current object's values.</param>
        /// <returns>True if the PlayingCard object passed in has the same value and suit as this one, false otherwise.</returns>
        public override bool Equals(object obj)
        {
            // null returns false
            if (obj == null) {
                return false;
            }

            // check that the parameter can be cast to PlayingCard
            PlayingCard card = obj as PlayingCard;
            if ((System.Object)card == null) {
                return false;
            }

            // compare fields
            return ( Rank == card.Rank && Suit == card.Suit );
        }

        /// <summary>
        /// PlayingCard specific value equality.
        /// </summary>
        /// <param name="card">PlayingCard whose values are to be comapred to this one's.</param>
        /// <returns>True if the specific PlayingCard's suit and value are equivalent to this one's, false otherwise.</returns>
        public bool Equals(PlayingCard card) {
            // check that the parameter can be cast to PlayingCard
            // see: http://msdn.microsoft.com/en-us/library/ms173147%28VS.80%29.aspx
            if ((object)card == null) {
                return false;
            }

            // compare fields
            return ( Rank == card.Rank && Suit == card.Suit );
        }

        /// <summary>
        /// PlayingCard specific hashcode generator.
        /// </summary>
        /// <returns>Hashcode for this PlayingCard.</returns>
        public override int GetHashCode() {
            return Suit.GetHashCode() ^ Rank.GetHashCode();
        }

        /// <summary>
        /// Support IComparable, allow for the comparison of this PlayingCard with the specified PlayingCard.
        /// </summary>
        /// <param name="card">PlayingCard to compare against this one.</param>
        /// <returns>
        ///     -1 if this PlayingCard is less than the one specified, 1 if this PlayingCard is greater than
        ///     the one specified, and 0 if they are equivalent.  Cards are considered equivalent if they 
        ///     have identical rank and suit values.
        /// </returns>
        public int CompareTo(PlayingCard card) {
            if (Rank < card.Rank)
                return -1;
            if (Rank > card.Rank)
                return 1;
            if (Suit < card.Suit)
                return -1;
            if (Suit > card.Suit)
                return 1;
            return 0;
        }
    }
}
