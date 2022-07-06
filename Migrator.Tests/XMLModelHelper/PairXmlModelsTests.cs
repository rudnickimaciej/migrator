using NUnit.Framework;
using System.Collections.Generic;


namespace Migrator.Tests.XMLModelHelper
{
    internal class PairXmlModelsTests
    {
        private TModel type1;
        private TModel type11;

        private TModel type2;
        private TModel type22;

        private TModel type3;
        private TModel type33;

        private TModel type4;
        private TModel type44;

        [SetUp]
        public void Setup()
        {
            type1 = new TModel() { EntityName = "entity1" };
            type11 = new TModel() { EntityName = "entity1" };

            type2 = new TModel() { EntityName = "entity2" };
            type22 = new TModel() { EntityName = "entity2" };

            type3 = new TModel() { EntityName = "entity3" };
            type33 = new TModel() { EntityName = "entity3" };

            type4 = new TModel() { EntityName = "entity4" };
            type44 = new TModel() { EntityName = "entity4" };

        }

        [Test]
        public void PairXmlModels_InitialMigration_TuplesShouldBeLeftHalfEmpty()
        {
            List<TModel> oldSchemas = new List<TModel>() { };
            List<TModel> newSchemas = new List<TModel>() { type1, type2, type3, type4 };

            List<TModelPair> pairs = Migrator.TModelHelper.PairSchemas(oldSchemas, newSchemas);

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
            List<TModel> oldSchemas = new List<TModel>() { type1, type2, type3, type4 };
            List<TModel> newSchemas = new List<TModel>() { };

            List<TModelPair> pairs = Migrator.TModelHelper.PairSchemas(oldSchemas, newSchemas);

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
            List<TModel> oldSchemas = new List<TModel>() { type1, type2, type3, type4 };
            List<TModel> newSchemas = new List<TModel>() { type22, type33, type11, type44 };

            List<TModelPair> pairs = Migrator.TModelHelper.PairSchemas(oldSchemas, newSchemas);

            Assert.AreEqual(4, pairs.Count);
            Assert.AreEqual(pairs[0].SchemaPair.Item1.EntityName, pairs[0].SchemaPair.Item2.EntityName);
            Assert.AreEqual(pairs[1].SchemaPair.Item1.EntityName, pairs[1].SchemaPair.Item2.EntityName);
            Assert.AreEqual(pairs[2].SchemaPair.Item1.EntityName, pairs[2].SchemaPair.Item2.EntityName);
            Assert.AreEqual(pairs[3].SchemaPair.Item1.EntityName, pairs[3].SchemaPair.Item2.EntityName);
        }

        [Test]
        public void PairXmlModels_TypeDeleted_TuplesShouldContainRightHalfEmptyTuple()
        {
            List<TModel> oldSchemas = new List<TModel>() { type1, type2, type3, type4 };
            List<TModel> newSchemas = new List<TModel>() { type22, type33, type44 };

            List<TModelPair> pairs = Migrator.TModelHelper.PairSchemas(oldSchemas, newSchemas);

            Assert.AreEqual(4, pairs.Count);

            Assert.AreEqual(pairs[0].SchemaPair.Item1.EntityName, pairs[0].SchemaPair.Item2.EntityName);
            Assert.AreEqual(pairs[1].SchemaPair.Item1.EntityName, pairs[1].SchemaPair.Item2.EntityName);
            Assert.AreEqual(pairs[2].SchemaPair.Item1.EntityName, pairs[2].SchemaPair.Item2.EntityName);
            Assert.IsNull(pairs[3].SchemaPair.Item2);
        }

        [Test]
        public void PairXmlModels_TypeAdded_TuplesShouldContainLeftHalfEmptyTuple()
        {
            List<TModel> oldSchemas = new List<TModel>() { type2, type3, type4 };
            List<TModel> newSchemas = new List<TModel>() { type22, type33, type44, type11 };

            List<TModelPair> pairs = Migrator.TModelHelper.PairSchemas(oldSchemas, newSchemas);

            Assert.AreEqual(4, pairs.Count);
            Assert.AreEqual(pairs[0].SchemaPair.Item1.EntityName, pairs[0].SchemaPair.Item2.EntityName);
            Assert.AreEqual(pairs[1].SchemaPair.Item1.EntityName, pairs[1].SchemaPair.Item2.EntityName);
            Assert.AreEqual(pairs[2].SchemaPair.Item1.EntityName, pairs[2].SchemaPair.Item2.EntityName);
            Assert.IsNull(pairs[3].SchemaPair.Item1);
        }
    }
}