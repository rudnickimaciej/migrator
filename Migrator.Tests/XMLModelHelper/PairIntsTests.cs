using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Migrator;

namespace Migrator.Tests.XMLModelHelper
{
    internal class PairInts
    {
        private XMLModel type1;
        private XMLModel type2;
        private XMLModel type3;
        private XMLModel type4;


        [SetUp]
        public void Setup()
        {
            type1 = new XMLModel() { EntityName = "entity1" };
            type2 = new XMLModel() { EntityName = "entity2" };
            type3 = new XMLModel() { EntityName = "entity3" };
            type4 = new XMLModel() { EntityName = "entity4" };
        }

        [Test]
        public void PairXmlModels_EmptyList_OutputSHoudBeEmpty()
        {
            List<int> oldSchemas = new List<int>() { };
            List<int> newSchemas = new List<int>() { };

            List<Tuple<int?, int?>> pairs = TypeMigrator.PairInts(oldSchemas, newSchemas);

            Assert.AreEqual(0, pairs.Count);
        }
        [Test]
        public void PairXmlModels_InitialMigration_TuplesShouldBeLeftHalfEmpty()
        {
            List<int> oldSchemas = new List<int>() {};
            List<int> newSchemas = new List<int>() { 1,2,3,4 };

           List<Tuple<int?, int?>> pairs = TypeMigrator.PairInts(oldSchemas, newSchemas);

            Assert.AreEqual(4, pairs.Count);
            Assert.IsNull(pairs[0].Item1);
            Assert.IsNull(pairs[1].Item1);
            Assert.IsNull(pairs[2].Item1);
            Assert.IsNull(pairs[3].Item1);

            Assert.IsNotNull(pairs[0].Item2);
            Assert.IsNotNull(pairs[1].Item2);
            Assert.IsNotNull(pairs[2].Item2);
            Assert.IsNotNull(pairs[3].Item2);
        }

        [Test]
        public void PairXmlModels_RemoveAllTypes_TuplesShouldBeRightHalfEmpty()
        {
            List<int> oldSchemas = new List<int>() { 1,2,3,4 };
            List<int> newSchemas = new List<int>() { };

            List<Tuple<int?, int?>> pairs = TypeMigrator.PairInts(oldSchemas, newSchemas);

            Assert.AreEqual(4, pairs.Count);
            Assert.IsNull(pairs[0].Item2);
            Assert.IsNull(pairs[1].Item2);
            Assert.IsNull(pairs[2].Item2);
            Assert.IsNull(pairs[3].Item2);

            Assert.IsNotNull(pairs[0].Item1);
            Assert.IsNotNull(pairs[1].Item1);
            Assert.IsNotNull(pairs[2].Item1);
            Assert.IsNotNull(pairs[3].Item1);
        }

        [Test]
        public void PairXmlModels_NoTypeAddedNorDeleted_TuplesShouldBeFull()
        {
            List<int> oldSchemas = new List<int>() { 1, 2, 3, 4 };
            List<int> newSchemas = new List<int>() { 3,4,2,1};

            List<Tuple<int?, int?>> pairs = TypeMigrator.PairInts(oldSchemas, newSchemas);

            Assert.AreEqual(4, pairs.Count);
            Assert.AreEqual(pairs[0].Item1, pairs[0].Item2);
            Assert.AreEqual(pairs[1].Item1, pairs[1].Item2);
            Assert.AreEqual(pairs[2].Item1, pairs[2].Item2);
            Assert.AreEqual(pairs[3].Item1, pairs[3].Item2);
        }

        [Test]
        public void PairXmlModels_TypeDeleted_TuplesShouldContainRightHalfEmptyTuple()
        {
            List<int> oldSchemas = new List<int>() { 1, 2, 3, 4 };
            List<int> newSchemas = new List<int>() { 2, 3, 4 };

            List<Tuple<int?, int?>> pairs = TypeMigrator.PairInts(oldSchemas, newSchemas);

            Assert.AreEqual(4, pairs.Count);
            Assert.AreEqual(pairs[0].Item1, pairs[0].Item2);
            Assert.AreEqual(pairs[1].Item1, pairs[1].Item2);
            Assert.AreEqual(pairs[2].Item1, pairs[2].Item2);
            Assert.IsNull(pairs[3].Item2);

        }

        [Test]
        public void PairXmlModels_TypeAdded_TuplesShouldContainLeftHalfEmptyTuple()
        {
            List<int> oldSchemas = new List<int>() { 1, 3, 4 };
            List<int> newSchemas = new List<int>() { 2, 3, 4, 1 };

            List<Tuple<int?, int?>> pairs = TypeMigrator.PairInts(oldSchemas, newSchemas);

            Assert.AreEqual(4, pairs.Count);
            Assert.AreEqual(pairs[0].Item1, pairs[0].Item2);
            Assert.AreEqual(pairs[1].Item1, pairs[1].Item2);
            Assert.AreEqual(pairs[2].Item1, pairs[2].Item2);
            Assert.IsNull(pairs[3].Item1);

        }
    }
}
