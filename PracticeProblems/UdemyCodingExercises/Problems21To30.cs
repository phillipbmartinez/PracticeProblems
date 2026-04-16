using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static UdemyCodingExercises.Problems11To20;

namespace UdemyCodingExercises
{
    internal class Problems21To30
    {
        // 21: Generate random discount codes
        // A store wants to create unique discount codes for its customers, consisting of random letters and numbers(e.g., "X7K9P2"). The DiscountCodeGenerator class uses an injected IRandom interface to pick characters from a predefined set. (IRandom works exactly the same as the Random class, but allows testability of the DiscountCodeGenerator).
        // Complete the body of the GenerateDiscountCode method according to those requirements:
        // It must generate a string of a length given as an argument
        // Each character of this string must be randomly selected from the "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789" characters (already defined in the Characters field). Randomly select each character of the code from the Characters field by using IRandom to generate an index from 0 to Characters.Length, then assign the character at that index to the code.
        public class DiscountCodeGenerator
        {
            private readonly IRandom _random;
            private const string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            public DiscountCodeGenerator(IRandom random)
            {
                _random = random;
            }

            public string GenerateDiscountCode(int length)
            {
                string discountCode = string.Empty;

                for (int i = 0; i < length; i++)
                {
                    discountCode += Characters[_random.Next(0,Characters.Length)];
                }

                return discountCode;

                // Solution from video
                // char[] code = new char[length];

                // for (int i = 0; i < length; i++)
                // {
                //     code[i] = Characters[_random.Next(0, Characters.Length)];
                // }

                // return new string(code);
            }
        }

        // For problem #21
        //those types allow tesing of the DiscountCodeGenerator
        public interface IRandom
        {
            int Next(int minValue, int maxValue);
        }

        // For problem #21
        public class RandomWrapper : IRandom
        {
            private readonly Random _random = new Random();

            public int Next(int minValue, int maxValue)
            {
                return _random.Next(minValue, maxValue);
            }
        }


        // #22: Validate user registration data
        // Implement the RegisterUser that validates user registration data.The method takes four parameters: username (string), password (string), age (int), and email (string). Your task is to validate these inputs based on the following rules:
        // Username: Cannot be null or empty.
        // If null, throw an ArgumentNullException.
        // If empty, throw an ArgumentException.
        // Password: Must be at least 8 characters long.
        // If invalid, throw an ArgumentException.
        // Age: Must be between 18 and 120 (inclusive).
        // If invalid, throw an ArgumentOutOfRangeException.
        // Email: Must contain both "@" and ".".
        // If invalid, throw a FormatException.
        // If all inputs are valid, the method should only print "User registered successfully!" to the console.
        internal static void RegisterUser(string username, string password, int age, string email)
        {
            ArgumentNullException.ThrowIfNull(username);

            if (username == "")
            {
                throw new ArgumentException("Username was empty");
            }

            if (password.Length < 8)
            {
                throw new ArgumentException("Password must be at least 8 characters long.");
            }

            if (age < 18 || age > 120)
            {
                throw new ArgumentOutOfRangeException(nameof(age));
            }

            if (!email.Contains('@') || !email.Contains('.'))
            {
                throw new FormatException("Email was not formatted properly.");
            }

            Console.WriteLine("User registered successfully!");
        }


        // #23: Check if parentheses are balanced
        // A code validator needs to check if the parentheses in an expression are balanced.We consider parentheses balanced if every opening parenthesis '(' has a matching closing parenthesis ')', and they are properly nested with no leftovers.
        // For example (a + b) is balanced, but(a + b or)a + b(are not.
        // Implement the AreParenthesesBalanced method in the Exercise class. The method takes a string with parentheses and returns true if they’re balanced, false otherwise.Focus only on round parentheses '(' and ')', ignoring other characters.
        // Consider using Stack to solve this exercise.
        internal static bool AreParenthesesBalanced(string expression)
        {
            Stack stack = new Stack();

            foreach (char c in expression)
            {
                if (c == '(')
                {
                    stack.Push(c);
                }
                else if (c == ')')
                {
                    if (stack.Count == 0)
                    {
                        return false; // No matching opening parenthesis
                    }
                    stack.Pop();
                }
            }

            return stack.Count == 0;
        }


        // #24: Add matrices with operator overloading
        // A math library needs to add two matrices together using a custom Matrix class. The class uses a 2D array(_data) to store integer values and has fields for row count(_rowsCount) and column count(_columnsCount) set in the constructor.
        // Your task is to:
        // Add an indexer to access and modify elements with matrix[row, column] syntax.
        // Overload the + operator to perform element-wise addition of two matrices, where the result matrix contains the sum of corresponding elements from the two input matrices (e.g., result[row, column] = matrixA[row, column] + matrixB[row, column]). If the matrices have different dimensions, throw an InvalidOperationException.
        public class Matrix
        {
            private readonly int[,] _data;
            private readonly int _rowsCount;
            private readonly int _columnsCount;

            public Matrix(int rowsCount, int columnsCount)
            {
                _rowsCount = rowsCount;
                _columnsCount = columnsCount;
                _data = new int[_rowsCount, _columnsCount];
            }

            public int this[int row, int column]
            {
                get { return _data[row, column]; }
                set { _data[row, column] = value; }
            }

            public static Matrix operator +(Matrix a, Matrix b)
            {
                if (a._rowsCount != b._rowsCount ||
                    a._columnsCount != b._columnsCount)
                {
                    throw new InvalidOperationException("Matrices must have the same dimensions for addition.");
                }

                Matrix result = new Matrix(a._rowsCount, a._columnsCount);
                for (int row = 0; row < a._rowsCount; row++)
                {
                    for (int col = 0; col < a._columnsCount; col++)
                    {
                        result[row, col] = a[row, col] + b[row, col];
                    }
                }
                return result;
            }
        }
    }
}
