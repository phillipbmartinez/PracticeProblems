using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.Mail;
using System.Numerics;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using UdemyCodingExercises;
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
        internal static int SumInRange(int start, int end, params int[] numbers)
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
        internal static (List<int> ints, List<string> strings, int unknownCount) SeparateObjects(List<object> objects)
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
        internal class Exercise
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
        internal interface IFileSystem
        {
            string ReadFile(string fileName);
        }

        // For problem #11
        internal interface ILogger
        {
            void Log(string message);
        }


        // #16: Format text with custom styles
        // A messaging system needs to process text inputs into different styles for display.The base TextFormatter class provides a default formatting method, while the derived classes ShoutFormatter and WhisperFormatter customize the output.
        // In the base class, the Format method simply trims the message and adds the "Message:" prefix.
        // Implement the Format method in each derived class:
        // ShoutFormatter trims it, converts it to uppercase and adds "!!!" at the end.For example, for input "   Hello  " the result will be "SHOUT: HELLO!!!".
        // WhisperFormatter trims it, converts it to lowercase, and wraps it in parentheses.For example, for input "   Hello  " the result will be "whisper: (hello)".
        internal class TextFormatter
        {
            internal virtual string Format(string message)
            {
                string processed = message.Trim();
                return $"Message: {processed}";
            }
        }

        // For problem #16
        internal class ShoutFormatter : TextFormatter
        {
            internal override string Format(string message)
            {
                string processed = message.Trim().ToUpper();
                return $"SHOUT: {processed}!!!";
            }
        }

        // For problem #16
        internal class WhisperFormatter : TextFormatter
        {
            internal override string Format(string message)
            {
                string processed = message.Trim().ToLower();
                return $"whisper: ({processed})";
            }
        }


        // #17: Read a string until the end marker
        // Implement the ReadUntilEndMarker method that takes a string input which may contain the marker "_END_".
        // The method should return the portion of the string that appears before the _END_ marker.
        // If the marker is found, return all characters before it.If the marker is not found, return the entire input string unchanged.
        // For example, for input "Hello there_END_ my fellow programmer" the result should be "Hello there".
        internal static string ReadUntilEndMarker(string input)
        {
            if (input.Contains("_END_"))
            {
                string inputBeforeEnd = input.Split("_END_")[0];
                return inputBeforeEnd;
            }
            else
            {
                return input;
            }
        }


        // #19: Count halvings until value drops below 1
        // Implement the CountHalvings method that takes a positive double and returns the number of times it can be divided by 2 before it becomes less than 1.
        // The method should always run the division at least once, even if the initial value is already less than 1. In this case, the method's result should be 1.
        // If the input is less than zero, throw ArgumentException.
        internal static int CountHalvings(double value)
        {
            int count = 0;

            if (value < 0)
            {
                throw new ArgumentException("Input value is below 0");
            }

            do
            {
                value = value / 2;
                count++;
            }
            while (value >= 1);

            return count;
        }


        // #20: Send notifications via multiple channels
        // A communication system needs to send messages to recipients through different channels, such as email and SMS.The Exercise class provides a SendToAll method that takes a list of notification objects, a recipient, and a message, and sends the message through each channel.
        // Complete the missing code to ensure the program compiles and the SendToAll method functions correctly with both EmailNotification and SmsNotification.To do this, you’ll need to:
        // Define an INotification interface with a method that matches the Send method signature.
        // Update the EmailNotification and SmsNotification classes to implement this interface, using their existing SendEmail and SendSms methods to handle the notification logic.
        public static void SendToAll(List<INotification> notifications, string recipient, string message)
        {
            foreach (var notification in notifications)
            {
                notification.Send(recipient, message);
            }
        }
    }


    // For problem #20
    public interface INotification
    {
        void Send(string recipient, string message);
    }


    // For problem #20
    public class EmailNotification : INotification
    {
        public void SendEmail(string emailAddress, string message)
        {
            Console.WriteLine($"[Email] To: {emailAddress} — Message: {message}");
        }

        public void Send(string emailAddress, string message)
        {
            Console.WriteLine($"[Email] To: {emailAddress} — Message: {message}");
        }

        // From solution
        // public void Send(string emailAddress, string message)
        // {
        //     SendEmail(emailAddress, message);
        // }
    }


    // For problem #20
    public class SmsNotification : INotification
    {
        public void SendSms(string phoneNumber, string message)
        {
            Console.WriteLine($"[SMS] To: {phoneNumber} — Message: {message}");
        }

        public void Send(string phoneNumber, string message)
        {
            Console.WriteLine($"[SMS] To: {phoneNumber} — Message: {message}");
        }

        // From solution
        // public void Send(string phoneNumber, string message)
        // {
        //     SendSms(phoneNumber, message);
        // }
    }


    // #18: Extend DayOfWeek to detect weekends
    // Implement the IsWeekend extension method for the built-in DayOfWeek enum.
    // This method should return true if the day is Saturday or Sunday, and false otherwise.
    public static class DayOfWeekExtensions
    {
        public static bool IsWeekend(this DayOfWeek day)
        {
            if (day == DayOfWeek.Saturday || day == DayOfWeek.Sunday)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}