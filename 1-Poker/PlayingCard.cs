using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1_Poker
{
    /// <summary>
    /// Card holds values associated with a physical playing card.
    /// </summary>
    public class PlayingCard
    {
        /// <summary>
        /// Playing card suits.
        /// </summary>
        public enum Suit { Club, Diamond, Heart, Spade }
        
        /// <summary>
        /// Playing card ranks/values.  The "number" of the card.
        /// </summary>
        public enum Value { Two=2, Three=3, Four=4, Five=5, Six=6, Seven=7, Eight=8, Nine=9, Ten=10, Jack=11, Queen=12, King=13, Ace=14  }

        private PlayingCard.Suit _suit;
        private PlayingCard.Value _value;

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
        /// Overload of constructor.  Allows for construction from char values.
        /// </summary>
        /// <param name="value">Character representing a playing card's value.</param>
        /// <param name="suit">Character representing a playing card's suit.</param>
        public PlayingCard(char value, char suit) : 
            this(
                PlayingCard.getPlayingCardValueForChar(value), PlayingCard.getPlayingCardSuitForChar(suit) ) { }
            

        /// <summary>
        /// Obtains the suit of this card.
        /// </summary>
        /// <returns>The card's suit.</returns>
        public PlayingCard.Suit getSuit() { return _suit; }

        /// <summary>
        /// Obtains the face value of this card.
        /// </summary>
        /// <returns>The card's value.</returns>
        public PlayingCard.Value getValue() { return _value; }

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

    }
}
