using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

/*
 * Primary class loads and caches the playing card images embedded within the
 * assembly. Several enums are available to load special images available with
 * the assembly.
 *
 * Author:  M. G. Slack
 * Written: 2013-12-05
 *
 * ----------------------------------------------------------------------------
 * 
 * Updated: 2014-01-01 - Added suit placeholders.
 *
 */
namespace PlayingCards
{
    /*
     * Enum used to define the available card back images.
     */
    public enum CardBacks { Spheres = 200, Blue, Red, Mountains, CheckerBoard, Music, Beany, Duke };

    /*
     * Enum used to define the available placeholder images.
     */
    public enum CardPlaceholders { RedX = 300, GreenCircle, Gray, White, Club, Diamond, Heart, Spade };

    /*
     * Enum used to define the available joker images.
     */
    public enum CardJokers { Red = 400, Black };

    public class PlayingCardImage
    {
        /* Width of card images (in pixels). */
        public const int IMAGE_WIDTH = 71;
        /* Height of card images (in pixels). */
        public const int IMAGE_HEIGHT = 96;
        /* Good offset value to use when laying cards on top of each other (in pixels). */
        public const int IMAGE_OFFSET = 15;

        private const string IMAGE_NAMESPACE = "PlayingCards.images.";
        private const string IMAGE_EXT = ".gif";

        private Dictionary<string, Bitmap> imageCache = new Dictionary<string, Bitmap>();

        // --------------------------------------------------------------------

        /*
         * Placeholder constructor.
         */
        public PlayingCardImage() { }

        // --------------------------------------------------------------------

        private Bitmap LoadImage(int imageNum)
        {
            string imageName = Convert.ToString(imageNum) + IMAGE_EXT;
            string path = IMAGE_NAMESPACE + imageName;
            Bitmap bitmap = null;

            if (imageCache.ContainsKey(path)) { return imageCache[path]; }

            try {
                Assembly asm = Assembly.GetExecutingAssembly();
                Stream stream = asm.GetManifestResourceStream(path);
                bitmap = new Bitmap(stream);

                if (bitmap != null) { imageCache.Add(path, bitmap); }
            }
            catch (Exception e) {
                MessageBox.Show("Image (" + imageName + "): " + e.Message, "LoadImage Error");
            }

            return bitmap;
        }

        // --------------------------------------------------------------------

        /*
         * Method returns a bitmap image of the PlayingCard passed in. Will
         * return null if the card is not a valid PlayingCard. The images for
         * Jokers will be returned if passed in and are valid Joker cards.
         */
        public Bitmap GetCardImage(PlayingCard card)
        {
            int deckValue = card.GetCardPointValueInDeck();
            Bitmap ret = null;

            // joker check (reset 0 to image number to load)
            if (card.CardValue == CardValue.Joker) {
                if (card.Suit == Suit.Hearts)
                    deckValue = (int) CardJokers.Red;
                else if (card.Suit == Suit.Spades)
                    deckValue = (int) CardJokers.Black;
            }

            if (deckValue != 0) ret = LoadImage(deckValue);

            return ret;
        }

        /*
         * Method returns one of the available card back images. May
         * return null if image can't be loaded or is invalid.
         */
        public Bitmap GetCardBackImage(CardBacks back)
        {
            return LoadImage((int) back);
        }

        /*
         * Method returns one of the available card placeholder images. May
         * return null if image can't be loaded or is invalid.
         */
        public Bitmap GetCardPlaceholderImage(CardPlaceholders holder)
        {
            return LoadImage((int) holder);
        }

        /*
         * Method returns either the red or black joker image based on which
         * joker was passed in. This will most likely be used mainly for
         * testing since joker images (as PlayingCards) are returned via the
         * GetCardImage method. May return null if image can't be loaded or
         * is invalid.
         */
        public Bitmap GetJokerImage(CardJokers joker)
        {
            return LoadImage((int) joker);
        }
    }
}
