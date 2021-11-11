using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * Primary class defines a standard Playing Card.  Card is defined by a suit
 * and value from the given enums.  Each card is defined with a 'deck value'
 * that is a number from 1 to 52 based on the suit and value of the card
 * (Ace of Clubs = 1 - King of Spades = 52).  A cards color is red if its suit
 * is Diamonds or Hearts, Black if suit is Clubs or Spades.
 *
 * Author:  M. G. Slack
 * Written: 2013-12-02
 *
 * ----------------------------------------------------------------------------
 * 
 * Updated: 2014-03-09 - Updated source to add regions.
 *
 */
namespace PlayingCards
{
    /* Playing card suit (note, Jokers should only be Hearts (red) or Spades (black)). */
    public enum Suit { None, Clubs, Diamonds, Hearts, Spades };

    /* Playing card value. */
    public enum CardValue { None, Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Joker };

    /* Playing card color (!!) */
    public enum CardColor { Unknown, Red, Black };

    public class PlayingCard
    {
        #region Public Static (Special Cards)
        /* Null or no card. */
        public static readonly PlayingCard EMPTY_CARD = new PlayingCard();
        /* Non-valid card to use for invalid card returns. */
        public static readonly PlayingCard BAD_CARD = new PlayingCard(Suit.None, CardValue.Joker);
        /* Red joker card. */
        public static readonly PlayingCard RED_JOKER = new PlayingCard(Suit.Hearts, CardValue.Joker);
        /* Black joker card. */
        public static readonly PlayingCard BLACK_JOKER = new PlayingCard(Suit.Spades, CardValue.Joker);

        #endregion

        #region Properties
        /* Suit of card, read-only. */
        private Suit _suit;
        public Suit Suit { get { return _suit; } }

        /* Value of card, read-only. */
        private CardValue _cardValue;
        public CardValue CardValue { get { return _cardValue; } }

        #endregion

        // --------------------------------------------------------------------

        #region Constructors

        /* Constructor creates an 'EMPTY_CARD' (no suit and no value) */
        public PlayingCard() : this(Suit.None, CardValue.None) { }

        /* Constructor creates a card with a given suit and value */
        public PlayingCard(Suit suit, CardValue value)
        {
            _suit = suit;
            _cardValue = value;
        }

        #endregion

        // --------------------------------------------------------------------

        #region Public Methods

        /*
         * Method returns the CardColor that represents the card based on the
         * suit of the card.
         */
        public CardColor GetCardColor()
        {
            CardColor ret = CardColor.Unknown;

            if ((_suit == Suit.Clubs) || (_suit == Suit.Spades))
                ret = CardColor.Black;
            else if ((_suit == Suit.Diamonds) || (_suit == Suit.Hearts))
                ret = CardColor.Red;

            return ret;
        }

        /*
         * Method returns the value of the card (1 - 13) based on the
         * CardValue set for the card. Jokers will return 0 as the
         * point value.
         */
        public int GetCardPointValue()
        {
            int val = (int) _cardValue;

            if (_cardValue == CardValue.Joker) val = 0;

            return val;
        }

        /*
         * Method returns the value of the card based on the CardValue
         * set for it, but in the range of 1-10. Jack, Queen and King
         * are valued as 10 instead of 11, 12 and 13. Jokers will
         * return 0.
         */
        public int GetCardPointValueFace10()
        {
            int val = GetCardPointValue();

            if (val > 10) val = 10;

            return val;
        }

        /*
         * Method returns a value of 1 - 52 based on the cards suit and
         * value set for it. The suit defines the relative value of the
         * card to others. Clubs are low, followed by Diamonds, then Hearts
         * with Spades being the highest value cards. Jokers will return
         * a 0 for value.
         */
        public int GetCardPointValueInDeck()
        {
            // should return a value between 0 and 52, if valid playing card
            // clubs = 1 - 13, diamonds = 14 - 26, hearts = 27 - 39, spades = 40 - 52
            // jokers have no 'deck' value
            int suitCount = (int) CardValue.King * ((int) _suit - 1);
            int val = (int) _cardValue + suitCount;

            if (_cardValue == CardValue.Joker) val = 0;

            return val;
        }

        #endregion

        // --------------------------------------------------------------------

        #region ToString
        /*
         * Returns a string representing the card. Mainly used for testing
         * and debugging.
         */
        public override string ToString()
        {
            string ret = "Not a valid card/No card";

            if (_cardValue == CardValue.Joker) {
                if (_suit == Suit.Hearts)
                    ret = "Red Joker";
                else if (_suit == Suit.Spades)
                    ret = "Black Joker";
            }
            else {
                int iV = (int) _cardValue;
                int iS = (int) _suit;

                if (iV != 0) {
                    switch (iV) {
                        case 1: ret = "Ace of "; break;
                        case 2: ret = "Two of "; break;
                        case 3: ret = "Three of "; break;
                        case 4: ret = "Four of "; break;
                        case 5: ret = "Five of "; break;
                        case 6: ret = "Six of "; break;
                        case 7: ret = "Seven of "; break;
                        case 8: ret = "Eight of "; break;
                        case 9: ret = "Nine of "; break;
                        case 10: ret = "Ten of "; break;
                        case 11: ret = "Jack of "; break;
                        case 12: ret = "Queen of "; break;
                        case 13: ret = "King of "; break;
                        default: ret = "?" + iV + "? of "; break;
                    }
                    switch (iS) {
                        case 1: ret += "Clubs"; break;
                        case 2: ret += "Diamonds"; break;
                        case 3: ret += "Hearts"; break;
                        case 4: ret += "Spades"; break;
                        default: ret += "?" + iS + "?"; break;
                    }
                }
            }

            return ret;
        }
        #endregion
    }
}
