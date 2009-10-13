using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using _1_Poker;
using _2_PokerPuzzle;

namespace _5_SelectingAWinner_ConsoleApplication
{
    /// <summary> runs one or more rounds of the random card selection game. </summary>
    public abstract class AbstractReferee<T> : IReferee<T>
    {

// fields
        /// <summary> cell holding player's ready state. </summary>
        protected Cell<bool> _readyCell;

        /// <summary> cell holding a player's selection. </summary>
        protected Cell<int> _chooseCell;

        /// <summary> deck of cards. </summary>
        protected Deck _deck;

        /// <summary> number of cards. </summary>
        protected int _cards;

        /// <summary> maximum number of players. </summary>
        protected int _maxPlayers;

        /// <summary> players for this referee. </summary>
        protected List<T> _players;

// constructors

        /// <summary> default constructor. </summary>
        /// <param name="cards"> number of cards in a game. </param>
        /// <param name="maxPlayers"> maximum number of players to allow for a game. </param>
        /// <param name="seed"> seed to use for deck shuffling. </param>
        public AbstractReferee(int cards, int maxPlayers, int seed) {
            if( cards < maxPlayers ) {
                throw new ArgumentException("The maximum number of players must be less than or equal to the number of cards in the game.");
            }
            _cards = cards;
            _maxPlayers = maxPlayers;
            _players = new List<T>(_maxPlayers);

            _deck = new Deck();
            _deck.Shuffle(seed);
        }

// IReferee interface

        /// <summary> joins a player to this game. </summary>
        /// <param name="player"> player to add. </param>
        public virtual void Join(T player) {
            if (_players.Count == _maxPlayers) {
                throw new InvalidOperationException("This referee has reached the maximum number of players.");
            }
            _players.Add(player);
        }

        /// <summary> remove a player from this game. </summary>
        /// <param name="player"> player to remove. </param>
        public virtual void Leave(T player) {
            _players.Remove(player);
        }

        /// <summary> obtain the players for which this Referee is gamekeeping. </summary>
        /// <returns> players in this game. </returns>
        public virtual IEnumerable<T> Players() {
            return (IEnumerable<T>)_players;
        }

        /// <summary> begins game-playing. </summary>
        public virtual void Start() {
            GameLoop();
        }

// overrideable game logic

        /// <summary> implements main game playing logic, must be overridden. </summary>
        protected abstract void GameLoop();
    }
}
