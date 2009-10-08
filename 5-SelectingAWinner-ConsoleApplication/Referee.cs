using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _5_SelectingAWinner_ConsoleApplication
{
    /// <summary>Runs one or more rounds of the Random selection game.</summary>
    public class Referee
    {

        /// <summary>Test for Referee</summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // end
            if (args == null || args.Count != 3) {
                
            }

            int numberOfPlayers = int.Parse(args[0]);
            int numberOfCards = int.Parse(args[1]);
            string urlOfCardsImages = args[2];

            // create referee
            Referee referee = new Referee();

            // create n views


        }
    }
}
