using NUnit.Framework;
using NUnit.Mocks;
using System.Collections.Generic;
using Migrator.Core;
using System;

namespace TestProject1.Core
{
    class Home
    {
        public Person Person { get; set; }
    }
    class Person
    {
        public Animal Animal { get; set; }

    }
    class Animal
    {

    }
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
           
        }

        [Test]
        [TestCase(typeof(Person), typeof(Animal), typeof(Home))]
        public void Sort(Type t1, Type t2, Type t3)
        {
            List<Node> list = new List<Node>()
            {
                new Node(t1),
                new Node(t2),
                new Node(t3)
            };
            List<Node> sortedList = new List<Node>();

           Migratorm.SortEntities(0, list, ref sortedList);

            Assert.AreEqual(typeof(Animal), sortedList[0].Type);
            Assert.AreEqual(typeof(Person), sortedList[0].Type);
            Assert.AreEqual(typeof(Home), sortedList[0].Type);

        }

        [Test]
        [TestCase(typeof(Person), typeof(Animal), typeof(Home))]
        public void Migrate(Type t1, Type t2, Type t3)
        {
          


        }
    }
}