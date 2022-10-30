using Migrator;
using Migrator.Commons;
using Migrator.Commons.Logger;
using Migrator.ISQLProviderNamespace;
using Migrator.SQLServerProviderNamespace;
using Migrator.SQLServerProviderNamespace.SQLActions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Migrator.Tests.Actions.Fields
{
    internal class AddFieldActionTests
    {

        [Test]
        public void CreateAction_SimpleFieldAdded_ReturnAddFieldAction()
        {
            string tableName = "Person";

            TModel oldSchema = new TModel()
            {
                EntityName = tableName,
                Fields = new List<TFieldModel>()
                    {
                        new TFieldModel()
                        {
                            ID = Guid.NewGuid(),
                            EntityName = tableName,
                            Name = "Age",
                            Type = FieldType.SIMPLE,
                            NetType = "INT",
                            SqlType = SQLType.INT,
                            Namespace = "SYSTEM"
                        }
                    }
            }; ;

            TModel newSchema = new TModel()
            {
                EntityName = tableName,
                Fields = new List<TFieldModel>()
                    {
                        new TFieldModel()
                        {
                            ID = Guid.NewGuid(),
                            EntityName = tableName,
                            Name = "Age",
                            Type = FieldType.SIMPLE,
                            NetType = "INT",
                            SqlType = SQLType.INT,
                            Namespace = "SYSTEM"
                        },
                        new TFieldModel()
                        {
                            ID = Guid.NewGuid(),
                            EntityName =tableName,
                            Name = "LastName",
                            Type = FieldType.SIMPLE,
                            NetType = "STRING",
                            SqlType = SQLType.VARCHAR,
                            Namespace = "SYSTEM"
                        }
                    }
            };

            IEnumerable<ISQLAction> actions = new SQLServerProvider(new TSqlLogger()).CreateActions(new TModelPair(oldSchema, newSchema));

            Assert.AreEqual(1, actions.Count());
            Assert.IsTrue(actions.ToList()[0].GetType().Equals(typeof(AddFieldAction)));

            IEnumerable<IEnumerable<SQLOperation>> operations = actions.Select(a => a.GenerateOperations()).ToList();
            IEnumerable<SQLOperation> flattenOperations = OperationActionHelper.FlattenOperations(operations);
            Assert.AreEqual(1, flattenOperations.Count());
        }
        [Test]
        public void CreateAction_ReferenceFieldAdded_ReturnAddFieldAction()
        {
            string tableName = "Person";

            TModel oldSchema = new TModel()
            {
                EntityName = tableName,
                Fields = new List<TFieldModel>()
                    {
                        new TFieldModel()
                        {
                            ID = Guid.NewGuid(),
                            EntityName = tableName,
                            Name = "Age",
                            Type = FieldType.SIMPLE,
                            NetType = "INT",
                            SqlType = SQLType.INT,
                            Namespace = "SYSTEM"
                        }
                    }
            }; ;

            TModel newSchema = new TModel()
            {
                EntityName = tableName,
                Fields = new List<TFieldModel>()
                    {
                        new TFieldModel()
                        {
                            ID = Guid.NewGuid(),
                            EntityName = tableName,
                            Name = "Age",
                            Type = FieldType.SIMPLE,
                            NetType = "INT",
                            SqlType = SQLType.INT,
                            Namespace = "SYSTEM"
                        },
                        new TFieldModel()
                        {
                            ID = Guid.NewGuid(),
                            EntityName =tableName,
                            Name = "Sex",
                            Type = FieldType.REFERENCE,
                            NetType = "Sex",
                            SqlType = SQLType.INT,
                            Namespace = "SYSTEM"
                        }
                    }
            };

            IEnumerable<ISQLAction> actions = new SQLServerProvider(new TSqlLogger()).CreateActions(new TModelPair(oldSchema, newSchema));

            Assert.AreEqual(1, actions.Count());
            Assert.IsTrue(actions.ToList()[0].GetType().Equals(typeof(AddFieldAction)));

            IEnumerable<IEnumerable<SQLOperation>> operations = actions.Select(a => a.GenerateOperations()).ToList();
            IEnumerable<SQLOperation> flattenOperations = OperationActionHelper.FlattenOperations(operations);
            Assert.AreEqual(2, flattenOperations.Count());
        }

        [Test]
        public void CreateAction_SimpleListFieldAdded_ReturnAddFieldAction()
        {
            string tableName = "Person";

            TModel oldSchema = new TModel()
            {
                EntityName = tableName,
                Fields = new List<TFieldModel>()
                    {
                        new TFieldModel()
                        {
                            ID = Guid.NewGuid(),
                            EntityName = tableName,
                            Name = "Age",
                            Type = FieldType.SIMPLE,
                            NetType = "INT",
                            SqlType = SQLType.INT,
                            Namespace = "SYSTEM"
                        }
                    }
            }; ;

            TModel newSchema = new TModel()
            {
                EntityName = tableName,
                Fields = new List<TFieldModel>()
                    {
                        new TFieldModel()
                        {
                            ID = Guid.NewGuid(),
                            EntityName = tableName,
                            Name = "Age",
                            Type = FieldType.SIMPLE,
                            NetType = "INT",
                            SqlType = SQLType.INT,
                            Namespace = "SYSTEM"
                        },
                        new TFieldModel()
                        {
                            ID = Guid.NewGuid(),
                            EntityName =tableName,
                            Name = "CarIds",
                            Type = FieldType.SIMPLE_LIST,
                            NetType = "System.Int32",
                            SqlType = SQLType.INT,
                            Namespace = "SYSTEM"
                        }
                    }
            };

            IEnumerable<ISQLAction> actions = new SQLServerProvider(new TSqlLogger()).CreateActions(new TModelPair(oldSchema, newSchema));

            Assert.AreEqual(1, actions.Count());
            Assert.IsTrue(actions.ToList()[0].GetType().Equals(typeof(AddFieldAction)));

            IEnumerable<IEnumerable<SQLOperation>> operations = actions.Select(a => a.GenerateOperations()).ToList();
            IEnumerable<SQLOperation> flattenOperations = OperationActionHelper.FlattenOperations(operations);
            Assert.AreEqual(5, flattenOperations.Count());
        }
    }
}