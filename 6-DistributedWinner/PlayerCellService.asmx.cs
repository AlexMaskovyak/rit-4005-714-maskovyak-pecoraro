using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;

using _5_SelectingAWinner_ConsoleApplication;


namespace _6_DistributedWinner
{
    /// <summary>Summary description for Service</summary>
    [WebService(Namespace = "http://www.cs.rit.edu/axel/conversions/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class PlayerCellService : System.Web.Services.WebService
    {
        /// <summary> current id to hand-off to a player. </summary>
        protected int _playerId;

        /// <summary> mappings between a cell id and the cell associated with it. </summary>
        protected IDictionary<int, Cell<int>> _cells;

        /// <summary> used for locking </summary>
        private static object monitor = new object();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        /// <summary> default constructor. </summary>
        public PlayerCellService () {
            _playerId = 0;
            _cells = new Dictionary<int, Cell<int>>();
        }

        /// <summary> login protocol for a joining player. </summary>
        /// <returns> player id. </returns>
        [WebMethod]
        public virtual int Login() {
            lock (monitor) {
                if( IsFirst( _playerId )) {
                    /* create a new cell and store it in the repository */
                    Cell<int> c1 = new Cell<int>();
                    Cell<int> c2 = new Cell<int>();
                    _cells.Add(_playerId, c1);
                    _cells.Add(++_playerId, c2);
                    return _playerId; 
                }

                /* give away the cell index to the new connectee */
                int temp = _playerId;
                ++_playerId;
                return temp;
            }
        }

        /// <summary> obtain the value stored for this player's partner. </summary>
        /// <param name="playerId"> our player id. </param>
        /// <returns> value stored from our partner. </returns>
        [WebMethod]
        public virtual int Get(int playerId) {
            lock (monitor) {
                Cell<int> complement = GetComplement(playerId);
                return complement.Value;
            }
        }

        /// <summary> sets the integer value into the player's cell. </summary>
        /// <param name="playerId"> player ID who is setting his cell's value. </param>
        /// <param name="selection"> value to set. </param>
        [WebMethod]
        public virtual void Set(int playerId, int selection ) {
            lock (monitor) {
                _cells[playerId].Value = selection;
            }
        }

        /// <summary> determines whether a player id </summary>
        /// <param name="playerId"> player ID for which to determine firstness. </param>
        /// <returns> true if this is the first player, false otherwise. </returns>
        public bool IsFirst( int playerId ) {
            return ( playerId % 2 == 0);
        }

        /// <summary> obtain a partner's cell. </summary>
        /// <param name="playerId"> player ID for which to obtain the corresponding partner cell. </param>
        /// <returns> partner's cell. </returns>
        protected virtual Cell<int> GetComplement( int playerId ) {
            return (_cells[GetComplementId(playerId)]);
        }
         
        /// <summary> obtain the id of the partner cell. </summary>
        /// <param name="cellId"> player ID for which to obtain the corresponding partner id</param>
        /// <returns> ID of the partner player. </returns>
        protected int GetComplementId( int playerId ) {
            return (IsFirst(playerId))
                    ? playerId + 1
                    : playerId - 1;
        }
    }
}
