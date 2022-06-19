using NUnit.Framework;
using NUnit.Mocks;
using System.Collections.Generic;
using Migrator;
using System;

namespace Migrator.Tests.Migrate
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
        #region packages
        private SQLPackage personPackage;
        private SQLScript personScript1;
        private SQLScript personScript2;

        private SQLPackage animalPackage;
        private SQLScript animalScript1;

        private SQLPackage homePackage;
        private SQLScript homeScript1;
        private SQLScript homeScript2;


        #endregion
        [SetUp]
        public void Setup()
        {
            personScript1 = new SQLScript()
            {
                SqlScriptType = SqlScriptType.CREATE_TABLE,
                Sql = "CREATE TABLE Person(...)"
            };
            personScript2 = new SQLScript()
            {
                SqlScriptType = SqlScriptType.ADD_FOREING_KEY,
                Sql = "ALTER TABLE Person ADD COLUMN Animal FOREIGN KEY REFERENCES ANIMAL(ID)"
            };

            personPackage = new SQLPackage(personScript1, personScript2);


            animalScript1 = new SQLScript()
            {
                SqlScriptType = SqlScriptType.CREATE_TABLE,
                Sql = "CREATE TABLE Animal(...)"
            };

            animalPackage = new SQLPackage(animalScript1);

            homeScript1 = new SQLScript()
            {
                SqlScriptType = SqlScriptType.CREATE_TABLE,
                Sql = "CREATE TABLE Home(...)"
            };
            homeScript2 = new SQLScript()
            {
                SqlScriptType = SqlScriptType.ADD_FOREING_KEY,
                Sql = "ALTER TABLE Home ADD COLUMN Person FOREIGN KEY REFERENCES Person(ID)"
            };

            homePackage = new SQLPackage(homeScript1, homeScript2);

        }

        [Test]
        public void PairXmlModels_InitialMigration_TuplesShouldBeHalfEmpty()
        {

        }

        [Test]
        public void Test1()
        {
            List<SQLScript> flat = Migratorm.FlattenPackages(new List<SQLPackage> { personPackage, animalPackage, homePackage });
            List<SQLScript> sorted = Migratorm.SortByType(flat);

            string full = Migratorm.ToSQL(sorted);

            Assert.AreEqual(5, sorted.Count);
            Assert.AreEqual(personScript1, sorted[0]);
            Assert.AreEqual(animalScript1, sorted[1]);
            Assert.AreEqual(homeScript1, sorted[2]);
            Assert.AreEqual(personScript2, sorted[3]);
            Assert.AreEqual(homeScript2, sorted[4]);




        }
    }
}