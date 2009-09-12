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
        public enum Suit { Club, Diamond, Heart, Spade }
        
        /// <summary>
        /// Playing card ranks/values.  The "number" of the card.
        /// </summary>
        public enum Value { Two=2, Three=3, Four=4, Five=5, Six=6, Seven=7, Eight=8, Nine=9, Ten=10, Jack=11, Queen=12, King=13, Ace=14  }

        private readonly PlayingCard.Suit _suit;
        private readonly PlayingCard.Value _value;

        /// <summary>
        /// Obtains the suit of this card.
        /// </summary>
        public PlayingCard.Suit suit 
        {
            get
            {
                return _suit;
            }
        }

        /// <summary>
        /// Obtains the face value of this card.
        /// </summary>
        /// <returns>The card's value.</returns>
        public PlayingCard.Value value 
        {
            get
            {
                return _value;
            }
        }

        /// <summary>
        /// Default constructor.  Defines the two necessary components of a playing card.
        /// </summary>
        /// <param name="value">Playing card's value.</param>
        /// <param name="suit">Playing card's suit.</param>
        public PlayingCard( PlayingCard.Value value, PlayingCard.Suit suit ) 
        {
            _value = value;
            _suit = suit;
        }

        /// <summary>
        /// Overload of constructor.  Allows for construction from a string containing two char values.
        /// </summary>
        /// <param name="valueSuitPair">String of two characters representing Value|Suit.</param>
        public PlayingCard(string valueSuitPair) :
            this( valueSuitPair[0], valueSuitPair[1] )  { }

        /// <summary>
        /// Overload of constructor.  Allows for construction from char values.
        /// </summary>
        /// <param name="value">Character representing a playing card's value.</param>
        /// <param name="suit">Character representing a playing card's suit.</param>
        public PlayingCard(char value, char suit) : 
            this(
                PlayingCard.getPlayingCardValueForChar(value), PlayingCard.getPlayingCardSuitForChar(suit) ) { }
            

        /// <summary>
        /// Converts a character representation of a playing card value into a PlayingCard.Value.
        /// </summary>
        /// <param name="valueChar">Character representing a playing card value.</param>
        /// <returns>PlayingCard.Value that the character corresponds to.</returns>
        public static PlayingCard.Value getPlayingCardValueForChar(char valueChar)
        {
            switch( valueChar ) {
                case '2' : return PlayingCard.Value.Two;
                case '3' : return PlayingCard.Value.Three;
                case '4' : return PlayingCard.Value.Four;
                case '5' : return PlayingCard.Value.Five;
                case '6' : return PlayingCard.Value.Six;
                case '7' : return PlayingCard.Value.Seven;
                case '8' : return PlayingCard.Value.Eight;
                case '9' : return PlayingCard.Value.Nine;
                case 'T' : return PlayingCard.Value.Ten;
                case 'J' : return PlayingCard.Value.Jack;
                case 'Q' : return PlayingCard.Value.Queen;
                case 'K' : return PlayingCard.Value.King;
                case 'A' : return PlayingCard.Value.Ace;
                default : throw new ArgumentException( "Character could not be converted into a PlayingCard face value." );
            }
        }

        /// <summary>
        /// Converts a character representation of a playing card value into a PlayingCard.Suit.
        /// </summary>
        /// <param name="suitChar">Character representing a playing card suit.</param>
        /// <returns>PlayingCard.Suit that the character corresponds to.</returns>
        public static PlayingCard.Suit getPlayingCardSuitForChar(char suitChar)
        {
            switch (suitChar)
            {
                case 'C': return PlayingCard.Suit.Club;
                case 'D': return PlayingCard.Suit.Diamond;
                case 'H': return PlayingCard.Suit.Heart;
                case 'S': return PlayingCard.Suit.Spade;
                default: throw new ArgumentException( "Character could not be converted into a PlayCard suit." );
            }
        }

        /// <summary>
        /// Override to return the full spoken/written name of a playing card.
        /// </summary>
        /// <returns>The contemporary spoken/written name of a playing card.</returns>
        public override string ToString()
        {
            return String.Format( "{0} of {1}s", _value, _suit );
        }

        /// <summary>
        /// Checks for value equivalence between two PlayingCards.
        /// </summary>
        /// <param name="obj">Object whose values are to be inspected against the current object's values.</param>
        /// <returns>True if the PlayingCard object passed in has the same value and suit as this one, false otherwise.</returns>
        public override bool Equals(object obj)
        {
            // null returns false
            if (obj == null)
            {
                return false;
            }

            // check that the parameter can be cast to PlayingCard
            PlayingCard card = obj as PlayingCard;
            if ((System.Object)card == null) 
            {
                return false;
            }

            // compare fields
            return ( value == card.value && suit == card.suit );
        }

        /// <summary>
        /// PlayingCard specific value equality.
        /// </summary>
        /// <param name="card">PlayingCard whose values are to be comapred to this one's.</param>
        /// <returns>True if the specific PlayingCard's suit and value are equivalent to this one's, false otherwise.</returns>
        public bool Equals(PlayingCard card)
        {
            // check that the parameter can be cast to PlayingCard
            if ((object)card == null)
            {
                return false;
            }

            // compare fields
            return ( value == card.value && suit == card.suit );
        }

        /// <summary>
        /// PlayingCard specific hashcode generator.
        /// </summary>
        /// <returns>Hashcode for this PlayingCard.</returns>
        public override int GetHashCode()
        {
            return (int)suit ^ (int)value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public int CompareTo(PlayingCard card)
        {
            if (value < card.value){
                return -1;
            }
            if (value > card.value) {
                return 1;
            }
            return 0;
        }
    }
}
