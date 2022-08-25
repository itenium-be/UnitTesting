using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Itenium.Interview
{
    public class LinqQuery
    {
        /// <summary>
        /// Calculate the 2 most common letters in the input string
        /// </summary>
        public static IEnumerable<Result> Get2MostCommonCharacters(string input)
        {
            // Hint: .GroupBy() returns a:
            // public interface IGrouping<out TKey, out TElement> : IEnumerable<TElement>, IEnumerable
            // {
            //    TKey Key { get; }
            // }

            return Enumerable.Empty<Result>();
        }
    }

    public class Result
    {
        public char Character { get; }
        public int TimesOccurring { get; }

        public Result(char character, int amount)
        {
            Character = character;
            TimesOccurring = amount;
        }

        public override string ToString() => $"{Character} x{TimesOccurring}";
    }

    public class LinqQueryTests
    {
        [Fact]
        public void LinqQuery_HappyPath()
        {
            string exampleString = "aabbbccccdddddd";

            var result = LinqQuery.Get2MostCommonCharacters(exampleString).ToArray();

            // Assert
        }
    }
}
