using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Migrator.Tests.SQLOperationFabric
{
    internal class SQLOperationFabricTests
    {
        [Test]
        public void CreateActions_FieldTypeNotChanged_ReturnEmptyActionList()
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
                        }
                    }
            };

            IEnumerable<ISQLAction> actions = Migrator.SQLOperationFabric.Create(new XMLModelPair(oldSchema, newSchema));

            Assert.AreEqual(0, actions.Count());
        }
        [Test]
        public void CreateActions_FieldRemoved_ReturnDeleteFieldAction()
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
                        }
                    }
            };

            IEnumerable<ISQLAction> actions = Migrator.SQLOperationFabric.Create(new XMLModelPair(oldSchema, newSchema));

            Assert.AreEqual(1, actions.Count());
            Assert.IsTrue(actions.ToList()[0].GetType().Equals(typeof(DeleteFieldAction)));
        }

        [Test]
        public void CreateActions_FieldAdded_ReturnAddFieldAction()
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

            IEnumerable<ISQLAction> actions = Migrator.SQLOperationFabric.Create(new XMLModelPair(oldSchema, newSchema));

            Assert.AreEqual(1, actions.Count());
            Assert.IsTrue(actions.ToList()[0].GetType().Equals(typeof(AddFieldAction)));
        }
        [Test]
        public void CreateActions_FieldTypeChanged_ReturnModifyFieldTypeAction()
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
                            Type = FieldType.SIMPLE,
                            NetType = "STRING",
                            SqlType = SQLType.INT,
                            Namespace = "SYSTEM"
                        }
                    }
            };

            IEnumerable<ISQLAction> actions = Migrator.SQLOperationFabric.Create(new XMLModelPair(oldSchema, newSchema));
            
            Assert.AreEqual(1, actions.Count());
            Assert.IsTrue(actions.ToList()[0].GetType().Equals(typeof(ModifyFieldTypeAction)));
        }


    
    }
}
