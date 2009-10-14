﻿using System;
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

using Thread = System.Threading.Thread;
using ThreadStart = System.Threading.ThreadStart;

using _5_SelectingAWinner_ConsoleApplication;

namespace _5_SelectingAWinner_GUIApplication {

    /// <summary> driver program </summary>
    class SelectingAWinner {

        /// <summary> number of cards in the game </summary>
        protected int _numCards;

        /// <summary> number of players in the game </summary>
        protected int _numPlayers;

        /// <summary> source for images </summary>
        protected string _imageURI;

        /// <summary> random number generator seed. </summary>
        protected int _seed;

        /// <summary> asynchronous counter of created views. </summary>
        protected int _created = 0;

        /// <summary> the referee </summary>
        protected IReferee<IView> _referee;

        /// <summary> used for locking </summary>
        private static object monitor = new object();

        /// <summary> constructor </summary>
        /// <param name="numCards"> number of cards. </param>
        /// <param name="numPlayers"> number of players. </param>
        /// <param name="imageURI"> images source. </param>
        /// <param name="seed"> random number generator seed. </param>
        public SelectingAWinner(int numCards, int numPlayers, string imageURI, int seed) {
            _numCards = numCards;
            _numPlayers = numPlayers;
            _imageURI = imageURI;
            _seed = seed;
        }

        /// <summary> factory method for creating a referee </summary>
        /// <returns> a new referee </returns>
        protected virtual IReferee<IView> CreateReferee(int numCards, int numPlayers, int seed) {
            return new Referee(numCards, numPlayers, seed);
        }

        /// <summary> factory method for creating a player </summary>
        /// <returns> a new view </returns>
        protected virtual IView CreateView(int numCards, string imageURI) {
            return new CardGameViewWindow(numCards, imageURI);
        }

        /// <summary> run the game. </summary>
        public virtual void Run() {

            // Run the Referee in this thread
            _referee = CreateReferee(_numCards, _numPlayers, _seed);

            // Create Views in their own threads
            for (int i=0; i<_numPlayers; ++i) {
                Thread t = new Thread(new ThreadStart(delegate {
                    CardGameViewWindow view = (CardGameViewWindow)CreateView(_numCards, _imageURI);
                    _referee.Join(view);
                    TriggerStart();
                    view.ShowDialog();
                }));
                t.SetApartmentState(System.Threading.ApartmentState.STA);
                t.Start();
            }
            
        }

        /// <summary> Only start once all the Views have Joined the Referee</summary>
        protected void TriggerStart() {
            lock (monitor) {
                _created++;
                if (_created == _numPlayers) {
                    new Thread(new ThreadStart(delegate { _referee.Start(); })).Start();
                }
            }
        }

        /// <summary> run the game </summary>
        [System.STAThreadAttribute()]
        public static void Main(string[] args) {

            // TODO: Remove this, it is helper debug code
            // ----------
            if (args.Length == 0) {
                args = new string[] { "5", "2", "http://www.cs.rit.edu/~ats/cs-2009-1/2/Release/images/", "1" };
            }
            // ----------

            // Usage
            if (args == null || args.Length < 2) {
                Console.WriteLine("usage: SelectingAWinner <numCards> <numPlayers> <imageURI> [ <seed> ]");
                Environment.Exit(1);
            }

            // Arguments
            int numCards = int.Parse(args[0]);
            int numPlayers = int.Parse(args[1]);
            string imageURI = args[2];
            int seed = (args.Length == 4) ? int.Parse(args[3]) : (int)DateTime.Now.Ticks;

            // Launch the Driver
            SelectingAWinner driver = new SelectingAWinner(numCards, numPlayers, imageURI, seed);
            driver.Run();

        }

    }
}