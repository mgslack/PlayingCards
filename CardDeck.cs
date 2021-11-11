using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

/*
 * Primary class defines a card deck for playing cards. The card deck can be
 * created with several options. Multiple decks can be defined for games that
 * need more than one deck (black jack, some solitaire games, etc.). In
 * addition, jokers can be added or Pinochle decks instantiated. The deck
 * options and number of decks are specified with the defined enums.
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
    /* The number of decks to use in this deck (1 - 7). Default is One_Deck. */
    public enum NumberOfDecks { Default, One_Deck, Two_Deck, Three_Deck, Four_Deck, Five_Deck, Six_Deck, Seven_Deck };

    /*
     * The deck option to use. IncludeJokers will add just 2 joker cards even
     * if more that one deck is used. Pinochle decks contain 48 cards,
     * two sets of each suit of cards from 9 - King + Ace.
     */
    public enum DeckOptions { None, IncludeJokers, PinochleDeck };

    public class CardDeck
    {
        /* Number of cards in a standard deck of playing cards (w/o jokers). */
        public const int NUM_CARDS_STD_DECK = 52;
        /* Number of cards in a pinochle deck. */
        public const int NUM_CARDS_PINOCHLE_DECK = 48;

        private int nextCard = 0;
        private int lastCard = 0;
        private int numDecks = 0;
        private bool _shuffled = false;

        // --------------------------------------------------------------------

        #region Properties

        private PlayingCard[] Cards { get; set; }

        /* Will be true if the deck has been shuffled at least once. */
        public bool Shuffled { get { return _shuffled; } }
        /* Number of decks used for this deck. */
        public int NumOfDecks { get { return numDecks; } }
        /* Number of cards currently available in the deck. */
        public int CardsLeft { get { return lastCard - nextCard; } }

        #endregion

        // --------------------------------------------------------------------

        #region Constructors

        /* Constructor creates a 1 deck deck with no options. */
        public CardDeck() : this(NumberOfDecks.One_Deck, DeckOptions.None) { }

        /* Constructor creates a deck with a defined number of decks with no options. */
        public CardDeck(NumberOfDecks num) : this(num, DeckOptions.None) { }

        /* Constructor creates a 1 deck deck with the given option. */
        public CardDeck(DeckOptions opts) : this(NumberOfDecks.One_Deck, opts) { }

        /*
         * Constructor creates a deck with the given number of decks along with
         * the provided option.
         */
        public CardDeck(NumberOfDecks num, DeckOptions opts)
        {
            List<PlayingCard> cards;

            numDecks = (int) num;
            if (numDecks == 0) numDecks = 1; // default passed in for NumberOfDecks...

            if (opts == DeckOptions.PinochleDeck) {
                lastCard = NUM_CARDS_PINOCHLE_DECK * numDecks;
                cards = Enumerable.Range(1, numDecks)
                    .SelectMany(d => Enumerable.Range(1, 2)
                        .SelectMany(p => Enumerable.Range(1, 4)
                            .SelectMany(s => Enumerable.Range(9, 5)  // nine up to king
                                .Select(c => new PlayingCard((Suit) s, (CardValue) c))))).ToList();
                for (int i = 0; i < numDecks; i++) {
                    cards.Add(new PlayingCard(Suit.Clubs, CardValue.Ace));
                    cards.Add(new PlayingCard(Suit.Clubs, CardValue.Ace));
                    cards.Add(new PlayingCard(Suit.Diamonds, CardValue.Ace));
                    cards.Add(new PlayingCard(Suit.Diamonds, CardValue.Ace));
                    cards.Add(new PlayingCard(Suit.Hearts, CardValue.Ace));
                    cards.Add(new PlayingCard(Suit.Hearts, CardValue.Ace));
                    cards.Add(new PlayingCard(Suit.Spades, CardValue.Ace));
                    cards.Add(new PlayingCard(Suit.Spades, CardValue.Ace));
                }
            }
            else {
                lastCard = NUM_CARDS_STD_DECK * numDecks;
                cards = Enumerable.Range(1, numDecks)
                    .SelectMany(d => Enumerable.Range(1, 4)
                        .SelectMany(s => Enumerable.Range(1, 13)
                            .Select(c => new PlayingCard((Suit) s, (CardValue) c)))).ToList();
            }

            if (opts == DeckOptions.IncludeJokers) {
                lastCard += 2;
                cards.Add(PlayingCard.RED_JOKER); 
                cards.Add(PlayingCard.BLACK_JOKER);
            }

            Cards = cards.ToArray<PlayingCard>();
        }

        #endregion

        // --------------------------------------------------------------------

        #region Public Methods

        /*
         * Shuffles the deck into a random order. Should be called prior to
         * using the card deck because the deck is created in a non-shuffled
         * order. In addition, Shuffle will reset the deck.
         */
        public void Shuffle()
        {
            nextCard = 0;
            _shuffled = true;

            Random rnd = new Random();

            // pseudo shuffle
            for (int i = 0; i < 10; i++) {
                for (int j = 0; j < lastCard; j++) {
                    int p = (int) Math.Floor(rnd.NextDouble() * lastCard);
                    // swap card at j with card at p
                    PlayingCard c = Cards[j];
                    Cards[j] = Cards[p];
                    Cards[p] = c;
                }
            }
        }

        /*
         * Returns true if more cards are still available to play in the
         * deck. If no more cards are available, then Shuffle can be called
         * to reset the deck (or decks).
         */
        public bool HasMoreCards()
        {
            return nextCard < lastCard;
        }

        /*
         * Returns the next card from the deck that is available. If no
         * more cards are available in the deck, returns an 'EMPTY_CARD'.
         * Can 'peek' at the next card in the deck by setting the
         * leave in deck flag to true, otherwise the card is removed.
         */
        public PlayingCard GetNextCard(bool LeaveInDeck)
        {
            PlayingCard ret = PlayingCard.EMPTY_CARD;

            if (HasMoreCards()) {
                ret = Cards[nextCard];
                if (!LeaveInDeck) nextCard++;
            }

            return ret;
        }

        /*
         * Returns the next card from the deck that is available. If no
         * more cards are available in the deck, returns an 'EMPTY_CARD'.
         * Card is removed from the deck.
         */
        public PlayingCard GetNextCard()
        {
            return GetNextCard(false);
        }

        /*
         * A simple undo method for the CardDeck. Will return the
         * previous card in the deck (the card before the 'GetNextCard'
         * call). If a card was not gotten from the deck or if only
         * the top card was used, undo will return an EMPTY_CARD (no
         * more undos available).
         * Note: Shuffle will reset undo (cannot undo to before the
         * shuffle).
         */
        public PlayingCard Undo()
        {
            PlayingCard ret = PlayingCard.EMPTY_CARD;

            if (nextCard > 1) {
                nextCard = nextCard - 2;
                ret = Cards[nextCard];
            }
            else if (nextCard == 1) {
                nextCard = 0;
            }

            return ret;
        }

        /*
         * Returns a collection of the cards in the deck in the current
         * shuffled order. The method allows one to 'peek' at the entire
         * deck at once, though other methods would have to be used to
         * determine what card you would get via the 'GetNextCard' call.
         */
        public ReadOnlyCollection<PlayingCard> GetDeckCards()
        {
            return Array.AsReadOnly(Cards);
        }

        #endregion

        // --------------------------------------------------------------------

        /*
         * Returns a string representation of the card deck. Mainly used for
         * testing and debugging.
         */
        public override string ToString()
        {
            return "CardDeck: (Decks-" + numDecks + ", Number of Cards-" + lastCard +
                   ", CurrentCard-" + nextCard + ")";
        }
    }
}
