using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UdemyCodingExercises
{
    internal class FirstTenProblems
    {
        // #1: Check if a collection contains any negative numbers
        // Exercise description: Implement the ContainsNegative method that checks whether a sequence of integers contains at least one negative number.
        internal static bool ContainsNegative(IEnumerable<int> numbers)
        {
            bool result = false;

            foreach (int i in numbers)
            {
                if (i < 0)
                {
                    result = true;
                }
            }

            return result;

            // other solution: return numbers.Any(number => number < 0);
        }


        // #2: Split a full name into first and last names
        // Implement the SplitFullName method that takes a string representing a person's full name and returns a tuple with two values: first name and last name.
        // Assume the full name contains exactly one space, separating the first and last name.
        internal static (string firstName, string lastname) SplitFullName(string fullName)
        {
            string firstName = fullName.Split(" ")[0];
            string lastName = fullName.Split(" ")[1];

            return (firstName, lastName);
        }


        // #3: Count character frequencies in a string
        // Implement the CountCharacterFrequencies method that takes a string and returns a dictionary mapping each character to the number of times it appears in the input.
        // The method should be case-sensitive, so 'H' and 'h' are treated as different characters. All characters, including spaces and symbols, should be counted.
        internal static Dictionary<char, int> CountCharacterFrequencies(string input)
        {
            Dictionary<char, int> charFreq = new Dictionary<char, int>();

            foreach (char c in input)
            {
                if (charFreq.ContainsKey(c))
                {
                    charFreq[c]++;
                }
                else
                {
                    charFreq.Add(c, 1);
                }
            }

            return charFreq;

            // Solution using LINQ
            //return input
            //    .GroupBy(c => c)
            //    .ToDictionary(group => group.Key, group => group.Count());
        }
    }
}
