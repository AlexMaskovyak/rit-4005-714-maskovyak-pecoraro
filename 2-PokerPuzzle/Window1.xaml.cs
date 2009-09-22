using System;
using BitArray = System.Collections.BitArray;
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

        /// <summary>Selected Cards Count</summary>
        private int _numSelected = 0;

        /// <summary>The Selected Cards</summary>
        private BitArray _selected;

        /// <summary>Creates a GUI with the default, 7 Cards.</summary>
        public Window1(): this(7)
        {}

        /// <summary>Creates a GUI with a specified number of cards showing.</summary>
        /// <param name="cardsToDisplay">Number of Cards in the Puzzle</param>
        public Window1(int cardsToDisplay) {

            // Initialize Data Structures
            _numCardsToDisplay = cardsToDisplay;
            _selected = new BitArray(_numCardsToDisplay);
            _puzzle = new Puzzle(_numCardsToDisplay);
            _deck = new Deck();
            _images = new List<Image>();
            _chks = new List<CheckBox>();

            // Create/Draw Components
            InitializeComponent();
            CreateGUIObjects();
            this.Width = (100 * _numCardsToDisplay) + 50; // 25px padding both sides

            // Run the Game
            RestartPuzzle();

        }

        /// <summary>Programmatically Creates StackPanels with Images and Checkboxes</summary>
        public void CreateGUIObjects() {
            int initialLeftMargin = 20;
            int offsetLeftMargin = 0;
            for (int i = 0; i < _numCardsToDisplay; ++i) {

                // Image
                Image img = new Image();
                img.Height = 96;
                img.Width = 72;
                img.Stretch = Stretch.Fill;
                _images.Add(img);

                // Checkbox
                CheckBox chk = new CheckBox();
                chk.Margin = new Thickness(15);
                chk.Height = 16.25;
                chk.Width = 16.25;
                _chks.Add(chk);

                // StackPanel
                int leftMargin = initialLeftMargin + offsetLeftMargin;
                StackPanel panel = new StackPanel();
                panel.Width = 100;
                panel.HorizontalAlignment = HorizontalAlignment.Left;
                panel.Margin = new Thickness(leftMargin, 12.5, 0, 60);
                panel.Children.Add(img);
                panel.Children.Add(chk);

                // Event Listeners
                img.MouseDown += new MouseButtonEventHandler(img_MouseDown);
                chk.Click += new RoutedEventHandler(chk_Click);

                // Add to the Grid
                grid.Children.Add(panel);
                offsetLeftMargin += 100;
            }
        }

        /// <summary>Displays a new Puzzle.</summary>
        /// <remarks>Shuffles First!</remarks>
        public void RestartPuzzle() {

            // Handle Data Structures
            _deck.Shuffle();
            _puzzle.Clear();
            _numSelected = 0;

            // Pick New Cards and Update GUIs
            int cardNum = 0;
            foreach (PlayingCard card in _deck) {
                if (cardNum == _numCardsToDisplay) { break; }
                _puzzle.Add(card);
                _chks[cardNum].IsChecked = false;
                _images[cardNum].Source = new BitmapImage(CardToUri(card));
                _selected[cardNum] = false;
                ++cardNum;
            }

            // TODO: REMOVE Debug
            foreach (PlayingCard c in _puzzle.Cards) {
                Console.WriteLine(c);
            }
        }

        /// <summary>Evalutes the User's Selections</summary>
        private void EvaluateSelection() {
            Console.WriteLine("Inside Evaluate selection");

            PokerHand bestHand = _puzzle.GetBestHandPossibleAlex();
            Console.WriteLine();
            Console.WriteLine("Best Hand: ");
            Console.WriteLine(bestHand.ScoreHand());
            foreach (PlayingCard c in bestHand) {
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

        /// <summary>"New Game" Button is Clicked</summary>
        private void btnNewGame_Click(object sender, RoutedEventArgs e) {
            RestartPuzzle();
        }

        /// <summary>Checkbox is Clicked</summary>
        private void chk_Click(object sender, RoutedEventArgs e) {
            int index = _chks.IndexOf((CheckBox)sender);
            selectedIndex(index);
        }

        /// <summary>An Image is Clicked, Toggles the Appropriate Checkbox</summary>
        private void img_MouseDown(object sender, MouseButtonEventArgs e) {
            int index = _images.IndexOf((Image)sender);
            _chks[index].IsChecked = !_chks[index].IsChecked;
            selectedIndex(index);
        }

        /// <summary>Handle when an item is selected</summary>
        /// <param name="index">The index of the item in stored lists</param>
        private void selectedIndex(int index) {
            CheckBox chk = _chks[index];
            _selected[index] = !_selected[index];
            _numSelected += (chk.IsChecked == true) ? 1 : -1;
            if (_numSelected >= 5) { // Should never be greater
                EvaluateSelection();
            }
        }

    }
}
