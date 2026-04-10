using System;
using System.Collections.Generic;
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
        }
    }
}
