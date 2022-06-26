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
                        Name = "field1",
                        Type = FieldType.SIMPLE,
                        NetType = "INT",
                        SqlType = SQLType.INT,
                        Namespace = "SYSTEM"
                    },
                    new XMLModelField()
                    {
                        EntityName = "entity1",
                        Name = "field2",
                        Type = FieldType.REFERENCE,
                        NetType = "INT",
                        SqlType = SQLType.INT,
                        Namespace = "SYSTEM"
                    }
                }
            };

            List<XMLModelFieldPair> pairs = Migrator.XMLModelHelper.PairFields(new XMLModelPair(schema, schema));

            Assert.AreEqual(2, pairs.Count);
            Assert.AreEqual(pairs[0].FieldPair.Item1.Name, pairs[0].FieldPair.Item2.Name);
            Assert.AreEqual(pairs[1].FieldPair.Item1.Name, pairs[1].FieldPair.Item2.Name);
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
                        Name = "field1",
                        Type = FieldType.SIMPLE,
                        NetType = "INT",
                        SqlType = SQLType.INT,
                        Namespace = "SYSTEM"
                    },
                    new XMLModelField()
                    {
                        ID = 21,
                        EntityName = "entity1",
                        Name = "field2",
                        Type = FieldType.REFERENCE,
                        NetType = "INT",
                        SqlType = SQLType.INT,
                        Namespace = "SYSTEM"
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
                            Name = "field1",
                            Type = FieldType.SIMPLE,
                            NetType = "INT",
                            SqlType = SQLType.INT,
                            Namespace = "SYSTEM"
                        },
                        new XMLModelField()
                        {
                            ID = 22,
                            EntityName = "entity1",
                            Name = "field2",
                            Type = FieldType.REFERENCE,
                            NetType = "INT",
                            SqlType = SQLType.INT,
                            Namespace = "SYSTEM"
                        },
                        new XMLModelField()
                        {
                            ID = 32,
                            EntityName = "entity1",
                            Name = "field3",
                            Type = FieldType.SIMPLE,
                            NetType = "STRING",
                            SqlType = SQLType.VARCHAR,
                            Namespace = "SYSTEM"
                        }
                    }
            };

            List<XMLModelFieldPair> pairs = Migrator.XMLModelHelper.PairFields(new XMLModelPair(oldSchema, newSchema));

            Assert.AreEqual(3, pairs.Count);
            Assert.AreEqual(pairs[0].FieldPair.Item1.Name, pairs[0].FieldPair.Item2.Name);
            Assert.AreEqual(pairs[1].FieldPair.Item1.Name, pairs[1].FieldPair.Item2.Name);
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
                        Name = "field1",
                        Type = FieldType.SIMPLE,
                        NetType = "INT",
                        SqlType = SQLType.INT,
                        Namespace = "SYSTEM"
                    },
                    new XMLModelField()
                    {
                        EntityName = "entity1",
                        Name = "field2",
                        Type = FieldType.REFERENCE,
                        NetType = "INT",
                        SqlType = SQLType.INT,
                        Namespace = "SYSTEM"
                    },
                    new XMLModelField()
                    {
                       EntityName = "entity1",
                       Name = "field3",
                       Type = FieldType.SIMPLE,
                       NetType = "STRING",
                       SqlType = SQLType.VARCHAR,
                       Namespace = "SYSTEM"
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
                            Name = "field1",
                            Type = FieldType.SIMPLE,
                            NetType = "INT",
                            SqlType = SQLType.INT,
                            Namespace = "SYSTEM"
                        },
                        new XMLModelField()
                        {
                            EntityName = "entity1",
                            Name = "field2",
                            Type = FieldType.REFERENCE,
                            NetType = "INT",
                            SqlType = SQLType.INT,
                            Namespace = "SYSTEM"
                        },
                    }
            };

            List<XMLModelFieldPair> pairs = Migrator.XMLModelHelper.PairFields(new XMLModelPair(oldSchema, newSchema));

            Assert.AreEqual(3, pairs.Count);
            Assert.AreEqual(pairs[0].FieldPair.Item1.Name, pairs[0].FieldPair.Item2.Name);
            Assert.AreEqual(pairs[1].FieldPair.Item1.Name, pairs[1].FieldPair.Item2.Name);
            Assert.IsNotNull(pairs[2].FieldPair.Item1);
            Assert.IsNull(pairs[2].FieldPair.Item2);
        }     
    }
}