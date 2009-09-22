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
using System.Windows.Media.Effects;
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

// Constants

        /// <summary>URI Prefix for Card Images</summary>
        public const string PlayingCardURIPrefix = "http://www.cs.rit.edu/~jjp1820/csharp/classic-cards/";

        /// <summary>URI Suffix for Card Images</summary>
        public const string PlayingCardURISuffix = ".png";

        /// <summary>Text Displayed when Computing</summary>
        public const string ComputingText = "Computing...";

        /// <summary>Text Displayed on Win</summary>
        public const string WinningText = "Great!";

        /// <summary>Text Displayed on Lose</summary>
        public const string LosingText = "Oops, there is a better hand.";

// Members

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

        /// <summary>Selected Cards</summary>
        private BitArray _selected;

        /// <summary>Game elements are disabled.</summary>
        private bool _disabled;

// Constructors

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
            _images = new List<Image>();
            _chks = new List<CheckBox>();
            _deck = new Deck();
            _disabled = false;

            // Create/Draw Components
            InitializeComponent();
            CreateGUIObjects();
            this.Width = (100 * _numCardsToDisplay) + 50; // 25px padding both sides

            // Run the Game
            RestartPuzzle();

        }

// Methods

        /// <summary>Programmatically Creates StackPanels with Images and Checkboxes</summary>
        public void CreateGUIObjects() {
            int initialLeftMargin = 20;
            int offsetLeftMargin = 0;
            MouseButtonEventHandler mousedown = new MouseButtonEventHandler(img_MouseDown);
            RoutedEventHandler click = new RoutedEventHandler(chk_Click);
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
                img.MouseDown += mousedown;
                chk.Click += click;

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
                Image img = _images[cardNum];
                img.Source = new BitmapImage(CardToUri(card));
                img.Effect = null;
                _chks[cardNum].IsChecked = false;
                _selected[cardNum] = false;
                _puzzle.Add(card);
                ++cardNum;
            }

            // If Needed
            EnableGameGUI();

        }

        /// <summary>Evalutes the User's Selections and Displays Results</summary>
        private void EvaluateSelection() {
            DisableGameGUI();
            lblResult.Content = ComputingText;
            Result result = _puzzle.Selected(_selected);
            HighlightCards(result.BestHandIndexes);
            lblResult.Content = (result.Best ? WinningText : LosingText);
        }

        /// <summary>Highlight Cards Indicated by their Position</summary>
        /// <param name="highlights">Positions of the Cards</param>
        private void HighlightCards(BitArray highlights) {

            // Red Color
            Color color = new Color();
            color.ScA = 1;
            color.ScB = 0;
            color.ScG = 0;
            color.ScR = 2;

            // Drop Shadow
            DropShadowEffect drop = new DropShadowEffect();
            drop.Color = color;
            drop.Opacity = 0.6;
            drop.ShadowDepth = 11;
            drop.Direction = 320;

            // Add Effect to the Best Hand
            for (int i = 0; i < highlights.Length; ++i) {
                if (highlights[i]) {
                    _images[i].Effect = drop;
                }
            }

        }

        /// <summary>Convert a Card to the URI for its Image</summary>
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
            int rankIdentifier = 14 - ((int)card.Rank); // 14 = Ace
            int cardIdentifier = (4 * (rankIdentifier)) + suitIdentifier;
            string uri = PlayingCardURIPrefix + cardIdentifier.ToString() + PlayingCardURISuffix;
            return new Uri(uri);
        }

        /// <summary>Disable the Game's GUI</summary>
        private void DisableGameGUI() {
            _disabled = true;
            btnNewGame.IsEnabled = true;
            lblResult.Visibility = Visibility.Visible;
            foreach (CheckBox c in _chks) {
                c.IsEnabled = false;
            }
        }

        /// <summary>Enable the Game's GUI</summary>
        private void EnableGameGUI() {
            _disabled = false;
            btnNewGame.IsEnabled = false;
            lblResult.Visibility = Visibility.Hidden;
            foreach (CheckBox c in _chks) {
                c.IsEnabled = true;
            }
        }

// Event Handlers

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
            if (_disabled) { return; }
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
