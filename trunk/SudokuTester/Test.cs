using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using _3_SudokuModel;

namespace _3_SudokuTester {
    /// <summary>Driver class for testing the capabilities of Board.</summary>
    public class Test {

        /// <summary>Sudoku Board</summary>
        protected IBoard _board;

        /// <summary>General Driver for Testing a Board</summary>
        public Test() {

            // Create a Board
            List<string> boardLines = ReadBoardLines();
            _board = CreateBoard(boardLines.ToArray<string>());

            // Read Command Lines
            List<string> commandLines = ReadCommandLines();
            RunCommands(commandLines);

        }

        /// <summary>Reads lines from Standard Input until the first blank line</summary>
        /// <returns>The list of lines</returns>
        protected virtual List<string> ReadBoardLines() {
            return ReadUntilBlankOrEnd();
        }

        /// <summary>Reads lines from Standard Input until the first blank line</summary>
        /// <returns>The list of lines</returns>
        protected virtual List<string> ReadCommandLines() {
            return ReadUntilBlankOrEnd();
        }

        /// <summary>Reads lines from Standard Input until the first blank line</summary>
        /// <returns>The list of lines</returns>
        protected virtual List<string> ReadUntilBlankOrEnd() {
            string input;
            List<string> rows = new List<string>();
            while ((input = Console.ReadLine()) != null) {
                input = input.Trim();
                if (input.Equals("")) {
                    break;
                } else {
                    rows.Add(input);
                }
            }
            return rows;
        }

        /// <summary>Creates a Board object with the given lines describing a board.</summary>
        /// <param name="boardLines">Strings describing a board</param>
        /// <returns>A new Board</returns>
        protected virtual IBoard CreateBoard(string[] boardLines) {
            return new Board(boardLines);
        }

        /// <summary>Process commands.</summary>
        /// <param name="commands">The command lines.</param>
        protected virtual void RunCommands(List<string> commands) {
            foreach (string line in commands) {

                // DEBUG: Command
                Console.WriteLine(line);

                // Split the Arguments to determine the action
                string[] command = line.Split(' ');
                
                // Known to be a set command
                ProcessSetCommand(command);
            }
        }

        /// <summary>Process a Set Command</summary>
        /// <param name="cell"></param>
        /// <param name="value"></param>
        protected void ProcessSetCommand(string[] command) {
            int cell = int.Parse(command[0]);
            int value = int.Parse(command[1]);
            Console.WriteLine("set in {0}:{1}", cell, value);
            _board.Set(cell, value);

            // DEBUG
            ((Board)_board).Debug();
            Console.WriteLine();

            // DEBUG: Print how the context changed
            foreach (int id in _board.Context(cell)) {
                Cell c = ((Board)_board).getCell(id);
                Console.Write("possible in {0}: ", c.Id);
                foreach (int v in c.Values) { Console.Write(v + " "); }
                Console.WriteLine();
            }

        }

        /// <summary>Main Method</summary>
        /// <param name="args">Command Line Arguments</param>
        public static void Main(string[] args) {
            bool debug = (args.Length > 0);
            Test tester = new Test();
        }
    }
}
