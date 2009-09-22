using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using _1_Poker;

namespace _2_PokerPuzzle
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {

        /// <summary>URI Prefix for Card Images</summary>
        public const string PlayingCardURIPrefix = "http://www.cs.rit.edu/~jjp1820/csharp/classic-cards/";

        /// <summary>URI Suffix for Card Images</summary>
        public const string PlayingCardURISuffix = ".png";

        /// <summary>Number of Cards to Display</summary>
        private int _numCardsToDisplay;

        /// <summary>Puzzle Model</summary>
        private Puzzle _puzzle;

        /// <summary>Deck of Cards</summary>
        private Deck _deck;

        /// <summary>Image GUI Element References</summary>
        private List<Image> _images;

        /// <summary>Checkbox GUI Element References</summary>
        private List<CheckBox> _chks;

        /// <summary>Creates a GUI with the default, 7 Cards.</summary>
        public Window1(): this(7)
        {}

        /// <summary>Creates a GUI with a specified number of cards showing.</summary>
        /// <param name="cardsToDisplay">Number of Cards in the Puzzle</param>
        public Window1(int cardsToDisplay) {

            // Initialize Data Structures
            _numCardsToDisplay = cardsToDisplay;
            _puzzle = new Puzzle(_numCardsToDisplay);
            _deck = new Deck();
            _images = new List<Image>();
            _chks = new List<CheckBox>();

            // Create/Draw Components
            InitializeComponent();
            CreateGUIObjects();
            this.Width = (100 * _numCardsToDisplay) + 50; // 25px padding both sides
            
            // TODO: Add Game Loop Here?

            // Run the Game
            RestartPuzzle();


            PokerHand bestHand = _puzzle.GetBestHandPossibleAlex();
            Console.WriteLine();
            Console.WriteLine("Best Hand: ");
            Console.WriteLine(bestHand.ScoreHand());
            foreach (PlayingCard c in bestHand) {
                Console.WriteLine(c);
            }

        }


        /// <summary>Programmatically Creates StackPanels with Images and Checkboxes</summary>
        public void CreateGUIObjects() {
            int initialLeftMargin = 20;
            int offsetLeftMargin = 0;
            for (int i = 0; i < _numCardsToDisplay; ++i) {

                Image img = new Image();
                img.Height = 96;
                img.Width = 72;
                img.Stretch = Stretch.Fill;
                _images.Add(img);

                CheckBox chk = new CheckBox();
                chk.Margin = new Thickness(15);
                chk.Height = 16.25;
                chk.Width = 16.25;
                _chks.Add(chk);

                int leftMargin = initialLeftMargin + offsetLeftMargin;
                StackPanel panel = new StackPanel();
                panel.Width = 100;
                panel.HorizontalAlignment = HorizontalAlignment.Left;
                panel.Margin = new Thickness(leftMargin, 12.5, 0, 60);
                panel.Children.Add(img);
                panel.Children.Add(chk);
                
                grid.Children.Add(panel);
                offsetLeftMargin += 100;
            }
        }


        /// <summary>Displays a new Puzzle.</summary>
        /// <remarks>Shuffles First!</remarks>
        public void RestartPuzzle() {
            _deck.Shuffle();
            _puzzle.Clear();
            int cardNum = 0;
            foreach (PlayingCard card in _deck) {
                if (cardNum == _numCardsToDisplay) { break; }
                _puzzle.Add(card);
                _images[cardNum].Source = new BitmapImage(CardToUri(card));
                ++cardNum;
            }

            // TODO: REMOVE Debug
            foreach (PlayingCard c in _puzzle.Cards) {
                Console.WriteLine(c);
            }
        }


        /// <summary>
        /// Convert a Card to the URI for its Image</summary>
        /// <param name="card">A valid PlayingCard</param>
        /// <returns>URI for the Card's image</returns>
        public Uri CardToUri(PlayingCard card) {
            int suitIdentifier = 0;
            switch (card.Suit) {
                case PlayingCard.Suits.Club: suitIdentifier = 1; break;
                case PlayingCard.Suits.Spade: suitIdentifier = 2; break;
                case PlayingCard.Suits.Heart: suitIdentifier = 3; break;
                case PlayingCard.Suits.Diamond: suitIdentifier = 4; break;
            }
            int rankIdentifier = 0;
            rankIdentifier = 14 - ((int)card.Rank);
            int cardIdentifier = (4 * (rankIdentifier)) + suitIdentifier;
            string uri = PlayingCardURIPrefix + cardIdentifier.ToString() + PlayingCardURISuffix;
            return new Uri(uri);
        }


        /// <summary>Click the "New Game" Button</summary>
        private void btnNewGame_Click(object sender, RoutedEventArgs e) {
            RestartPuzzle();
        }

    }
}
