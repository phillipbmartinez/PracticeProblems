using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
    }
}
