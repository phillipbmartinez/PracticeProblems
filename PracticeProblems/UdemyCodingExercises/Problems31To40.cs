using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static UdemyCodingExercises.Problems11To20;

namespace UdemyCodingExercises
{
    internal class Problems31To40
    {
        // #31: Check the result of the tic-tac-toe game
        // We need to determine the outcome of a Tic-Tac-Toe game.
        // Your task is to implement the GetTicTacToeResult method to check if a player has won or if the game is a draw. In Tic-Tac-Toe, a player wins by placing three of their symbols ('X' or 'O') in a horizontal, vertical, or diagonal line on a 3x3 grid; if all cells are filled without a winner, the game ends in a draw.
        // The GetTicTacToeResult method takes a 2D array (char[,]) where every cell is either 'X' or 'O' (no empty cells*) and returns a GameResult enum value : XWins, OWins, or Draw.
        // Throw an ArgumentException if the grid isn’t 3x3.
        // Feel free to define helper methods to keep the code clean.
        public static GameResult GetTicTacToeResult(char[,] grid)
        {
            if (grid.Length != 9)
            {
                throw new ArgumentException("Grid is not 3 x 3.");
            }
            
            for (int i = 0; i < grid.Length / 3; i++)
            {
                for (int j = 0; j < grid.Length / 3; j++)
                {
                    Console.Write(grid[i, j]);
                }

                Console.WriteLine();
            }

            if ((grid[0, 0] == 'X' && grid[0, 1] == 'X' && grid[0, 2] == 'X') || (grid[1, 0] == 'X' && grid[1, 1] == 'X' && grid[1, 2] == 'X') || (grid[2, 0] == 'X' && grid[2, 1] == 'X' && grid[2, 2] == 'X'))
            {
                return GameResult.XWins;
            }
            else if ((grid[0, 0] == 'O' && grid[0, 1] == 'O' && grid[0, 2] == 'O') || (grid[1, 0] == 'O' && grid[1, 1] == 'O' && grid[1, 2] == 'O') || (grid[2, 0] == 'O' && grid[2, 1] == 'O' && grid[2, 2] == 'O'))
            {
                return GameResult.OWins;
            }
            else if ((grid[0, 0] == 'X' && grid[1, 0] == 'X' && grid[2, 0] == 'X') || (grid[0, 1] == 'X' && grid[1, 1] == 'X' && grid[2, 1] == 'X') || (grid[0, 2] == 'X' && grid[1, 2] == 'X' && grid[2, 2] == 'X'))
            {
                return GameResult.XWins;
            }
            else if ((grid[0, 0] == 'O' && grid[1, 0] == 'O' && grid[2, 0] == 'O') || (grid[0, 1] == 'O' && grid[1, 1] == 'O' && grid[2, 1] == 'O') || (grid[0, 2] == 'O' && grid[1, 2] == 'O' && grid[2, 2] == 'O'))
            {
                return GameResult.OWins;
            }
            else if ((grid[0, 0] == 'X' && grid[1, 1] == 'X' && grid[2, 2] == 'X') || (grid[0, 2] == 'X' && grid[1, 1] == 'X' && grid[2, 0] == 'X'))
            {
                return GameResult.XWins;
            }
            else if ((grid[0, 0] == 'O' && grid[1, 1] == 'O' && grid[2, 2] == 'O') || (grid[0, 2] == 'O' && grid[1, 1] == 'O' && grid[2, 0] == 'O'))
            {
                return GameResult.OWins;
            }
            else
            {
                return GameResult.Draw;
            }

            // Video solution
            //if (grid.GetLength(0) != 3 || grid.GetLength(1) != 3)
            //{
            //    throw new ArgumentException("Grid is not 3 x 3.");
            //}

            //GameResult? rowWinner = CheckRows(grid);
            //if (rowWinner is not null)
            //{
            //    return rowWinner.Value;
            //}

            //GameResult? columnWinner = CheckColumns(grid);
            //if (columnWinner is not null)
            //{
            //    return columnWinner.Value;
            //}

            //GameResult? diagonalWinner = CheckDiagonals(grid);
            //if (diagonalWinner is not null)
            //{
            //    return diagonalWinner.Value;
            //}

            //return GameResult.Draw;
        }

        // For problem #31
        private static GameResult? CheckRows(char[,] grid)
        {
            for (int row = 0; row < 3; row++)
            {
                if (grid[row, 0] == grid[row, 1] && grid[row, 1] == grid[row, 2])
                {
                    return grid[row, 0] == 'X' ? GameResult.XWins : GameResult.OWins;
                }
            }

            return null;
        }

        // For problem #31
        private static GameResult? CheckColumns(char[,] grid)
        {
            for (int column = 0; column < 3; column++)
            {
                if (grid[0, column] == grid[1, column] && grid[1, column] == grid[2, column])
                {
                    return grid[0, column] == 'X' ? GameResult.XWins : GameResult.OWins;
                }
            }

            return null;
        }

        // For problem #31
        private static GameResult? CheckDiagonals(char[,] grid)
        {

            if (grid[0, 0] == grid[1, 1] && grid[1, 1] == grid[2, 2])
            {
                return grid[0, 0] == 'X' ? GameResult.XWins : GameResult.OWins;
            }

            if (grid[0, 2] == grid[1, 1] && grid[1, 1] == grid[2, 0])
            {
                return grid[0, 2] == 'X' ? GameResult.XWins : GameResult.OWins;
            }

            return null;
        }

        // For problem #31
        public enum GameResult
        {
            Draw,
            XWins,
            OWins
        }


        // #32: Event ticket composed ID
        // An event system needs to compare tickets based on their event name and date.
        // Your task is to implement equality members for the Ticket class by overriding Equals and GetHashCode.The class has EventName(string) and EventDate(DateTime) properties, and two tickets are equal if both properties match exactly.
        public class Ticket
        {
            public string EventName { get; }
            public DateTime EventDate { get; }

            public Ticket(string eventName, DateTime eventDate)
            {
                EventName = eventName;
                EventDate = eventDate;
            }

            public override bool Equals(object obj)
            {
                if (obj is null || obj is not Ticket)
                {
                    return false;
                }

                Ticket other = (Ticket)obj;

                return EventDate == other.EventDate && EventName == other.EventName;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(EventName, EventDate);
            }
        }
    }
}
