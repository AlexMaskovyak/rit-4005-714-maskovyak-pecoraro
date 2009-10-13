using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _5_SelectingAWinner_ConsoleApplication
{
    /// <summary> defines the gamekeeping Referee. </summary>
    /// <typeparam name="T"> player type. </typeparam>
    public interface IReferee<T> {
        /// <summary> joins a player to this game. </summary>
        /// <param name="player"> player to add. </param>
        void Join(T player);

        /// <summary> remove a player from this game. </summary>
        /// <param name="player"> player to remove. </param>
        void Leave(T player);

        /// <summary> obtain the players for which this Referee is gamekeeping. </summary>
        /// <returns> players in this game. </returns>
        IEnumerable<T> Players();

         /// <summary> begins game-playing. </summary>
        void Start();
    }
}
