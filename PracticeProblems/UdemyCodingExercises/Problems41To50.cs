using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UdemyCodingExercises.Problems11To20;

namespace UdemyCodingExercises
{
    internal class Problems41To50
    {
        // Problem #41: Get top N recent messages by timestamp
        // Implement the GetTopNRecentMessages method to take a collection of Message objects and an integer n.Assume all messages are ordered by Timestamp.
        // This method should skip all messages with a Timestamp older than today, and return the next n messages.
        public record Message(string Content, DateTime Timestamp);
        public static IEnumerable<Message> GetTopNRecentMessages(IEnumerable<Message> messages, int n)
        {
            if (messages is null)
            {
                throw new ArgumentNullException(
                    nameof(messages),
                    "Messages cannot be null.");
            }
            if (n < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(n),
                    "Number of messages to take must be non-negative.");
            }

            IEnumerable<Message> oldMessages = 
                messages
                .SkipWhile(message => message.Timestamp < DateTime.Today)
                .Take(n);

            return oldMessages;
        }
    }
}
