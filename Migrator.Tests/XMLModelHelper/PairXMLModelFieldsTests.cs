using NUnit.Framework;
using System.Collections.Generic;

namespace Migrator.Tests.XMLModelHelper
{
    internal class PairXMLModelFieldsTests
    {
        private XMLModel type1;
        private XMLModel type2;
        private XMLModel type3;
        private XMLModel type4;
        private string entity1Name = "entity1";

        [SetUp]
        public void Setup()
        {
            type1 = new XMLModel()
            {
                EntityName = entity1Name,
                Fields = new List<XMLModelField>()
                {
                    new XMLModelField()
                    {
                        EntityName =  entity1Name,
                        fieldName = "field1",
                        fieldType = FieldType.SIMPLE,
                        netType = "INT",
                        sqlType = SQLType.INT,
                        typeNamespace = "SYSTEM"
                    },
                     new XMLModelField()
                    {
                        EntityName =  entity1Name,
                        fieldName = "field2",
                        fieldType = FieldType.SIMPLE,
                        netType = "INT",
                        sqlType = SQLType.INT,
                        typeNamespace = "SYSTEM"
                    }
                }
            };
            type2 = new XMLModel() { EntityName = "entity2" };
            type3 = new XMLModel() { EntityName = "entity3" };
            type4 = new XMLModel() { EntityName = "entity4" };
        }

        [Test]
        public void PairXMLModelFields_NumberOfFieldsAndNamesAreExactlyTheSame_TuplesShouldBeFullyMatched()
        {
            XMLModel schema = new XMLModel()
            {
                EntityName = "entity1",
                Fields = new List<XMLModelField>()
                {
                    new XMLModelField()
                    {
                        EntityName = "entity1",
                        fieldName = "field1",
                        fieldType = FieldType.SIMPLE,
                        netType = "INT",
                        sqlType = SQLType.INT,
                        typeNamespace = "SYSTEM"
                    },
                    new XMLModelField()
                    {
                        EntityName = "entity1",
                        fieldName = "field2",
                        fieldType = FieldType.REFERENCE,
                        netType = "INT",
                        sqlType = SQLType.INT,
                        typeNamespace = "SYSTEM"
                    }
                }
            };

            List<XMLModelFieldPair> pairs = Migrator.XMLModelHelper.PairFields(new XMLModelPair(schema, schema));

            Assert.AreEqual(2, pairs.Count);
            Assert.AreEqual(pairs[0].FieldPair.Item1.fieldName, pairs[0].FieldPair.Item1.fieldName);
            Assert.AreEqual(pairs[1].FieldPair.Item1.fieldName, pairs[1].FieldPair.Item1.fieldName);
        }

        [Test]
        public void PairXMLModelFields_FieldAddedToSchema_ThereShouldBeRightHalfEmptyTuple()
        {
            XMLModel oldSchema = new XMLModel()
            {
                EntityName = "entity1",
                Fields = new List<XMLModelField>()
                {
                    new XMLModelField()
                    {
                        EntityName = "entity1",
                        fieldName = "field1",
                        fieldType = FieldType.SIMPLE,
                        netType = "INT",
                        sqlType = SQLType.INT,
                        typeNamespace = "SYSTEM"
                    },
                    new XMLModelField()
                    {
                        EntityName = "entity1",
                        fieldName = "field2",
                        fieldType = FieldType.REFERENCE,
                        netType = "INT",
                        sqlType = SQLType.INT,
                        typeNamespace = "SYSTEM"
                    }
                }
            };

            XMLModel newSchema = new XMLModel()
            {
                EntityName = "entity1",
                Fields = new List<XMLModelField>()
                    {
                        new XMLModelField()
                        {
                            EntityName = "entity1",
                            fieldName = "field1",
                            fieldType = FieldType.SIMPLE,
                            netType = "INT",
                            sqlType = SQLType.INT,
                            typeNamespace = "SYSTEM"
                        },
                        new XMLModelField()
                        {
                            EntityName = "entity1",
                            fieldName = "field2",
                            fieldType = FieldType.REFERENCE,
                            netType = "INT",
                            sqlType = SQLType.INT,
                            typeNamespace = "SYSTEM"
                        },
                        new XMLModelField()
                        {
                            EntityName = "entity1",
                            fieldName = "field3",
                            fieldType = FieldType.SIMPLE,
                            netType = "STRING",
                            sqlType = SQLType.VARCHAR,
                            typeNamespace = "SYSTEM"
                        }
                    }
            };

            List<XMLModelFieldPair> pairs = Migrator.XMLModelHelper.PairFields(new XMLModelPair(oldSchema, newSchema));

            //Assert.AreEqual(4, pairs.Count);
            //Assert.IsNull(pairs[0].SchemaPair.Item2);
            //Assert.IsNull(pairs[1].SchemaPair.Item2);
            //Assert.IsNull(pairs[2].SchemaPair.Item2);
            //Assert.IsNull(pairs[3].SchemaPair.Item2);

            //Assert.IsNotNull(pairs[0].SchemaPair.Item1);
            //Assert.IsNotNull(pairs[1].SchemaPair.Item1);
            //Assert.IsNotNull(pairs[2].SchemaPair.Item1);
            //Assert.IsNotNull(pairs[3].SchemaPair.Item1);
        }

        [Test]
        public void PairXmlModels_NoTypeAddedNorDeleted_TuplesShouldBeFull()
        {
            List<XMLModel> oldSchemas = new List<XMLModel>() { type1, type2, type3, type4 };
            List<XMLModel> newSchemas = new List<XMLModel>() { type2, type3, type1, type4 };

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
            List<XMLModel> newSchemas = new List<XMLModel>() { type2, type3, type4 };

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
            List<XMLModel> newSchemas = new List<XMLModel>() { type2, type3, type4, type1 };

            List<XMLModelPair> pairs = Migrator.XMLModelHelper.PairSchemas(oldSchemas, newSchemas);

            Assert.AreEqual(4, pairs.Count);
            Assert.AreEqual(pairs[0].SchemaPair.Item1.EntityName, pairs[0].SchemaPair.Item2.EntityName);
            Assert.AreEqual(pairs[1].SchemaPair.Item1.EntityName, pairs[1].SchemaPair.Item2.EntityName);
            Assert.AreEqual(pairs[2].SchemaPair.Item1.EntityName, pairs[2].SchemaPair.Item2.EntityName);
            Assert.IsNull(pairs[3].SchemaPair.Item1);
        }
    }
}