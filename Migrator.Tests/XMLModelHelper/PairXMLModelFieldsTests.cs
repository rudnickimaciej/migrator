using NUnit.Framework;
using System.Collections.Generic;

namespace Migrator.Tests.XMLModelHelper
{
    internal class PairXMLModelFieldsTests
    {
        private string entity1Name = "entity1";
        private XMLModel type1;
        private XMLModel type2;
        private XMLModel type3;
        private XMLModel type4;

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
            Assert.AreEqual(pairs[0].FieldPair.Item1.fieldName, pairs[0].FieldPair.Item2.fieldName);
            Assert.AreEqual(pairs[1].FieldPair.Item1.fieldName, pairs[1].FieldPair.Item2.fieldName);
        }


        [Test]
        public void PairXMLModelFields_FieldAddedToSchema_TuplesShouldContaintLeftHalfEmptyTuple()
        {
            XMLModel oldSchema = new XMLModel()
            {
                EntityName = "entity1",
                Fields = new List<XMLModelField>()
                {
                    new XMLModelField()
                    {
                        ID = 11,
                        EntityName = "entity1",
                        fieldName = "field1",
                        fieldType = FieldType.SIMPLE,
                        netType = "INT",
                        sqlType = SQLType.INT,
                        typeNamespace = "SYSTEM"
                    },
                    new XMLModelField()
                    {
                        ID = 21,
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
                            ID = 21,
                            EntityName = "entity1",
                            fieldName = "field1",
                            fieldType = FieldType.SIMPLE,
                            netType = "INT",
                            sqlType = SQLType.INT,
                            typeNamespace = "SYSTEM"
                        },
                        new XMLModelField()
                        {
                            ID = 22,
                            EntityName = "entity1",
                            fieldName = "field2",
                            fieldType = FieldType.REFERENCE,
                            netType = "INT",
                            sqlType = SQLType.INT,
                            typeNamespace = "SYSTEM"
                        },
                        new XMLModelField()
                        {
                            ID = 32,
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

            Assert.AreEqual(3, pairs.Count);
            Assert.AreEqual(pairs[0].FieldPair.Item1.fieldName, pairs[0].FieldPair.Item2.fieldName);
            Assert.AreEqual(pairs[1].FieldPair.Item1.fieldName, pairs[1].FieldPair.Item2.fieldName);
            Assert.IsNull(pairs[2].FieldPair.Item1);
            Assert.IsNotNull(pairs[2].FieldPair.Item2);
        }

        [Test]
        public void PairXMLModelFields_FieldRemovedFromSchema_TuplesShouldContaintRightHalfEmptyTuple()
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
                    }
            };

            List<XMLModelFieldPair> pairs = Migrator.XMLModelHelper.PairFields(new XMLModelPair(oldSchema, newSchema));

            Assert.AreEqual(3, pairs.Count);
            Assert.AreEqual(pairs[0].FieldPair.Item1.fieldName, pairs[0].FieldPair.Item2.fieldName);
            Assert.AreEqual(pairs[1].FieldPair.Item1.fieldName, pairs[1].FieldPair.Item2.fieldName);
            Assert.IsNotNull(pairs[2].FieldPair.Item1);
            Assert.IsNull(pairs[2].FieldPair.Item2);
        }     
    }
}