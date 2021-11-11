using System;
using System.Windows.Forms;
using PlayingCards;

namespace PlayingCardsTest
{
    public partial class TestWindow : Form
    {
        private CardDeck cardDeck;
        private PlayingCardImage images = new PlayingCardImage();
        private Random rnd = new Random();

        public TestWindow()
        {
            InitializeComponent();
        }

        private void DisplayDeck()
        {
            int i = 0;

            showResults.Clear();
            showResults.AppendText(cardDeck.ToString() + Environment.NewLine);

            foreach (PlayingCard card in cardDeck.GetDeckCards()) {
                showResults.AppendText(Convert.ToString(++i) + ") " + card.ToString() + Environment.NewLine);
            }
        }

        private void TestWindow_Load(object sender, EventArgs e)
        {
            numDecks.SelectedIndex = 0;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            NumberOfDecks numDs = (NumberOfDecks) Convert.ToInt32(numDecks.SelectedItem);
            DeckOptions opts = DeckOptions.None;

            if (rbJokers.Checked)
                opts = DeckOptions.IncludeJokers;
            else if (rbPinochle.Checked)
                opts = DeckOptions.PinochleDeck;

            cardDeck = new CardDeck(numDs, opts);
            btnShuffle.Enabled = true;
            btnShow.Enabled = true;
            DisplayDeck();
        }

        private void btnShuffle_Click(object sender, EventArgs e)
        {
            cardDeck.Shuffle();
            DisplayDeck();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (!cardDeck.HasMoreCards()) btnShuffle_Click(sender, e);
            image.Image = images.GetCardImage(cardDeck.GetNextCard());
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
