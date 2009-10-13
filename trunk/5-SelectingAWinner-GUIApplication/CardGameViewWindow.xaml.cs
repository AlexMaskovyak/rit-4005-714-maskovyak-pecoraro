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
using System.Windows.Shapes;

using _5_SelectingAWinner_UserControlLibrary;
using _5_SelectingAWinner_ConsoleApplication;

namespace _5_SelectingAWinner_GUIApplication {
    
    /// <summary> Interaction logic for CardGameViewWindow.xaml </summary>
    public partial class CardGameViewWindow : Window, IView {

// fields

        /// <summary> cell holding player's ready state. </summary>
        protected Cell<bool> _readyCell;

        /// <summary> cell holding a player's selection. </summary>
        protected Cell<int> _chooseCell;

        /// <summary> number of cards to display </summary>
        protected int _numCards;

        /// <summary> URI prefix for the images to display </summary>
        protected string _imagePrefixURI;

        /// <summary> the card controls </summary>
        protected List<CardUserControl> _cards;

        /// <summary> wether or not its this players turn to choose a card </summary>
        protected bool _isMyTurn = false;

// constructors

        /// <summary> convenience constructor </summary>
        public CardGameViewWindow() : this(5, "http://www.cs.rit.edu/~ats/cs-2009-1/2/Release/images/") { }

        /// <summary> default constructor </summary>
        /// <param name="numCards"> number of cards to display </param>
        /// <param name="imagePrefixURI"> URI Prefix for the card images </param>
        public CardGameViewWindow(int numCards, string imagePrefixURI) {
            _cards = new List<CardUserControl>(_numCards);
            _readyCell = new Cell<bool>();
            _chooseCell = new Cell<int>();
            _imagePrefixURI = imagePrefixURI;
            _numCards = numCards;
            _isMyTurn = false;

            InitializeComponent();
            InitializeUI();
        }

// UI Builders

        /// <summary> build the UI </summary>
        protected virtual void InitializeUI() {

            // Background Images
            Uri[] backgroundImages = new Uri[] {
                new Uri(_imagePrefixURI + "red.png"),
                new Uri(_imagePrefixURI + "blue.png")
            };

            // Add Cards and Handlers
            double leftMargin = 20.0;
            int backgroundCount = backgroundImages.Length;
            CardFlippedEventHandler flipHandler = new CardFlippedEventHandler(Card_Flipped);
            for (int i = 0; i < _numCards; ++i) {
                Uri back = backgroundImages[i % backgroundCount];
                CardUserControl card = new CardUserControl(back);
                card.OnFlip += flipHandler;
                card.Margin = new Thickness(leftMargin, 0, 0, 0);
                card.HorizontalAlignment = HorizontalAlignment.Left;
                card.Tag = i;

                leftMargin += card.Width;
                grid.Children.Add(card);
                _cards.Add(card);
            }

            // Adjust Window size
            Width = leftMargin + 40.0;

            // Disable / Hide / Show Appropriately
            ShowStatus("Waiting for other Players.");
            btnNew.IsEnabled = false;

        }

        /// <summary> set the text in the status label </summary>
        /// <param name="txt"> the text to display </param>
        protected virtual void ShowStatus(string txt) {
            lblStatus.Content = txt;
            lblStatus.Visibility = Visibility.Visible;
        }

        /// <summary> hide the status label </summary>
        protected virtual void HideStatus() {
            lblStatus.Visibility = Visibility.Hidden;
        }

// IView implementation
// NOTE: Referee Thread enters each of these

        /// <summary> return <c>0..m-1</c>, index of chosen (and unexposed) card. </summary>
        public virtual int Choose() {

            // Enable Selection
            _isMyTurn = true;
            lblStatus.Dispatcher.Invoke(new Action(delegate {
                ShowStatus("Choose a Card");
            }));

            // Referee Thread Wait on Value
            int selection = _chooseCell.Value;

            // Disable Selection and return
            _isMyTurn = false;
            return selection;

        }

        /// <summary> find out about a chosen card. </summary>
        public virtual void Tell(int index, int suit, int value) {
            throw new NotImplementedException();
        }

        /// <summary> find out about a round's outcome. </summary>
        public virtual void Winner(bool yes) {
            throw new NotImplementedException();
        }

        /// <summary> return once view is ready for a new round. </summary>
        public virtual void Ready() {
            return; // TODO IMPLEMENT
            //throw new NotImplementedException();
        }

// handlers

        /// <summary> handles new button clicks signifying the start of a new game. </summary>
        /// <param name="sender"> object which initiated this method call. </param>
        /// <param name="e"> event which caused this call. </param>
        private void New_Clicked(object sender, RoutedEventArgs e) {
            _readyCell.Value = true;
        }

        /// <summary> handles a card being flipped. </summary>
        /// <param name="sender"> the card that was clicked </param>
        protected virtual void Card_Flipped(CardUserControl sender) {
            // TODO: Quick Exit if already set inside Value { set; } ?
            // to prevent blocking this thread on double click, etc.
            if (_isMyTurn) {
                _chooseCell.Value = (int)sender.Tag;
            }
        }
        
    }
}
