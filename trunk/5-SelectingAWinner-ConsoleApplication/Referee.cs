using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using _1_Poker;
using _2_PokerPuzzle;

namespace _5_SelectingAWinner_ConsoleApplication
{

    /// <summary> deck of SuitPrecedencePlayingCards. </summary>
    public class SuitPrecedenceDeck : Deck {
        /// <summary> default constructor. </summary>
        public SuitPrecedenceDeck() : base() { }

        /// <summary> overrides creation of PlayingCard objects to created SuitPrecedencePlayingCard objects. </summary>
        protected override void CreatePlayingCards() {
            int index = 0;
            foreach (SuitPrecedencePlayingCard.Suits suit in Enum.GetValues(typeof(SuitPrecedencePlayingCard.Suits))) {
                foreach (SuitPrecedencePlayingCard.Ranks rank in Enum.GetValues(typeof(SuitPrecedencePlayingCard.Ranks))) {
                    if (rank == SuitPrecedencePlayingCard.Ranks.NAR) { continue; }
                    _playingCards[index++] = new SuitPrecedencePlayingCard(rank, suit);
                }
            }
        }
    }

    /// <summary> implements IReferee for use with IView and random card selection game. </summary>
    public class Referee : AbstractReferee<IView> {

// fields

        /// <summary> deck of cards. </summary>
        protected Deck _deck;

        /// <summary> number of cards. </summary>
        protected int _cards;

        /// <summary> cards for the current game. </summary>
        protected List<PlayingCard> _gameCards;

        /// <summary> indices that have been selected by players. </summary>
        protected HashSet<int> _selectedIndices;

        /// <summary> number of rounds to play </summary>
        protected int? _numRounds;

        /// <summary> seed to use for random shuffling. </summary>
        protected int _seed;

// constructors 

        /// <summary> conevenience constructor. </summary>
        public Referee() : this( 5 ) { }

        /// <summary> convenience constructor. </summary>
        /// <param name="cards"> number of cards in a game. </param>
        public Referee(int cards) : this( cards, 2 ) { }

        /// <summary> convenience constructor. </summary>
        /// <param name="cards"> number of cards in a game. </param>
        /// <param name="maxPlayers"> maximum number of players to allow for a game. </param>
        public Referee(int cards, int maxPlayers) : this(cards, maxPlayers, (int)DateTime.Now.Ticks) { }

        /// <summary> constructor </summary>
        /// <param name="cards"> number of cards in a game. </param>
        /// <param name="maxPlayers"> maximum number of players to allow for a game. </param>
        /// <param name="seed"> seed for the random number generator. </param>
        public Referee(int cards, int maxPlayers, int seed) : base(cards, maxPlayers) {
            _cards = cards;
            _numRounds = null;
            _deck = CreateDeck();
            _selectedIndices = new HashSet<int>();
            _gameCards = new List<PlayingCard>(cards);
            _seed = seed;
        }

// properties

        /// <summary> the number of rounds in the game </summary>
        public int? Rounds {
            get { return _numRounds; }
            set { _numRounds = value; }
        }

// factory methods

        /// <summary> factory method to create a Deck </summary>
        /// <returns> a new Deck </returns>
        protected Deck CreateDeck() {
            return new SuitPrecedenceDeck();
        }

// methods

        /// <summary> provides main logic for holding a game. </summary>
        protected override void GameLoop() {
            while (true) {
                _deck.Shuffle(_seed++);

                // rounds left, no value means infinite
                if (_numRounds.HasValue) {
                    if (_numRounds.Value <= 0) {
                        break;
                    } else {
                        _numRounds -= 1;
                    }
                }

                // shuffle the cards and select the first m cards
                _gameCards.Clear();
                _gameCards.AddRange(_deck.Shuffle().Take(_cards));
                _selectedIndices.Clear();

                PlayingCard bestCard = null;  // hold the current best card
                IView winningPlayer = null;   // the player with the best card selected so far

                // ensure that all players are ready
                foreach (IView player in Players) {
                    player.Ready();
                }

                // obtain every player's chosen card index
                // tell all players which card was selected
                foreach (IView player in Players) {
                    int index = player.Choose();
                    // ensure it is in range
                    if (index < 0 || index > (_gameCards.Count - 1)) {
                        throw new IndexOutOfRangeException(
                            String.Format("A card was selected outside of the range of accepted values: 0 through {1}", _gameCards.Count));
                    }

                    // ensure that it hasn't been selected, and add it to the selected list
                    if (!_selectedIndices.Add(index)) {
                        throw new ArgumentException("Invalid index selected.  Only non-selected cards my be chosen.");
                    }

                    // compute new best card and player
                    PlayingCard selectedCard = _gameCards.ElementAt(index);
                    if (bestCard == null || bestCard.CompareTo(selectedCard) < 0) {
                        bestCard = selectedCard;
                        winningPlayer = player;
                    }

                    // tell everyone the result
                    foreach (IView toInform in Players) {
                        toInform.Tell(index, (int)selectedCard.Suit, (int)selectedCard.Rank);
                    }
                }

                // inform everyone of the result
                foreach (IView player in Players) {
                    player.Winner(player == winningPlayer);
                }

            }
        }

// tester

        /// <summary>Test for Referee</summary>
        /// <param name="args">[mCards dealt] i[1]-i[n] cards selected per player</param>
        public static void Main(string[] args) {

            // Terminate early
            if (args == null || args.Count() < 4) {
                System.Console.Error.WriteLine("usage: Referee <seed> <numCards> i[player 1 card index selected]-i[player n card index selected]");
                System.Console.Error.WriteLine("example: Referee 1000 5 1 2 3");
                Environment.Exit(1);
            }

            int seed = int.Parse(args[0]);
            int numberOfCards = int.Parse(args[1]);
            var playerIndexSelections = args.Skip(2);

            // Create referee
            IReferee<IView> referee = new Referee(numberOfCards, playerIndexSelections.Count(), seed);
            ((Referee)referee).Rounds = 1;

            // Create players
            int playerNumber = 0;
            foreach( var selection in playerIndexSelections ) {
                referee.Join(new SimplePlayer(playerNumber++, int.Parse(selection)));
            }

            // Start the game
            referee.Start();

        }
    }
}
