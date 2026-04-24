using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Reflection.Metadata.BlobBuilder;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static UdemyCodingExercises.Problems11To20;
using static UdemyCodingExercises.Problems31To40;

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


        // #33: Produce Fibonacci sequence
        // A math tool needs to generate Fibonacci numbers up to a given limit as an iterator method that can be lazily evaluated in a foreach loop(using "yield" is crucial here).
        // The Fibonacci sequence is a series of numbers where each number is the sum of the two preceding ones, typically starting with 0 and 1, resulting in a sequence like 0, 1, 1, 2, 3, 5, 8, and so on.
        // Your task is to implement the GetFibonacciSequence method to produce Fibonacci numbers(starting with 0, 1) up to a specified cap, stopping immediately if the cap is negative or when a number exceeds the cap.The method takes an integer cap and returns an IEnumerable<int> with the sequence.
        public static IEnumerable<int> GetFibonacciSequence(int cap)
        {
            int previous = 0;
            int current = 1;

            while (previous <= cap)
            {
                yield return previous;

                int temp = previous + current;
                previous = current;
                current = temp;
            }
        }


        // #34: Calculate discounted price
        // A pricing system needs to compute the final price of goods after applying a discount and tax based on their type.
        // Your task is to implement the CalculateDiscountedPrice method to calculate the discounted price for a given base price and goods type, with an optional discount percentage.
        // The algorithm for calculating the price is as follows:
        // Validate that basePrice and discountPercentage are non-negative, throwing an ArgumentOutOfRangeException if either is negative.Validate that discountPercentage does not exceed 100, throwing an ArgumentOutOfRangeException if it does.
        // Calculate the price after the discount. To do that, take the base price and reduce it by the discount percentage. For example, if the base price is 200, and the discount percentage is 20%, the price after the discount will be 160 (80% of 200).
        // Apply the tax to the price after the discount using the provided ApplyTax method.
        // For luxury goods, apply a tax by calling ApplyTax with the price after discount and LuxuryGoodsExtraTax
        // For basic goods, call ApplyTax with just the price after discount.
        // For example, if the price after discount is 160, and we have basic goods, the price after the tax will be 171.2 (160 + 7% of 160, which is 160 + 11.2 = 171.2).
        // Return the result from ApplyTax as the final price.
        private const int BasicGoodsTax = 7;
        private const int LuxuryGoodsExtraTax = 9;

        public static decimal CalculateDiscountedPrice(decimal basePrice, GoodsType goodsType, decimal discountPercentage = 10m)
        {
            if (basePrice < 0 || discountPercentage < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (discountPercentage > 100)
            {
                throw new ArgumentOutOfRangeException();
            }

            decimal priceAfterDiscount = basePrice * (1 - (discountPercentage / 100m));

            return goodsType == GoodsType.Luxury ? 
                ApplyTax(priceAfterDiscount, LuxuryGoodsExtraTax) : 
                ApplyTax(priceAfterDiscount);
        }

        // For problem #34
        public static decimal ApplyTax(decimal amount, decimal extraTax = 0m)
        {
            var taxRate = BasicGoodsTax + extraTax;
            if (amount < 0 || taxRate < 0)
            {
                throw new ArgumentException(
                    "Amount and tax rate must be non-negative.");
            }

            return amount * (1 + taxRate / 100m);
        }

        // For problem #34
        public enum GoodsType
        {
            Basic,
            Luxury
        }

        // Problem #35: Reverse a string without built-in methods
        // A text processor needs to reverse a string without relying on built-in reverse methods.
        // Your task is to implement the ReverseString method to take a string input and return it with its characters in reverse order, handling null input by throwing an ArgumentNullException.
        public static string ReverseString(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input), "Input cannot be null.");
            }

            string reversedString = string.Empty;

            for (int i = input.Length - 1; i >= 0; i--)
            {
                Console.WriteLine($"Index: {i} => {input[i]}");
                reversedString += input[i];
                Console.WriteLine($"Reversed String = {reversedString}");
            }

            return reversedString;
        }


        // Problem #36: Custom book sorting with IComparer
        // Comparers like IComparer<T> are used to customize sorting for collections, such as ordering objects in a list by specific properties or in non-default ways, enabling flexible comparisons in applications like inventory systems or data displays.
        // Your task is to implement the Compare method in the BookTitleComparer class to compare two Book objects based on their Title properties, using the provided Book class. The method should return an integer indicating the order(negative if x<y, zero if x = y, positive if x > y), handling null cases appropriately(null books should go before non-null in sorting order).
        public class BookTitleComparer : IComparer<Book>
        {
            public int Compare(Book? book1, Book? book2)
            {
                if (book1 == null && book2 == null)
                {
                    return 0;
                }
                
                if (book1 == null && book2 != null)
                {
                    return -1;
                }

                if (book1 != null && book2 == null)
                {
                    return 1;
                }

                return string.Compare(book1?.Title, book2?.Title, StringComparison.OrdinalIgnoreCase);
            }
        }

        // For problem #36
        public class Book
        {
            public string Title { get; }
            public int PageCount { get; }

            public Book(string title, int pageCount)
            {
                Title = title;
                PageCount = pageCount;
            }
        }
    }
}
