using NUnit.Framework;
using System.Collections.Generic;


namespace Migrator.Tests.XMLModelHelper
{
    internal class PairXmlModelsTests
    {
        private XMLModel type1;
        private XMLModel type11;

        private XMLModel type2;
        private XMLModel type22;

        private XMLModel type3;
        private XMLModel type33;

        private XMLModel type4;
        private XMLModel type44;

        [SetUp]
        public void Setup()
        {
            type1 = new XMLModel() { EntityName = "entity1" };
            type11 = new XMLModel() { EntityName = "entity1" };

            type2 = new XMLModel() { EntityName = "entity2" };
            type22 = new XMLModel() { EntityName = "entity2" };

            type3 = new XMLModel() { EntityName = "entity3" };
            type33 = new XMLModel() { EntityName = "entity3" };

            type4 = new XMLModel() { EntityName = "entity4" };
            type44 = new XMLModel() { EntityName = "entity4" };

        }

        [Test]
        public void PairXmlModels_InitialMigration_TuplesShouldBeLeftHalfEmpty()
        {
            List<XMLModel> oldSchemas = new List<XMLModel>() { };
            List<XMLModel> newSchemas = new List<XMLModel>() { type1, type2, type3, type4 };

            List<XMLModelPair> pairs = Migrator.XMLModelHelper.PairSchemas(oldSchemas, newSchemas);

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

            List<XMLModelPair> pairs = Migrator.XMLModelHelper.PairSchemas(oldSchemas, newSchemas);

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
            List<XMLModel> newSchemas = new List<XMLModel>() { type22, type33, type11, type44 };

            List<XMLModelPair> pairs = Migrator.XMLModelHelper.PairSchemas(oldSchemas, newSchemas);

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
            List<XMLModel> newSchemas = new List<XMLModel>() { type22, type33, type44 };

            List<XMLModelPair> pairs = Migrator.XMLModelHelper.PairSchemas(oldSchemas, newSchemas);

            Assert.AreEqual(4, pairs.Count);

            Assert.AreEqual(pairs[0].SchemaPair.Item1.EntityName, pairs[0].SchemaPair.Item2.EntityName);
            Assert.AreEqual(pairs[1].SchemaPair.Item1.EntityName, pairs[1].SchemaPair.Item2.EntityName);
            Assert.AreEqual(pairs[2].SchemaPair.Item1.EntityName, pairs[2].SchemaPair.Item2.EntityName);
            Assert.IsNull(pairs[3].SchemaPair.Item2);
        }

        [Test]
        public void PairXmlModels_TypeAdded_TuplesShouldContainLeftHalfEmptyTuple()
        {
            List<XMLModel> oldSchemas = new List<XMLModel>() { type2, type3, type4 };
            List<XMLModel> newSchemas = new List<XMLModel>() { type22, type33, type44, type11 };

            List<XMLModelPair> pairs = Migrator.XMLModelHelper.PairSchemas(oldSchemas, newSchemas);

            Assert.AreEqual(4, pairs.Count);
            Assert.AreEqual(pairs[0].SchemaPair.Item1.EntityName, pairs[0].SchemaPair.Item2.EntityName);
            Assert.AreEqual(pairs[1].SchemaPair.Item1.EntityName, pairs[1].SchemaPair.Item2.EntityName);
            Assert.AreEqual(pairs[2].SchemaPair.Item1.EntityName, pairs[2].SchemaPair.Item2.EntityName);
            Assert.IsNull(pairs[3].SchemaPair.Item1);
        }
    }
}