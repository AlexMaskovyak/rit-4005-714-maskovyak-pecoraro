using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using _3_SudokuModel;
using _3_SudokuModelAdvanced;
using _3_SudokuTester;

namespace _3_SudokuTesterAdvanced {

    /// <summary>Test Class for a BoardAdvanced Model</summary>
    public class TestAdvanced : Test {

        /// <summary>Default Constructor</summary>
        public TestAdvanced() : base() { }

        /// <summary>Creates an Advanced Board object with the given lines describing a board.</summary>
        /// <param name="boardLines">Strings describing a board</param>
        /// <returns>A new Board</returns>
        protected override IBoard CreateBoard(string[] boardLines) {
            return new BoardAdvanced(boardLines);
        }

        /// <summary>Process commands.</summary>
        /// <param name="commands">The command lines.</param>
        protected override void RunCommands(List<string> commands) {
            foreach (string line in commands) {

                // DEBUG: Command
                Console.WriteLine(line);

                // Split the Arguments to determine the action
                string[] command = line.Split(' ');
                switch (command.Length) {
                    case 2:
                        ProcessSetCommand(command);
                        break;
                    case 1:
                        ProcessClearCommand(command);
                        break;
                    default:
                        throw new InvalidOperationException("Bad Command: " + command);
                }
            }
        }

        /// <summary>Run a Clear Command on the Board</summary>
        /// <param name="command">The command.</param>
        protected virtual void ProcessClearCommand(string[] command) {
            int cell = int.Parse(command[0]);
            BoardAdvanced board = (BoardAdvanced) _board;
            Console.WriteLine("clear in {0}", cell);
            board.Clear(cell);

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
        public new static void Main(string[] args) {
            bool debug = (args.Length > 0);
            TestAdvanced tester = new TestAdvanced();
        }
    }
}
