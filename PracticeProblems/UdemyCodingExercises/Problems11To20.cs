using System;
using System.Collections;
using System.Collections.Generic;
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
