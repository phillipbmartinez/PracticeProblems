using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Numerics;
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


        // #4: Filter and select qualified players from a game leaderboard
        // Implement the GetQualifiedPlayers method that takes a dictionary of player names and their scores.
        // Your goal is to build a list of players who qualify for the next round based on the following rules:
        // A player must have a score of at least 50 to qualify.
        // If a player has a score of 100 or more, the process should stop immediately—this may indicate suspected cheating or a system rule violation.However, any players who qualified before this point should still be included in the result.
        // Return the list of names of players who qualified before any disqualifying score was encountered.
        internal static List<string> GetQualifiedPlayers(Dictionary<string, int> playerScores)
        {
            List<string> qualifiedPlayers = new List<string>();

            foreach (KeyValuePair<string, int> player in playerScores)
            {
                if (player.Value >= 100)
                {
                    break;
                }
                else if (player.Value >= 50)
                {
                    qualifiedPlayers.Add(player.Key);
                }
                else
                {
                    continue;
                }
            }

            // LINQ solution
            // return playerScores
            //     .TakeWhile(score => score.Value < 100)
            //     .Where(score => score.Value >= 50)
            //     .Select(score => score.Key)
            //     .ToList();

            return qualifiedPlayers;
        }
    }
}
