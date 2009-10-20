using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using _5_SelectingAWinner_ConsoleApplication;

namespace _6_DistributedWinner_Client
{
    /// <summary>Proxy Player</summary>
    public class Remote : IView {

// Fields

        /// <summary>SOAP Service Proxy built from WSDL</summary>
        protected SelectingAWinnerService.PlayerCellServiceSoapClient _proxy;

        /// <summary>Player Id for the Proxy</summary>
        protected int _id;

        /// <summary>Is First Player or not (Assumes two player game)</summary>
        protected bool _isFirst;

// Constructors

        /// <summary>Default Constructor</summary>
        public Remote() {
            _proxy = new SelectingAWinnerService.PlayerCellServiceSoapClient();
            _id = _proxy.Login();
            _isFirst = _proxy.IsFirst(_id);
            Console.WriteLine(_id);
            Console.WriteLine(_isFirst ? "First" : "Second");
        }

// Properties

        /// <summary> is this the first player or the second player? </summary>
        public bool IsFirst {
            get { return _isFirst; }
        }

// IView Interface

        /// <summary> return <c>0..m-1</c>, index of chosen (and unexposed) card. </summary>
        /// <remarks> calls the Web Service (via the Proxy) to get the complementing Player's selection. </remarks>
        /// <returns> the other players selection. </returns>
        public int Choose() {
            return _proxy.Get(_id);
        }

        /// <summary> find out about a chosen card. </summary>
        /// <remarks> calls the Web Service (via the Proxy) to set this Player's selection </remarks>
        public void Tell(int index, int suit, int value) {
            _proxy.Set(_id, index);
        }

        /// <summary> find out about a round's outcome. </summary>
        /// <remarks> ignored, the symmetric Referees will let the real players know. </remarks>
        public void Winner(bool yes) {
            // ignored
        }

        /// <summary> return once view is ready for a new round. </summary>
        public void Ready() {
            // ignored
            // FIXME: should block on the cell for a dummy value for "ready"?
        }
    }
}
