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


            // TEST WHAT WE KNOW
            if (debug) {

                // Enumerate the Rows
                for (int i = 0; i < 9; ++i) {
                    int rowStarter = i * 9;
                    Console.Write("Row {0}: ({1}) ", i, rowStarter);
                    foreach (int id in board.Row(rowStarter)) {
                        Console.Write(id + " ");
                    }
                    Console.WriteLine();
                }

                // Enumerate the Cols
                for (int i = 0; i < 9; ++i) {
                    Console.Write("Col {0}: ({1}) ", i, i);
                    foreach (int id in board.Column(i)) {
                        Console.Write(id + " ");
                    }
                    Console.WriteLine();
                }

                // Enumerate the Shapes
                int[] shapeStarters = {0,4,7,18,23,42,54,58,69};
                for (int i = 0; i < 9; ++i) {
                    int start = shapeStarters[i];
                    Console.Write("Shape {0}: ({1}) ", i, start);
                    foreach (int id in board.Shape(start)) {
                        Console.Write(id + " ");
                    }
                    Console.WriteLine();
                }

                // Enumerator the Context of some nodes
                for (int i = 0; i < 9; ++i) {
                    int start = shapeStarters[i];
                    Console.Write("Context {0}: ({1}) ", i, start);
                    foreach (int id in board.Context(start)) {
                        Console.Write(id + " ");
                    }
                    Console.WriteLine();
                }

            }



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
