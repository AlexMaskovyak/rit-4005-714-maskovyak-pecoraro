using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;

using _1_Poker;

namespace _5_SelectingAWinner_ConsoleApplication
{
    /// <summary>Provides conversion functionality between PlayingCard.Suits and SuitPrecedencePlayingCard.Suits</summary>
    [ComVisibleAttribute(true)]
    [HostProtectionAttribute(SecurityAction.LinkDemand, SharedState = true)]
    public class SuitsConverter : TypeConverter {

        /// <summary> check to see if the conversion can be made </summary>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) {
            if (destinationType == typeof(PlayingCard.Suits)) {
                return true;
            }
            return base.CanConvertTo(context, destinationType);
        }

        /// <summary> check if a conversion can be made</summary>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) {
            if (sourceType == typeof(PlayingCard.Suits)) {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary> provides conversions from PlayingCard.Suits to SuitPrecedencePlayingCard.Suits. </summary>
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value) {
            if (value.GetType() == typeof(PlayingCard.Suits)) {
                switch ((PlayingCard.Suits)value) {
                    case PlayingCard.Suits.Club:    return SuitPrecedencePlayingCard.Suits.Club;
                    case PlayingCard.Suits.Diamond: return SuitPrecedencePlayingCard.Suits.Diamond;
                    case PlayingCard.Suits.Heart:   return SuitPrecedencePlayingCard.Suits.Heart;
                    case PlayingCard.Suits.Spade:   return SuitPrecedencePlayingCard.Suits.Spade;
                }
            }

            return base.ConvertFrom(context, culture, value);
        }

        /// <summary> provides conversions from SuitPrecedencePlayingCard.Suits to PlayingCard.Suits</summary>
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType) {
            if (destinationType == typeof(PlayingCard.Suits)) {
                switch ((SuitPrecedencePlayingCard.Suits)value) {
                    case SuitPrecedencePlayingCard.Suits.Club:    return PlayingCard.Suits.Club;
                    case SuitPrecedencePlayingCard.Suits.Diamond: return PlayingCard.Suits.Diamond;
                    case SuitPrecedencePlayingCard.Suits.Heart:   return PlayingCard.Suits.Heart;
                    case SuitPrecedencePlayingCard.Suits.Spade:   return PlayingCard.Suits.Spade;
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    /// <summary>PlayingCard whose value/ordering is influenced by its Suit first, then its rank.</summary>
    public class SuitPrecedencePlayingCard : PlayingCard {

        /// <summary>New ordering of Suit values for SuitPrecedencePlayingCard.</summary>
        [TypeConverter(typeof(SuitsConverter))]
        public new enum Suits { Diamond = 1, Heart, Spade, Club };

        /// <summary>
        /// Obtains the suit of this card.
        /// </summary>
        [TypeConverter(typeof(SuitsConverter))]
        public new SuitPrecedencePlayingCard.Suits Suit {
            get { return (Suits)TypeDescriptor.GetConverter(SuitPrecedencePlayingCard.Suits.Club).ConvertFrom(base.Suit); }
        }

        /// <summary> Default constructor.  Defines the two necessary components of a playing card. </summary>
        /// <param name="rank">Playing card's value.</param>
        /// <param name="suit">Playing card's suit.</param>
        public SuitPrecedencePlayingCard(Ranks rank, Suits suit)
            : base(rank, (PlayingCard.Suits)TypeDescriptor.GetConverter(suit).ConvertTo(suit, typeof(PlayingCard.Suits))) { }

        /// <summary> Overload of constructor.  Allows for construction from a string containing two char values. </summary>
        /// <param name="rankSuitPair">String of two characters representing Value|Suit.</param>
        public SuitPrecedencePlayingCard(string rankSuitPair) : base(rankSuitPair) { }

        /// <summary>Overload of constructor.  Allows for construction from char values.</summary>
        /// <param name="rank">Character representing a playing card's rank.</param>
        /// <param name="suit">Character representing a playing card's suit.</param>
        public SuitPrecedencePlayingCard(char rank, char suit) : base(rank, suit) { }


        /// <summary>Overload PlayingCard comparison.</summary>
        /// <param name="card"> the other card. </param>
        public override int CompareTo(PlayingCard card) {
            if (card is SuitPrecedencePlayingCard) {
                return this.CompareTo((SuitPrecedencePlayingCard)card);
            }

            return base.CompareTo(card);
        }

        /// <summary>
        /// Support IComparable, allow for the comparison of this PlayingCard with the specified PlayingCard.
        /// </summary>
        /// <param name="card">PlayingCard to compare against this one.</param>
        /// <returns>
        ///     -1 if this PlayingCard is less than the one specified, 1 if this PlayingCard is greater than
        ///     the one specified, and 0 if they are equivalent.  Cards are considered equivalent if they 
        ///     have identical rank and suit values.
        ///     First comparison by suit: (high to low) clubs, spades, hearts, and diamonds
        ///     Second comparison by value: (high to low) ace, king, queen, jack, ten...two.
        /// </returns>
        public virtual int CompareTo(SuitPrecedencePlayingCard card) {
            if (Suit < card.Suit) {
                return -1;
            }
            if (Suit > card.Suit) {
                return 1;
            }
            if (Rank < card.Rank) {
                return -1;
            }
            if (Rank > card.Rank) {
                return 1;
            }
            return 0;
        }
    }
}
