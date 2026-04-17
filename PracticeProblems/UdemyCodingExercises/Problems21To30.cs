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
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static UdemyCodingExercises.Problems11To20;
using static UdemyCodingExercises.Problems21To30;

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


        // #25: Track class usage statistics
        // A logging system needs to track how many messages it has processed by assigning each a unique ID.The Logger class uses a static field(_logCounter) to count log entries.
        // Your task is to:
        // Implement a static constructor to initialize _logCounter to 1.
        // Use the provided static field(_logCounter) as the ID.
        // Implement the static method Log to print each message with its ID in brackets(e.g., "[1] Message").
        // Each call to Log should increment the counter so the next message gets the next ID.
        // Use Console.WriteLine to print the message.
        public class Logger
        {
            private static int _logCounter;

            static Logger()
            {
                ResetCounter();
            }

            public static void Log(string message)
            {
                Console.WriteLine($"[{_logCounter}] {message}");
                _logCounter++;
            }

            //do not modify - this method is needed for testing
            public static void ResetCounter()
            {
                _logCounter = 1;
            }
        }


        // #26: Time duration adapter
        // A time-tracking system needs a custom TimeDuration class to work seamlessly with TimeSpan from the.NET framework.The TimeDuration class has a read-only Seconds property(an int) set via its constructor.Your task is to:
        // Implement an implicit conversion from TimeDuration to TimeSpan to allow automatic use in time APIs.
        // Implement an explicit conversion from TimeSpan to TimeDuration to allow casting back, truncating fractional seconds (TimeSpan allows fractional seconds, but TimeDuration does not).
        public class TimeDuration
        {
            public int Seconds { get; }

            public TimeDuration(int seconds)
            {
                Seconds = seconds;
            }

            // This means you can automatically convert a TimeDuration into a TimeSpan without casting. Safe with no data loss
            public static implicit operator TimeSpan(TimeDuration timeDuration) => TimeSpan.FromSeconds(timeDuration.Seconds);
            // This means you must manually cast a TimeSpan into a TimeDuration. Potential data loss
            public static explicit operator TimeDuration(TimeSpan timeSpan) => new TimeDuration((int)timeSpan.TotalSeconds);
        }


        // #27: Count unique elements in a list
        // Implement the CountUnique method that takes a generic List<T> and returns the number of distinct elements it contains.
        public static int CountUnique<T>(List<T> items)
        {
            HashSet<T> uniqueItems = new HashSet<T>();

            foreach (T item in items)
            {
                uniqueItems.Add(item);
            }

            return uniqueItems.Count;

            // Other solution
            // return new HashSet<T>(items).Count;

            // LINQ Solution
            // return items.Distinct().Count();
        }


        // #28: A utility needs a simple container to hold two values of the same type(for example, two ints (1,2) or two strings("hey", "you").
        // Define a generic Pair class that can store two values of the same type.It must have:
        // Two properties, First and Second, giving access to read those values, but not allowing external code to modify them.
        // A constructor to set these values.
        // A public Swap method to exchange the values(it shall be void, and after it is called, First shall hold the value that was earlier in the Second, and vice versa).
        // Use generics to make it work with any type.
        // Remember to make this class public.
        public class Pair<T>
        {
            public T First { get; private set; }
            public T Second { get; private set; }

            public Pair(T first, T second)
            {
                First = first;
                Second = second;
            }

            public void Swap()
            {
                T temp = First;
                First = Second;
                Second = temp;

                // Using a tuple
                // (Second, First) = (First, Second);

                Console.WriteLine($"First: {First}, Second: {Second}");
            }
        }


        // #29: Unique task identifier with string formatting
        // Finish the implementation of the TaskItem class. Each object of this class has an identifier of Guid type, and a string description.
        // Guid is a structure that represents a globally unique identifier, typically generated by Guid.NewGuid to ensure uniqueness across systems using a combination of time, machine-specific data, and random values.
        // Define:
        // A private constructor to set the Id and Description properties.
        // A public static Create method to generate a new TaskItem with a unique GUID and given description.Use Guid.NewGuid method to create a globally unique Guid object. This method should take the description parameter and return TaskItem.
        // An overridden ToString method to return a formatted string like "Task [giud-prefix]: [description]". The Guid prefix should be the first 8 characters of the GUID. See the Example usage below for examples.
        public class TaskItem
        {
            public Guid Id { get; }
            public string Description { get; }

            private TaskItem(Guid id, string description)
            {
                Id = id;
                Description = description;
            }

            public static TaskItem Create(string description)
            {
                return new TaskItem(Guid.NewGuid(), description);
            }

            public override string ToString()
            {
                string guidPrefix = Id.ToString().Substring(0, 8);

                return $"Task [{guidPrefix}]: {Description}";
            }
        }


        // #30: Calculate factorial with recursion
        // A math utility needs to compute the factorial of a non-negative integer
        // (e.g., 5! = 5 * 4 * 3 * 2 * 1 = 120).
        // Important: The factorial of 0 is 1.
        // Your task is to implement the CalculateFactorial method.The method takes an integer and returns its factorial as an int. Throw an ArgumentException if the input is negative since the factorial is undefined for negative numbers.
        public static int CalculateFactorial(int number)
        {
            int factorial = 1;

            if (number < 0)
            {
                throw new ArgumentException("Number cannot be less than 0");
            }

            if (number == 0 || number == 1)
            {
                return 1;
            }

            // Using recursion
            // return number * CalculateFactorial(number - 1);

            for (int i = 2; i <= number; i++)
            {
                factorial = factorial * i;
            }

            return factorial;
        }
    }
}
