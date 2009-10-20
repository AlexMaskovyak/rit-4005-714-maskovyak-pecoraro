using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using _5_SelectingAWinner_ConsoleApplication;

namespace _6_DistributedWinner_Client
{
    /// <summary>Proxy Player</summary>
    public class Remote : IView {

        /// <summary>Dummy value used occasionally</summary>
        protected const int Dummy = -1;

// Fields

        /// <summary>SOAP Service Proxy built from WSDL</summary>
        protected SelectingAWinnerService.PlayerCellServiceSoapClient _proxy;

        /// <summary>Player Id for the Proxy</summary>
        protected int _id;

        /// <summary>Is First Player or not (Assumes two player game)</summary>
        protected bool _isFirst;

        /// <summary>
        ///   Symmetric Referees means a message is told twice,
        ///   once by each referee. This is used to indicate if
        ///   the next Tell() call is a duplicate and should be
        ///   skipped.
        /// </summary>
        protected bool _skipTell;

// Constructors

        /// <summary> Default Constructor </summary>
        public Remote() : this((int)DateTime.Now.Ticks) { }

        /// <summary> Constructor with Known Seed </summary>
        public Remote(int seed) {
            _skipTell = false;
            _proxy = new SelectingAWinnerService.PlayerCellServiceSoapClient();
            _id = _proxy.Login();
            _isFirst = _proxy.IsFirst(_id);
        }

// Properties

        /// <summary> is this the first player or the second player? </summary>
        public virtual bool IsFirst {
            get { return _isFirst; }
        }

// Connection Protocol

        /// <summary> exchange of data between connected players. </summary>
        /// <remarks> player one's data gets used, player two's data is ignored. </remarks>
        /// <param name="data"> the data to send. </param>
        /// <returns> the agreed data. </returns>
        public virtual int[] Exchange(params int[] data) {
            int length = data.Length;
            int[] results = new int[length];

            for (int i = 0; i < length; ++i) {
                int value = data[i];
                if (IsFirst) {                    // Proxy 1
                    _proxy.Set(_id, value);       // send value
                    _proxy.Get(_id);              // receive ACK
                    results[i] = value;
                } else {                          // Proxy 2
                    results[i] = _proxy.Get(_id); // receive value
                    _proxy.Set(_id, Dummy);       // send ACK
                }
            }

            return results;
        }

// IView Interface

        /// <summary> return <c>0..m-1</c>, index of chosen (and unexposed) card. </summary>
        /// <remarks> calls the Web Service (via the Proxy) to get the complementing Player's selection. </remarks>
        /// <returns> the other players selection. </returns>
        public virtual int Choose() {
            int index = _proxy.Get(_id);
            _skipTell = true;
            return index;
        }

        /// <summary> find out about a chosen card. </summary>
        /// <remarks> calls the Web Service (via the Proxy) to set this Player's selection </remarks>
        public virtual void Tell(int index, int suit, int value) {
            if (_skipTell) {
                _skipTell = false;
                return;
            }

            _proxy.Set(_id, index);
        }

        /// <summary> find out about a round's outcome. </summary>
        /// <remarks> ignored, the symmetric Referees will let the real players know. </remarks>
        public virtual void Winner(bool yes) {
            // ignored (see comments)
        }

        /// <summary> return once view is ready for a new round. </summary>
        /// <remarks>
        ///   The Proxy on the Real Player 1 side only returns when the Real Player 2
        ///   has indicated he is ready.  The Proxy on the Real Player 2 side will
        ///   indicate to the referee on the Real Player 1 side when he is ready.
        /// </remarks>
        public virtual void Ready() {
            if (IsFirst) {
                _proxy.Get(_id); // get dummy value that other player is ready
            } else {
                _proxy.Set(_id, Dummy); // set dummy value that player is ready
            }
        }
    }
}
