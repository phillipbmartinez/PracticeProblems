using static UdemyCodingExercises.Problems31To40;

namespace UdemyCodingExercises
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var messages = new List<Message>
            {
                new Message("Hello", new DateTime(2025, 12, 1, 14, 30, 0)),
                new Message("Hi", new DateTime(2025, 12, 1, 15, 0, 0)),
                new Message("Hey", new DateTime(2025, 12, 1, 14, 0, 0))
            };

            IEnumerable<string> sortedMessages = Problems31To40.GetRecentMessages(messages);
        }
    }
}
