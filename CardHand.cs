using System;

/*
 * Primary class is used to implement a card hand (what the players are holding)
 * for the PlayingCards assembly. Hands can be created with any number of cards
 * for the hand, but can't be less than 1. The default number of cards in a
 * hand is 5. Typically, the hand is stored in 'sorted' order where the sort is
 * defined by an available enum. The default sort order in the hand is
 * 'SuitsEqual'.
 *
 * Author:  M. G. Slack
 * Written: 2013-12-04
 *
 * ----------------------------------------------------------------------------
 * 
 * Updated: 2013-12-31 - Added a 'Shuffle()' method to the card hand.
 *          2014-01-01 - Fixed bug in card hand creation (assigned wrong value).
 *          2014-01-02 - Fixed a bug in the card counts in the 'Replace' method.
 *          2014-01-16 - Added consts for FIRST and NEXT slot values.
 *          2014-03-09 - Added Contains methods for suit and value. Updated source
 *                       to add regions.
 *
 */
namespace PlayingCards
{
    public enum CardComparer { DeckValue, SuitsEqual, FaceCards10 };

    public class CardHand
    {
        /* Default maximum number of cards that can be contained in the hand. */
        public const int DEF_MAX_NBR_CARDS = 5;
        /* Minimum number of cards that can be contained in the hand. */
        public const int MIN_NBR_CARDS = 1;
        /* Slot value for first card in card hand. */
        public const int FIRST = 0;
        /* Slot value for next card in card hand. */
        public const int NEXT = 1;

        private PlayingCard[] cards;     // card hand
        private int maxCards = 0;        // max cards in hand
        private int numCards = 0;        // current number of cards in hand
        private bool handSorted = false; // card hand is in sorted order
        private CardComparer compMode = CardComparer.SuitsEqual;

        // --------------------------------------------------------------------

        #region Properties

        /* Maximum number of cards that can be contained in the hand. */
        public int MaxCardsInHand { get { return maxCards; } }
        /* Number of cards currently contained in the hand. */
        public int CurNumCardsInHand { get { return numCards; } }
        /* Is hand in sorted order (per the CardComparer enum). */
        public bool HandSorted { get { return handSorted; } }
        /*
         * Hand sort mode.
         *  DeckValue = sorts the cards in order (Ace - King) with Clubs first,
         *   Diamonds second, Hearts third and Spades last.
         *  SuitsEqual (default order) = sorts cards in order (Ace - King), suit
         *   is ignored.
         *  FaceCards10 = same as SuitsEqual, except Jack-King are assumed to be 10.
         */
        public CardComparer CompareMode { get { return compMode; } set { compMode = value; } }

        #endregion

        // --------------------------------------------------------------------

        #region Constructors

        /* Creates card hand that can contain 5 cards in sorted order. */
        public CardHand() : this(DEF_MAX_NBR_CARDS, true) { }

        /*
         * Creates a card hand that can contain the defined NumberOfCards
         * in a sorted order.
         */
        public CardHand(int NumberOfCards) : this(NumberOfCards, true) { }

        /*
         * Creates a card hand that can contain 5 cards with the sort flag
         * set be the caller.
         */
        public CardHand(bool Sorted) : this(DEF_MAX_NBR_CARDS, Sorted) { }

        /*
         * Creates a card hand that can contain the defined NumberOfCards
         * with the sort determined by the caller. Initializes the hand
         * slots to be EMPTY_CARDs.
         */
        public CardHand(int NumberOfCards, bool Sorted)
        {
            if (NumberOfCards > MIN_NBR_CARDS)
                maxCards = NumberOfCards;
            else
                maxCards = MIN_NBR_CARDS;

            handSorted = Sorted;

            cards = new PlayingCard[maxCards];

            RemoveAll();
        }

        #endregion

        // --------------------------------------------------------------------

        #region Private / Internal Methods

        private int CompareCards(PlayingCard crd1, PlayingCard crd2)
        {
            int comp = 0; // assume equal
            int c1, c2;

            switch (compMode) {
                case CardComparer.SuitsEqual:
                    c1 = crd1.GetCardPointValue();
                    c2 = crd2.GetCardPointValue();
                    break;
                case CardComparer.FaceCards10:
                    c1 = crd1.GetCardPointValueFace10();
                    c2 = crd2.GetCardPointValueFace10();
                    break;
                default:
                    c1 = crd1.GetCardPointValueInDeck();
                    c2 = crd2.GetCardPointValueInDeck();
                    break;
            }

            if (c1 > c2)
                comp = 1;
            else if (c1 < c2)
                comp = -1;

            return comp;
        }

        private void SortHand()
        {
            // only sort if more than one card, simple bubble sort (small set of cards)
            if (numCards > 1) {
                for (int i = FIRST; i < (maxCards - 1); i++) {
                    for (int j = (i + 1); j < maxCards; j++) {
                        if ((CompareCards(cards[i], cards[j]) < 0) || (cards[i] == PlayingCard.EMPTY_CARD)) {
                            PlayingCard t = cards[i]; // swap
                            cards[i] = cards[j];
                            cards[j] = t;
                        }
                    }
                }
            }
        }

        // --------------------------------------------------------------------

        internal void DupCards(PlayingCard[] handCards)
        {
            if (handCards.Length >= numCards) {
                for (int i = FIRST; i < numCards; i++)
                    cards[i] = handCards[i];
            }
        }

        #endregion

        // --------------------------------------------------------------------

        #region Public Methods

        /*
         * Adds a card to the card hand. Will place the card in the first
         * empty spot of the hand. Will sort the card into place if the hand
         * is sorted. Passes back a bool indicating a good add or not. Must
         * be a valid card.
         */
        public bool Add(PlayingCard card)
        {
            bool added = false;

            if ((numCards != maxCards) && (card.Suit != 0) && (card.CardValue != 0)) {
                for (int i = FIRST; i < maxCards; i++) {
                    if (cards[i] == PlayingCard.EMPTY_CARD) {
                        numCards++;
                        cards[i] = card;
                        added = true;
                        break;
                    }
                }
            }

            if ((added) && (handSorted)) SortHand();

            return added;
        }

        /*
         * Removes a card from the hand at a particular slot. Passes back
         * the card removed from the hand. Will return a 'BAD_CARD' if the
         * slot is out of range. Slots start at 0 for the first and go to
         * max cards - 1.
         */
        public PlayingCard Remove(int slot)
        {
            PlayingCard ret = PlayingCard.BAD_CARD;

            if ((slot >= FIRST) && (slot < maxCards)) {
                ret = cards[slot];
                cards[slot] = PlayingCard.EMPTY_CARD;
                numCards--;
            }

            return ret;
        }

        /*
         * Removes all cards from the hand and resets each slot to be an
         * EMPTY_CARD.
         */
        public void RemoveAll()
        {
            numCards = 0;
            for (int i = FIRST; i < maxCards; i++) {
                cards[i] = PlayingCard.EMPTY_CARD;
            }
        }

        /*
         * For card hands not sorted, which automatically compress, method
         * moves all cards to the beginning slots of the hand.
         */
        public void CompressHand()
        {
            PlayingCard[] temp = new PlayingCard[maxCards];
            int j = 0;

            // initialize temporary hand
            for (int i = FIRST; i < maxCards; i++) {
                temp[i] = PlayingCard.EMPTY_CARD;
            }
            // move good cards over to temp
            for (int i = FIRST; i < maxCards; i++) {
                if (cards[i] != PlayingCard.EMPTY_CARD) {
                    temp[j++] = cards[i];
                }
            }
            // replace hand with compressed hand
            for (int i = FIRST; i < maxCards; i++) {
                cards[i] = temp[i];
            }
        }

        /*
         * Returns the PlayingCard from the card hand at the particular
         * slot (or position) in the hand. May return a 'BAD_CARD' if
         * the slot is not valid for the card hand. Slots start at
         * 0 and go to max cards - 1. Card is left in the card hand.
         */
        public PlayingCard CardAt(int slot)
        {
            PlayingCard ret = PlayingCard.BAD_CARD;

            if ((slot >= FIRST) && (slot < maxCards)) ret = cards[slot];

            return ret;
        }

        /*
         * Replaces the card in the hand at the given card hand
         * slot and passes back the card replaced. Will pass back
         * a 'BAD_CARD' if the slot is out of bounds or the card
         * to replace is not a valid card. Card hand will not change
         * if either of the previous checks is true.
         * Slots start at 0 and go to max cards - 1.
         */
        public PlayingCard Replace(PlayingCard card, int slot)
        {
            PlayingCard ret = PlayingCard.BAD_CARD;

            // let remove handle slot check
            if ((card.Suit > 0) && (card.CardValue > 0)) ret = Remove(slot);

            if (ret != PlayingCard.BAD_CARD) { 
                if (handSorted) {
                    Add(card); // let add handle placement
                }
                else {
                    cards[slot] = card; // replace it...
                    numCards++; // remove() decremented the card count, need to add back
                }
            }

            return ret;
        }

        /*
         * Method returns true if the PlayingCard passed in is contained within
         * the card hand.
         */
        public bool Contains(PlayingCard card)
        {
            bool ret = false;

            if ((card.CardValue != 0) && (card.Suit != 0)) {
                for (int i = FIRST; i < maxCards; i++) {
                    if (cards[i] != PlayingCard.EMPTY_CARD) {
                        ret = ((cards[i].Suit == card.Suit) &&
                               (cards[i].CardValue == card.CardValue));
                        if (ret) break;
                    }
                }
            }

            return ret;
        }

        /*
         * Method will return true if the CardHand contains cards with
         * the given card Suit. Will return false if given Suit.None or
         * the CardHand does not have cards with the suit in question.
         */
        public bool Contains(Suit suit)
        {
            bool ret = false;

            if (suit != Suit.None) {
                for (int i = FIRST; i < maxCards; i++) {
                    if (cards[i] != PlayingCard.EMPTY_CARD) {
                        ret = (cards[i].Suit == suit);
                        if (ret) break;
                    }
                }
            }

            return ret;
        }

        /*
         * Method returns true if the CardHand contains cards with the
         * given card value (Ace, Seven, Ten, etc.). Will return false
         * if given CardValue.None or the hand does not have a card with
         * the given card value.
         */
        public bool Contains(CardValue cardValue)
        {
            bool ret = false;

            if (cardValue != CardValue.None) {
                for (int i = FIRST; i < maxCards; i++) {
                    if (cards[i] != PlayingCard.EMPTY_CARD) {
                        ret = (cards[i].CardValue == cardValue);
                        if (ret) break;
                    }
                }
            }

            return ret;
        }

        /*
         * Method returns the first available card in the hand and removes
         * it from the card hand - card is 'played'.
         */
        public PlayingCard GetFirstAvailableCard()
        {
            PlayingCard ret = PlayingCard.EMPTY_CARD;

            if (numCards > 0) {
                for (int i = FIRST; i < maxCards; i++) {
                    if (cards[i] != PlayingCard.EMPTY_CARD) {
                        ret = Remove(i);
                        break;
                    }
                }
            }

            return ret;
        }

        /*
         * Method used to return a new duplicate copy of the CardHand
         * being copied. The main use envisioned would be to
         * implement undo behavior for the card hand.
         * I.E., Hand is changed (add/remove/etc.) in program, copy
         * hand to 'undo' list. If undo is selected in program,
         * copy hand in undo list to game hand.
         */
        public CardHand Copy()
        {
            CardHand hand = new CardHand(numCards, handSorted);

            hand.DupCards(cards);
            
            return hand;
        }

        /*
         * Method used to shuffle the card hand. Shuffles the cards
         * available in a random order and packs the hand so that all
         * EMPTY_CARDS are placed in the back of the hand.
         * Calling Shuffle on a sorted hand does nothing.
         */
        public void Shuffle()
        {
            // only shuffle if have more than 1 card in hand and not sorted
            if ((!handSorted) && (numCards > 1)) {
                Random rnd = new Random();

                // pseudo shuffle
                for (int i = 0; i < 10; i++) {
                    for (int j = FIRST; j < maxCards; j++) {
                        int p = (int) Math.Floor(rnd.NextDouble() * maxCards);
                        // swap card at j with card at p
                        PlayingCard c = cards[j];
                        cards[j] = cards[p];
                        cards[p] = c;
                    }
                }
                // pack the hand (moves empty_cards to back of hand)
                CompressHand();
            }
        }

        #endregion

        // --------------------------------------------------------------------

        /*
         * String representation of the card hand, mainly used for testing and
         * debugging.
         */
        public override string ToString()
        {
            String ret = "CardHand: [";

            for (int i = FIRST; i < maxCards; i++) {
                ret += "(" + i + ") ";
                if (cards[i] != PlayingCard.EMPTY_CARD)
                    ret += cards[i].ToString();
                else
                    ret += "Empty";
                if (i < maxCards - 1) ret += ", ";
            }
            ret += "]";

            return ret;
        }
    }
}
