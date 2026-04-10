using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UdemyCodingExercises
{
    internal class FirstTenProblems
    {
        // #1: Check if a collection contains any negative numbers (⭐)
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


        // #2: Split a full name into first and last names (⭐)
        // Implement the SplitFullName method that takes a string representing a person's full name and returns a tuple with two values: first name and last name.
        // Assume the full name contains exactly one space, separating the first and last name.
        internal static (string firstName, string lastname) SplitFullName(string fullName)
        {
            string firstName = fullName.Split(" ")[0];
            string lastName = fullName.Split(" ")[1];

            return (firstName, lastName);
        }
    }
}
