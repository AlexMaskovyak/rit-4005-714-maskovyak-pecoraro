using System;
using System.Collections.Generic;
using System.ComponentModel;
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

using Thread = System.Threading.Thread;
using ThreadStart = System.Threading.ThreadStart;

using _5_SelectingAWinner_ConsoleApplication;
using _5_SelectingAWinner_GUIApplication;

namespace _6_DistributedWinner_Client
{

    /// <summary> driver program </summary>
    public class DistributedSelectingAWinner : SelectingAWinner {

// Constructors

        /// <summary> constructor </summary>
        /// <remarks> the number of players are guarenteed to be 2 </remarks>
        /// <param name="numCards"> number of cards. </param>
        /// <param name="imageURI"> images source. </param>
        /// <param name="seed"> random number generator seed. </param>
        public DistributedSelectingAWinner(int numCards, string imageURI, int seed)
            : base(numCards, 2, imageURI, seed) { }

// Factory Methods

        /// <summary> template method for createing a proxy player. </summary>
        /// <returns> a proxy player. </returns>
        protected IView CreateProxyPlayer() {
            return new Remote();
        }

        /// <summary> run the game. </summary>
        public override void Run() {

            // Create Real Player View
            CardGameViewWindow realPlayer = (CardGameViewWindow)CreateView(_numCards, _imageURI);
            realPlayer.Show();

            // Create Proxy Player
            Remote proxyPlayer = (Remote)CreateProxyPlayer();

            // Create Referee and have the players join in the proper order
            _referee = CreateReferee(_numCards, 2, _seed);
            if (proxyPlayer.IsFirst) {
                Console.WriteLine("I am player 1");
                _referee.Join(proxyPlayer);
                _referee.Join(realPlayer);
            } else {
                Console.WriteLine("I am player 2");
                _referee.Join(realPlayer);
                _referee.Join(proxyPlayer);
            }

            // run referee in background thread
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(delegate { _referee.Start(); });
            worker.RunWorkerAsync();

            // run event loop in this thread
            App app = new App();
            app.Run();

        }

        /// <summary> run the game </summary>
        [System.STAThreadAttribute()]
        public static new void Main(string[] args) {

            // Debug Mode: 1 command line argument "debug" or NO command line arguments
            // NOTE: Hardcoded seed
            if (args == null || args.Length == 0 || (args.Length == 1 && args[0] == "debug")) {
                args = new string[] { "5", "http://www.cs.rit.edu/~ats/cs-2009-1/2/Release/images/", "1" };
            }

            // Usage
            if (args == null || args.Length < 2) {
                Console.WriteLine("usage: SelectingAWinner <numCards> [ <imageURI> <seed> ]");
                Environment.Exit(1);
            }

            // Arguments
            int numCards = int.Parse(args[0]);
            string imageURI = (args.Length > 2) ? args[1] : "http://www.cs.rit.edu/~ats/cs-2009-1/2/Release/images/";
            int seed = (args.Length > 3) ? int.Parse(args[2]) : (int)DateTime.Now.Ticks;

            // Launch the Driver
            DistributedSelectingAWinner driver = new DistributedSelectingAWinner(numCards, imageURI, seed);
            driver.Run();

        }

    }
}
