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

            // await set # # commands
            string[] setCommandArgs;
            int commandIndex = 0;
            int cellIndex = 1;
            int valueIndex = 2;
            while ((input = Console.ReadLine().Trim()) != null && !input.Equals("")) {

                setCommandArgs = input.Split(' ', ':');
                if (!setCommandArgs[commandIndex].Equals("set") || setCommandArgs.Length != 3) {
                    break;
                }
                board.Set(
                    int.Parse(setCommandArgs[cellIndex]),
                    int.Parse(setCommandArgs[valueIndex]));
            }

            Console.WriteLine("tada");
        }
    }
}
