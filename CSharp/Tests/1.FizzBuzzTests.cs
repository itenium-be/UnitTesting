using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Itenium.Interview
{
    public class FizzBuzzTests
    {
        [Fact]
        public void PrintFizzBuzz_HappyPath()
        {
            // How to write tests for this method?
            // - When it is possible to change the implementation of FizzBuzz.PrintFizzBuzz
            // - And when it is not possible
            FizzBuzz.PrintFizzBuzz(_output);

            // Assert.Equal(1, ???);
        }

        private readonly ITestOutputHelper _output;

        public FizzBuzzTests(ITestOutputHelper output)
        {
            _output = output;
        }
    }
}
