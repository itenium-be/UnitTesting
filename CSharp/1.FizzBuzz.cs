using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Itenium.Interview
{
    public class FizzBuzz
    {
        /// <summary>
        /// Loop from 1 to 100 (including 100) and do an output.WriteLine for each number, except
        /// When it is a multiple of 3 print Fizz
        /// When it is a multiple of 5 print Buzz
        /// When it is a multiple of 3 and 5 print FizzBuzz
        /// </summary>
        /// <example>
        /// Example output:
        /// 1, 2, Fizz, 4, Buzz, Fizz, 7, 8, Fizz, Buzz, 11, Fizz, 13, 14, FizzBuzz, 16
        /// </example>
        /// <remarks>
        /// This is a timed exercise: the faster you get to a working solution, the better.
        /// Performance, Maintainability, Duplication, Code Smells, ... it is all irrelevant.
        /// Only the amount of time needed to come to a complete solution matters.
        /// </remarks>
        public static void PrintFizzBuzz(ITestOutputHelper output)
        {
            // your code here
            output.WriteLine("1");
            output.WriteLine("2");
            output.WriteLine("Fizz");
        }
    }
}
