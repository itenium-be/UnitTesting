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
        internal static readonly Random Rnd = new Random();

        [Fact]
        public void AverageAge_WithoutChildren()
        {
            var mother1 = new Person(30);
            var mother2 = new Person(40);

            var averageAge = LinqEasyQuery.GetAverageAgeOfPersonsAndChildrenOlderThan8(new [] {mother1, mother2});

            Assert.Equal(35, averageAge);
        }

        [Fact]
        public void AverageAge_WithChildren()
        {
            var mother1 = new Person(30, new Child(10), new Child(10));
            var mother2 = new Person(40, new Child(4), new Child(14));

            var averageAge = LinqEasyQuery.GetAverageAgeOfPersonsAndChildrenOlderThan8(new[] { mother1, mother2 });

            Assert.Equal(20.8, averageAge);
        }
    }
}