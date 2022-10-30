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

namespace Migrator.Tests.Actions.Tables
{
    internal class DropTableActionTests
    {

        [Test]
        public void CreateAction_TypeAdded_ReturnDropTableAction()
        {
            TModel newSchema = null;

            TModel oldSchema = new TModel()
            {
                EntityName = "entity1",
                Fields = new List<TFieldModel>()
                    {
                        new TFieldModel()
                        {
                            ID = Guid.NewGuid(),
                            EntityName = "entity1",
                            Name = "field1",
                            Type = FieldType.SIMPLE,
                            NetType = "INT",
                            SqlType = SQLType.INT,
                            Namespace = "SYSTEM"
                        },
                        new TFieldModel()
                        {
                            ID = Guid.NewGuid(),
                            EntityName = "entity1",
                            Name = "field2",
                            Type = FieldType.SIMPLE,
                            NetType = "STRING",
                            SqlType = SQLType.VARCHAR,
                            Namespace = "SYSTEM"
                        }
                    }
            };

            IEnumerable<ISQLAction> actions = new SQLServerProvider(new TSqlLogger()).CreateActions(new TModelPair(oldSchema, newSchema));

            Assert.AreEqual(1, actions.Count());
            Assert.IsTrue(actions.ToList()[0].GetType().Equals(typeof(DeleteTableAction)));

            IEnumerable<IEnumerable<SQLOperation>> operations = actions.Select(a => a.GenerateOperations()).ToList();
            IEnumerable<SQLOperation> flattenOperations = OperationActionHelper.FlattenOperations(operations);
            Assert.AreEqual(1, flattenOperations.Count());
        }
    }
}