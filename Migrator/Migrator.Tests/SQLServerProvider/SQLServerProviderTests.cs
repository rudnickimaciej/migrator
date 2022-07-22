using Migrator.Commons;
using Migrator.ISQLProviderNamespace;
using Migrator.SQLServerProviderNamespace;
using Migrator.SQLServerProviderNamespace.SQLActions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Migrator.Tests.SQLServerProviderTests
{
    internal class SQLServerProviderTests
    {
        [Test]
        public void CreateActions_FieldTypeNotChanged_ReturnEmptyActionList()
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
                        ID = 12,
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
                        }
                    }
            };

            IEnumerable<ISQLAction> actions = new SQLServerProvider().Create(new TModelPair(oldSchema, newSchema));

            Assert.AreEqual(0, actions.Count());
        }

        [Test]
        public void CreateActions_FieldRemoved_ReturnDeleteFieldAction()
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
                        ID = 12,
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
                        }
                    }
            };

            IEnumerable<ISQLAction> actions = new SQLServerProvider().Create(new TModelPair(oldSchema, newSchema));

            Assert.AreEqual(1, actions.Count());
            Assert.IsTrue(actions.ToList()[0].GetType().Equals(typeof(DeleteFieldAction)));
        }

        [Test]
        public void CreateActions_FieldAdded_ReturnAddFieldAction()
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
                            ID = 12,
                            EntityName = "entity1",
                            Name = "field2",
                            Type = FieldType.REFERENCE,
                            NetType = "INT",
                            SqlType = SQLType.INT,
                            Namespace = "SYSTEM"
                        }
                    }
            };

            IEnumerable<ISQLAction> actions = new SQLServerProvider().Create(new TModelPair(oldSchema, newSchema));

            Assert.AreEqual(1, actions.Count());
            Assert.IsTrue(actions.ToList()[0].GetType().Equals(typeof(AddFieldAction)));
        }

        [Test]
        public void CreateActions_FieldTypeChanged_ReturnModifyFieldTypeAction()
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
                        ID = 12,
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
                            Type = FieldType.SIMPLE,
                            NetType = "STRING",
                            SqlType = SQLType.VARCHAR,
                            Namespace = "SYSTEM"
                        }
                    }
            };

            IEnumerable<ISQLAction> actions = new SQLServerProvider().Create(new TModelPair(oldSchema, newSchema));

            Assert.AreEqual(1, actions.Count());
            Assert.IsTrue(actions.ToList()[0].GetType().Equals(typeof(ModifyFieldTypeAction)));
        }

        [Test]
        public void CreateActions_TypeAdded_ReturnCreateTableAction()
        {
            TModel oldSchema = null;

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
                            Type = FieldType.SIMPLE,
                            NetType = "STRING",
                            SqlType = SQLType.VARCHAR,
                            Namespace = "SYSTEM"
                        }
                    }
            };

            IEnumerable<ISQLAction> actions = new SQLServerProvider().Create(new TModelPair(oldSchema, newSchema));

            Assert.AreEqual(1, actions.Count());
            Assert.IsTrue(actions.ToList()[0].GetType().Equals(typeof(AddTableAction)));
        }

        [Test]
        public void CreateActions_TypeRemoved_ReturnDeleteTableAction()
        {
            TModel oldSchema = new TModel()
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
                            Type = FieldType.SIMPLE,
                            NetType = "STRING",
                            SqlType = SQLType.VARCHAR,
                            Namespace = "SYSTEM"
                        }
                    }
            };

            TModel newSchema = null;

            IEnumerable<ISQLAction> actions = new SQLServerProvider().Create(new TModelPair(oldSchema, newSchema));

            Assert.AreEqual(1, actions.Count());
            Assert.IsTrue(actions.ToList()[0].GetType().Equals(typeof(DeleteTableAction)));
        }
    }
}