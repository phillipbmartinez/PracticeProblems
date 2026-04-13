using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using static System.Net.Mime.MediaTypeNames;
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


        // #5: Calculate total break time from multiple time ranges
        // A company tracks employee breaks using start and end timestamps.Each break is represented as a pair of DateTime values—when the break started and when it ended.
        // Implement the CalculateTotalBreakTime method that receives a list of tuples, each containing a start and end DateTime, and returns the total break time as a TimeSpan.
        // Assume that each start is before its corresponding end.
        internal static TimeSpan CalculateTotalBreakTime(List<(DateTime Start, DateTime End)> breaks)
        {
            TimeSpan totalBreakTime = new TimeSpan(0);

            foreach ((DateTime Start, DateTime End) employeeBreak in breaks)
            {
                totalBreakTime += employeeBreak.End - employeeBreak.Start;
            }

            return totalBreakTime;
        }


        // #6: Track the highest score for a player
        // Implement the UpdateHighestScore method that updates the highest score recorded in a player's statistics.
        // Implement the UpdateHighestScore method that updates the highest score recorded in a player's statistics.
        // The player parameter may be null. Its Statistics property may also be null , and so can be its HighestScore property.
        // If the player or their statistics are null, the method should do nothing (it should not update any data or throw an exception).
        // If HighestScore is null, set it to the provided newScore.
        // If the existing HighestScore is lower than newScore, update it—so that it always stores the highest value.
        internal static void UpdateHighestScore(Player? player, int newScore)
        {
            if (player == null || player.Statistics == null)
            {
                return;
            }

            if (player.Statistics.HighestScore == null)
            {
                player.Statistics.HighestScore = newScore;
            }

            if (player.Statistics.HighestScore < newScore)
            {
                player.Statistics.HighestScore = newScore;
            }
        }


        // #7: Calculate shipping cost based on order total and customer status
        // Implement the CalculateShippingCost method that calculates the shipping cost based on the total value of an order and whether the customer is a premium member.
        // The method takes two parameters:
        // orderTotal – the total amount of the order(decimal)
        // isPremiumCustomer – a boolean flag indicating if the customer is a premium member
        // The method should return the shipping cost according to these rules:
        // If the order total is less than 50 and the customer is not a premium member, return 10
        // If the order total is less than 50 and the customer is a premium member, return 5
        // If the order total is 50 or more and the customer is not a premium member, return 5
        // If the order total is 50 or more and the customer is a premium member, return 0
        // You can use plain old if-else statements, but consider using a switch expression with when clauses to make the code shorter and cleaner.
        internal static decimal CalculateShippingCost(decimal orderTotal, bool isPremiumCustomer)
        {
            decimal result = 0;

            switch (orderTotal)
            {
                case < 50 when isPremiumCustomer == false:
                    result = 10;
                    break;
                case < 50 when isPremiumCustomer == true:
                    result = 5;
                    break;
                case >= 50 when isPremiumCustomer == false:
                    result = 5;
                    break;
                case >= 50 when isPremiumCustomer == true:
                    result = 0;
                    break;
                default:
                    break;
            }

            return result;

            // Course solution
            // return orderTotal switch
            // {
            //     < 50 when !isPremiumCustomer => 10,
            //     < 50 when isPremiumCustomer => 5,
            //     >= 50 when !isPremiumCustomer => 5,
            //     >= 50 when !isPremiumCustomer => 0
            // };
        }


        // #8: Try parsing a date and extracting its components
        // Implement the TryExtractDateComponents method that takes a string representing a date and attempts to extract its components: year, month, and day.
        // If the input can be successfully parsed into a DateTime, the method should:
        // Set the out parameters for year, month, and day
        // Return true
        // If the input cannot be parsed, the method should:
        // Set all out values to zero
        // Return false
        // To ensure your code works consistently regardless of the system’s culture, use a parsing approach that is culture-invariant.Consider using DateTime.TryParse.
        internal static bool TryExtractDateComponents(string dateInput, out int year, out int month, out int day)
        {
            if (DateTime.TryParse(dateInput, out DateTime dateTimeParseResult))
            {
                year = dateTimeParseResult.Year;
                month = dateTimeParseResult.Month;
                day = dateTimeParseResult.Day;

                return true;
            }
            else
            {
                year = 0;
                month = 0;
                day = 0;

                return false;
            }
        }


        // #9: Check if a string is a palindrome
        // Implement the IsPalindrome method that checks whether a given string is a palindrome.A palindrome reads the same forward and backward.For example, the words "madam" or "radar" are palindromes, but "hello" is not.
        // The comparison should be case-sensitive and should include all characters (spaces, punctuation, etc.).
        // Return true if the string is a palindrome; otherwise, return false.
        internal static bool IsPalindrome(string input)
        {
            // radar == radar    hello != olleh
            // 01234   43210     01234    43210
            for (int i = 0; i < input.Length / 2; i++)
            {
                if (input[i] != input[input.Length - 1 - i])
                {
                    return false;
                }

                // Other option
                // if (input[i] != input[^(i + 1)])
                // {
                    // return false;
                // }
            }

            return true;

            // LINQ solution
            // return input.SequenceEqual(input.Reverse());

        }
    }

    // For problem #6
    internal class Player
    {
        public Statistics? Statistics { get; set; }
    }

    // For problem #6
    internal class Statistics
    {
        public int? HighestScore { get; set; }
    }

    // #10: Implement a weekly schedule with multiple custom indexers
    // Implement the WeekSchedule class that allows storing and retrieving plans for days of the week using two custom indexers.
    // The first indexer accepts a DayOfWeek enum value.
    // The second indexer accepts a string and attempts to parse it into a DayOfWeek.If parsing fails, it should throw an ArgumentException.
    // Both should use the underlying _plans Dictionary.
    // The string-based indexer should work regardless of casing (e.g. "monday" or "MONDAY" are both valid).
    // If no note is stored for a given day, the indexer should return null.
    internal class WeekSchedule
    {
        private readonly Dictionary<DayOfWeek, string?> _plans = new Dictionary<DayOfWeek, string?>();

        internal string? this[DayOfWeek dayOfWeek]
        {
            get => _plans.ContainsKey(dayOfWeek) ? _plans[dayOfWeek] : null;
            set => _plans[dayOfWeek] = value;
        }

        internal string? this[string dayName]
        {
            get => this[ParseDayString(dayName)];
            set => this[ParseDayString(dayName)] = value;
        }

        internal DayOfWeek ParseDayString(string dayString)
        {
            if (!Enum.TryParse<DayOfWeek>(dayString, ignoreCase: true, out var result))
            {
                throw new ArgumentException($"\"{dayString}\" is not a valid day name.");
            }

            return result;
        }
    }
}
