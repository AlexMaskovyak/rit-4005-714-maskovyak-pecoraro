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

        protected bool _skipTell;

// Constructors

        /// <summary>Default Constructor</summary>
        public Remote() {
            _skipTell = false;
            _proxy = new SelectingAWinnerService.PlayerCellServiceSoapClient();
            _id = _proxy.Login();
            _isFirst = _proxy.IsFirst(_id);
            Console.WriteLine(_id);
            Console.WriteLine(_isFirst ? "First" : "Second");
        }

// Properties

        /// <summary> is this the first player or the second player? </summary>
        public virtual bool IsFirst {
            get { return _isFirst; }
        }

// IView Interface

        /// <summary> return <c>0..m-1</c>, index of chosen (and unexposed) card. </summary>
        /// <remarks> calls the Web Service (via the Proxy) to get the complementing Player's selection. </remarks>
        /// <returns> the other players selection. </returns>
        public virtual int Choose() {
            Console.WriteLine("Proxy is Fetching");
            int i = _proxy.Get(_id);
            _skipTell = true;
            Console.WriteLine("Proxy got {0}", i);
            return i;
        }

        /// <summary> find out about a chosen card. </summary>
        /// <remarks> calls the Web Service (via the Proxy) to set this Player's selection </remarks>
        public virtual void Tell(int index, int suit, int value) {
            if (_skipTell) {
                _skipTell = false;
                return;
            }

            Console.WriteLine("Proxy is Sending");
            _proxy.Set(_id, index);
            Console.WriteLine("Proxy sent {0}", index);
            //_proxy.Get(_id); // should come right back due to symmetry
        }

        /// <summary> find out about a round's outcome. </summary>
        /// <remarks> ignored, the symmetric Referees will let the real players know. </remarks>
        public virtual void Winner(bool yes) {
            // ignored
        }

        /// <summary> return once view is ready for a new round. </summary>
        public virtual void Ready() {
            if (IsFirst)
                _proxy.Get(_id); // get dummy value that other player is ready
            else
                _proxy.Set(_id, 0); // set dummy value that player is ready
        }
    }
}
