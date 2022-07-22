using Migrator.Commons;
using NUnit.Framework;
using System.Collections.Generic;

namespace Migrator.Tests.XMLModelHelper
{
    internal class PairTModelFieldsTests
    {
        private string entity1Name = "entity1";
        private TModel type1;
        private TModel type2;
        private TModel type3;
        private TModel type4;

        [Test]
        public void PairXMLModelFields_NumberOfFieldsAndNamesAreExactlyTheSame_TuplesShouldBeFullyMatched()
        {
            TModel schema = new TModel()
            {
                EntityName = "entity1",
                Fields = new List<TFieldModel>()
                {
                    new TFieldModel()
                    {
                        EntityName = "entity1",
                        Name = "field1",
                        Type = FieldType.SIMPLE,
                        NetType = "INT",
                        SqlType = SQLType.INT,
                        Namespace = "SYSTEM"
                    },
                    new TFieldModel()
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

            List<TFieldModelPair> pairs = TModelHelper.PairFields(new TModelPair(schema, schema));

            Assert.AreEqual(2, pairs.Count);
            Assert.AreEqual(pairs[0].FieldPair.Item1.Name, pairs[0].FieldPair.Item2.Name);
            Assert.AreEqual(pairs[1].FieldPair.Item1.Name, pairs[1].FieldPair.Item2.Name);
        }


        [Test]
        public void PairXMLModelFields_FieldAddedToSchema_TuplesShouldContaintLeftHalfEmptyTuple()
        {
            TModel oldSchema = new TModel()
            {
                EntityName = "entity1",
                Fields = new List<TFieldModel>()
                {
                    new TFieldModel()
                    {
                        ID = 11,
                        EntityName = "entity1",
                        Name = "field1",
                        Type = FieldType.SIMPLE,
                        NetType = "INT",
                        SqlType = SQLType.INT,
                        Namespace = "SYSTEM"
                    },
                    new TFieldModel()
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

            TModel newSchema = new TModel()
            {
                EntityName = "entity1",
                Fields = new List<TFieldModel>()
                    {
                        new TFieldModel()
                        {
                            ID = 21,
                            EntityName = "entity1",
                            Name = "field1",
                            Type = FieldType.SIMPLE,
                            NetType = "INT",
                            SqlType = SQLType.INT,
                            Namespace = "SYSTEM"
                        },
                        new TFieldModel()
                        {
                            ID = 22,
                            EntityName = "entity1",
                            Name = "field2",
                            Type = FieldType.REFERENCE,
                            NetType = "INT",
                            SqlType = SQLType.INT,
                            Namespace = "SYSTEM"
                        },
                        new TFieldModel()
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

            List<TFieldModelPair> pairs = TModelHelper.PairFields(new TModelPair(oldSchema, newSchema));

            Assert.AreEqual(3, pairs.Count);
            Assert.AreEqual(pairs[0].FieldPair.Item1.Name, pairs[0].FieldPair.Item2.Name);
            Assert.AreEqual(pairs[1].FieldPair.Item1.Name, pairs[1].FieldPair.Item2.Name);
            Assert.IsNull(pairs[2].FieldPair.Item1);
            Assert.IsNotNull(pairs[2].FieldPair.Item2);
        }

        [Test]
        public void PairXMLModelFields_FieldRemovedFromSchema_TuplesShouldContaintRightHalfEmptyTuple()
        {
            TModel oldSchema = new TModel()
            {
                EntityName = "entity1",
                Fields = new List<TFieldModel>()
                {
                    new TFieldModel()
                    {
                        EntityName = "entity1",
                        Name = "field1",
                        Type = FieldType.SIMPLE,
                        NetType = "INT",
                        SqlType = SQLType.INT,
                        Namespace = "SYSTEM"
                    },
                    new TFieldModel()
                    {
                        EntityName = "entity1",
                        Name = "field2",
                        Type = FieldType.REFERENCE,
                        NetType = "INT",
                        SqlType = SQLType.INT,
                        Namespace = "SYSTEM"
                    },
                    new TFieldModel()
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

            TModel newSchema = new TModel()
            {
                EntityName = "entity1",
                Fields = new List<TFieldModel>()
                    {
                        new TFieldModel()
                        {
                            EntityName = "entity1",
                            Name = "field1",
                            Type = FieldType.SIMPLE,
                            NetType = "INT",
                            SqlType = SQLType.INT,
                            Namespace = "SYSTEM"
                        },
                        new TFieldModel()
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

            List<TFieldModelPair> pairs = TModelHelper.PairFields(new TModelPair(oldSchema, newSchema));

            Assert.AreEqual(3, pairs.Count);
            Assert.AreEqual(pairs[0].FieldPair.Item1.Name, pairs[0].FieldPair.Item2.Name);
            Assert.AreEqual(pairs[1].FieldPair.Item1.Name, pairs[1].FieldPair.Item2.Name);
            Assert.IsNotNull(pairs[2].FieldPair.Item1);
            Assert.IsNull(pairs[2].FieldPair.Item2);
        }     
    }
}