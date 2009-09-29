using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using _3_SudokuModel;

namespace _3_SudokuTester {
    /// <summary>Driver class for testing the capabilities of Board.</summary>
    public class Test {
        public static void Main(string[] args) {
            // determine whether to display output
            bool debug = (args.Length > 0);

            // create the string array of rows to build our Board
            List<string> rows = new List<string>();
            string input;
            while ((input = Console.ReadLine().Trim()) != null && !input.Equals("")) {
                rows.Add(input.Trim());
            }

            // build the board
            Board board = new Board(rows.ToArray<string>());
            Console.WriteLine("Board Done");

            // await set # # commands
            string[] setCommandArgs;
            //int commandIndex = 0;
            int cellIndex = 0;
            int valueIndex = 1;
            while ( (input = Console.ReadLine()) != null ) {

                // End on a Blank Line
                input = input.Trim();
                if (input.Equals("")) {
                    break;
                }

                // Split the Arguments to determine the action
                setCommandArgs = input.Split(' ', ':');
                int cell = int.Parse(setCommandArgs[cellIndex]);
                int value = int.Parse(setCommandArgs[valueIndex]);
                Console.WriteLine("set in {0}:{1}", cell, value);

                // Set
                board.Set(cell, value);

            }

            Console.WriteLine("-- Done --");
        }
    }
}
