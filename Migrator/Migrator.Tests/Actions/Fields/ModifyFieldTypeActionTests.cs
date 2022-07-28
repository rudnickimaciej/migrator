using Migrator;
using Migrator.Commons;
using Migrator.ISQLProviderNamespace;
using Migrator.SQLServerProviderNamespace;
using Migrator.SQLServerProviderNamespace.SQLActions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Migrator.Tests.Actions.Fields
{
    internal class ModifyFieldTypeActionTests
    {

        [Test]
        public void CreateAction_SimpleFieldModified_ReturnModifyFieldAction()
        {
            string tableName = "Person";

            TModel oldSchema = new TModel()
            {
                EntityName = tableName,
                Fields = new List<TFieldModel>()
                    {
                        new TFieldModel()
                        {
                            ID = 21,
                            EntityName = tableName,
                            Name = "Age",
                            Type = FieldType.SIMPLE,
                            NetType = "Int32",
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
                            ID = 21,
                            EntityName = tableName,
                            Name = "Age",
                            Type = FieldType.SIMPLE,
                            NetType = "Int64",
                            SqlType = SQLType.BIGINT,
                            Namespace = "SYSTEM"
                        }
                    }
            };

            IEnumerable<ISQLAction> actions = new SQLServerProvider().CreateActions(new TModelPair(oldSchema, newSchema));

            Assert.AreEqual(1, actions.Count());
            Assert.IsTrue(actions.ToList()[0].GetType().Equals(typeof(ModifyFieldTypeAction)));

            IEnumerable<IEnumerable<SQLOperation>> operations = actions.Select(a => a.GenerateOperations()).ToList();
            IEnumerable<SQLOperation> flattenOperations = OperationActionHelper.FlattenOperations(operations);
            Assert.AreEqual(1, flattenOperations.Count());
        }
    }
}