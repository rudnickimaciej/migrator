using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Migrator;

namespace Migrator.Tests.Migrate
{
    internal class PairXmlModelsTests2
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
        public void PairXmlModels_InitialMigration_TuplesShouldBeLeftHalfEmpty()
        {
            List<XMLModel> oldSchemas = new List<XMLModel>() {};
            List<XMLModel> newSchemas = new List<XMLModel>() { type1, type2, type3, type4 };

           List<XmlModelPair> pairs =  Migratorm.PairSchemas5(oldSchemas, newSchemas);

            Assert.AreEqual(4, pairs.Count);
            Assert.IsNull(pairs[0].SchemaPair.Item1);
            Assert.IsNull(pairs[1].SchemaPair.Item1);
            Assert.IsNull(pairs[2].SchemaPair.Item1);
            Assert.IsNull(pairs[3].SchemaPair.Item1);

            Assert.IsNotNull(pairs[0].SchemaPair.Item2);
            Assert.IsNotNull(pairs[1].SchemaPair.Item2);
            Assert.IsNotNull(pairs[2].SchemaPair.Item2);
            Assert.IsNotNull(pairs[3].SchemaPair.Item2);
        }

        [Test]
        public void PairXmlModels_RemoveAllTypes_TuplesShouldBeRightHalfEmpty()
        {
            List<XMLModel> oldSchemas = new List<XMLModel>() { type1, type2, type3, type4 };
            List<XMLModel> newSchemas = new List<XMLModel>() { };

            List<XmlModelPair> pairs = Migratorm.PairSchemas5(oldSchemas, newSchemas);

            Assert.AreEqual(4, pairs.Count);
            Assert.IsNull(pairs[0].SchemaPair.Item2);
            Assert.IsNull(pairs[1].SchemaPair.Item2);
            Assert.IsNull(pairs[2].SchemaPair.Item2);
            Assert.IsNull(pairs[3].SchemaPair.Item2);

            Assert.IsNotNull(pairs[0].SchemaPair.Item1);
            Assert.IsNotNull(pairs[1].SchemaPair.Item1);
            Assert.IsNotNull(pairs[2].SchemaPair.Item1);
            Assert.IsNotNull(pairs[3].SchemaPair.Item1);
        }

        [Test]
        public void PairXmlModels_NoTypeAddedNorDeleted_TuplesShouldBeFull()
        {
            List<XMLModel> oldSchemas = new List<XMLModel>() { type1, type2, type3, type4 };
            List<XMLModel> newSchemas = new List<XMLModel>() { type2, type3, type1, type4 };

            List<XmlModelPair> pairs = Migratorm.PairSchemas5(oldSchemas, newSchemas);

            Assert.AreEqual(4, pairs.Count);
            Assert.AreEqual(pairs[0].SchemaPair.Item1.EntityName, pairs[0].SchemaPair.Item2.EntityName);
            Assert.AreEqual(pairs[1].SchemaPair.Item1.EntityName, pairs[1].SchemaPair.Item2.EntityName);
            Assert.AreEqual(pairs[2].SchemaPair.Item1.EntityName, pairs[2].SchemaPair.Item2.EntityName);
            Assert.AreEqual(pairs[3].SchemaPair.Item1.EntityName, pairs[3].SchemaPair.Item2.EntityName);
        }

        [Test]
        public void PairXmlModels_TypeDeleted_TuplesShouldContainRightHalfEmptyTuple()
        {
            List<XMLModel> oldSchemas = new List<XMLModel>() { type1, type2, type3, type4 };
            List<XMLModel> newSchemas = new List<XMLModel>() { type2, type3, type4 };

            List<XmlModelPair> pairs = Migratorm.PairSchemas5(oldSchemas, newSchemas);

            Assert.AreEqual(4, pairs.Count);
            Assert.AreEqual(pairs[0].SchemaPair.Item1.EntityName, pairs[0].SchemaPair.Item2.EntityName);
            Assert.AreEqual(pairs[1].SchemaPair.Item1.EntityName, pairs[1].SchemaPair.Item2.EntityName);
            Assert.AreEqual(pairs[2].SchemaPair.Item1.EntityName, pairs[2].SchemaPair.Item2.EntityName);
            Assert.IsNull  (pairs[3].SchemaPair.Item2);

        }

        [Test]
        public void PairXmlModels_TypeAdded_TuplesShouldContainLeftHalfEmptyTuple()
        {
            List<XMLModel> oldSchemas = new List<XMLModel>() { type2, type3, type4 };
            List<XMLModel> newSchemas = new List<XMLModel>() { type2, type3, type4, type1 };

            List<XmlModelPair> pairs = Migratorm.PairSchemas5(oldSchemas, newSchemas);

            Assert.AreEqual(4, pairs.Count);
            Assert.AreEqual(pairs[0].SchemaPair.Item1.EntityName, pairs[0].SchemaPair.Item2.EntityName);
            Assert.AreEqual(pairs[1].SchemaPair.Item1.EntityName, pairs[1].SchemaPair.Item2.EntityName);
            Assert.AreEqual(pairs[2].SchemaPair.Item1.EntityName, pairs[2].SchemaPair.Item2.EntityName);
            Assert.IsNull(pairs[3].SchemaPair.Item1);

        }
    }
}
