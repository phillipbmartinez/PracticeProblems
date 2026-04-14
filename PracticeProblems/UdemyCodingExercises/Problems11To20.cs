using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UdemyCodingExercises
{
    internal class Problems11To20
    {
        // #12: Sum a range of values from a variable-length input
        // Implement the SumInRange method that accepts a start index, an end index, and a variable-length list of numbers(using the params keyword).
        // The method should return the sum of all numbers from the start index up to(but not including) the end index.
        // For example, for start index 1, end index 4, and numbers[5, 10, 15, 20, 25, 30, 35] the result shall be 45 (10+15+20).
        // If the start or end indices are outside the bounds of the array, adjust them accordingly:
        // Clamp start to 0 if it's negative
        // Clamp end to the array’s length if it's too large (larger than array's length)
        // If start >= end, return 0
        public static int SumInRange(int start, int end, params int[] numbers)
        {
            int sum = 0;

            if (start < 0)
            {
                start = 0;
            }

            if (end > numbers.Length)
            {
                end = numbers.Length;
            }

            if (start >= end)
            {
                return 0;
            }

            foreach (int num in numbers[start..end])
            {
                sum += num;
            }

            return sum;

            // Using LINQ: return numbers[start..end].Sum();
        }


        // #13: Separate strings and integers from a mixed object list
        // Implement the SeparateObjects method that receives a list of object values.
        // Each value may be an integer, a string, or any other type.
        // The method should return a tuple containing:
        // A list of all int values
        // A list of all string values
        // A count of unknown values that were neither int nor string
        public static (List<int> ints, List<string> strings, int unknownCount) SeparateObjects(List<object> objects)
        {
            List<int> ints = new List<int>();
            List<string> strings = new List<string>();
            int unknownCount = 0;

            foreach (object obj in objects)
            {
                if (obj.GetType() == typeof(int))
                {
                    ints.Add((int)obj);
                }
                else if (obj.GetType() == typeof(string))
                {
                    strings.Add((string)obj);
                }
                else
                {
                    unknownCount++;
                }

                // Option #2:
                // if (obj is int i)
                // {
                //     ints.Add(i);
                // }
                // else if (obj is string s)
                // {
                //     strings.Add(s);
                // }
                // else
                // {
                //     unknownCount++;
                // }
                // Option #2 with LINQ:
                // var integars = objects.OfType<int>().ToList();
                // var string = objects.OfType<string>().ToList();
                // var unknownCount = objects.Count - integars.Count - strings.Count;
                // return (integars, strings, unknownCount);
            }

            return (ints, strings, unknownCount);
        }


        // #14: Sum integers with strict overflow checking
        // Implement the StrictSum method that takes a collection of integers and returns their total as an int.
        // The method must ensure that if the integer overflow* happens(total sum exceeds the bounds of the int type), an OverflowException is thrown.
        // * Integer overflow happens when a calculation exceeds the maximum value an int can hold in C# (2,147,483,647), causing it to wrap around and produce incorrect results.
        internal static int StrictSum(IEnumerable<int> numbers)
        {
            int total = 0;

            foreach (int num in numbers)
            {
                checked
                {
                    total += num;
                }
            }

            return total;

            // Other solution with LINQ
            // return numbers.Sum();
        }

        // #15: Safely sum integers into a long
        // Implement the SafeSum method that takes a collection of integers and returns their total as a long.
        // The goal is to avoid integer overflow* when summing large numbers; make sure the result is always a long, even when the sum of the integers exceeds int.MaxValue.
        internal static long SafeSum(IEnumerable<int> numbers)
        {
            long total = 0;

            foreach (int num in numbers)
            {
                total += num;
            }

            return total;

            // Solution with LINQ
            // return numbers.Sum(number => (long)number);
        }

    // #11: Read file content safely
    // Implement the TryReadFile method that takes a file name and attempts to read it using IFileSystem.ReadFile.
    // If the file exists, return its contents.
    // If the file does not exist (throws FileNotFoundException), log a warning (using ILogger) with the exception message and rethrow the exception.The warning can be a string in any format you like, but it must contain the exception's Message property.
    // Whether the file is found or not, always log "Attempted to read file: [filename]" using the ILogger.
    public class Exercise
    {
        private readonly IFileSystem _fileSystem;
        private readonly ILogger _logger;

        public Exercise(IFileSystem fileSystem, ILogger logger)
        {
            _fileSystem = fileSystem;
            _logger = logger;
        }

        public string TryReadFile(string fileName)
        {
            try
            {
                return _fileSystem.ReadFile(fileName);
            }
            catch (FileNotFoundException ex)
            {
                _logger.Log(ex.Message);
                throw;
            }
            finally
            {
                _logger.Log($"Attempted to read file: {fileName}");
            }
        }
    }

    // For problem #11
    public interface IFileSystem
    {
        string ReadFile(string fileName);
    }

    // For problem #11
    public interface ILogger
    {
        void Log(string message);
    }
}
