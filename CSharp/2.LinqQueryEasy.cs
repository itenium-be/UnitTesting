using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Itenium.Interview
{
    public class LinqEasyQuery
    {
        /// <summary>
        /// Calculate the average age of the parents and their children.
        /// But only take children > 8 years into account
        /// </summary>
        public static double GetAverageAgeOfPersonsAndChildrenOlderThan8(IEnumerable<Person> parents)
        {
            // Your implementation here
            return 0;
        }
    }

    public class Person : IPerson
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public ICollection<Child> Children { get; }

        public Person(int age, params Child[] children)
        {
            Id = LinqEasyQueryTests.Rnd.Next(500);
            Age = age;
            Children = new List<Child>(children);
        }

        public override string ToString() => $"{Id}={Age}";
    }

    public class Child : IPerson
    {
        public int Id { get; set; }
        public int Age { get; set; }

        public Child(int age)
        {
            Id = LinqEasyQueryTests.Rnd.Next(500);
            Age = age;
        }

        public override string ToString() => $"{Id}={Age}";
    }

    public interface IPerson
    {
        int Id { get; set; }
        int Age { get; set; }
    }


    public class LinqEasyQueryTests
    {
        [Fact]
        public void AverageAge_xxx()
        {

        }
    }
}
