using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ATS.Winner;

namespace _6_DistributedWinner_Client
{
    /// <summary></summary>
    public class ProxyPlayer : IView
    {
        public ProxyPlayer()
        {

        }

        /// <summary> return <c>0..m-1</c>, index of chosen (and unexposed) card. </summary>
        public int Choose()
        {

        }

        /// <summary> find out about a chosen card. </summary>
        public void Tell(int index, int suit, int value)
        {

        }

        /// <summary> find out about a round's outcome. </summary>
        public void Winner(bool yes)
        {

        }

        /// <summary> return once view is ready for a new round. </summary>
        public void Ready()
        {

        }
    }
}
