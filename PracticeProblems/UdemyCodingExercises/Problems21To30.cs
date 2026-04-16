using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
    }
}
