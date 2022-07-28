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
    internal class DeleteFieldActionTests
    {
        internal class Pet
        { }

        internal class PersonOld
        {
            public int Age { get; set; }
            public string Name { get; set; }
        }

        internal class PersonNew
        {
            public int Age { get; set; }
        }

        [Test]
        public void CreateAction_SimpleFieldRemoved_ReturnDeleteFieldAction()
        {

            TModel oldModel = TModelConverter.ConvertTypeToTypeModel(typeof(PersonOld));
            TModel newModel = TModelConverter.ConvertTypeToTypeModel(typeof(PersonNew));

           

            IEnumerable<ISQLAction> actions = new SQLServerProvider().CreateActions(new TModelPair(oldModel, newModel));

            Assert.AreEqual(1, actions.Count());
            Assert.IsTrue(actions.ToList()[0].GetType().Equals(typeof(DeleteFieldAction)));

            IEnumerable<IEnumerable<SQLOperation>> operations = actions.Select(a => a.GenerateOperations()).ToList();
            IEnumerable<SQLOperation> flattenOperations = OperationActionHelper.FlattenOperations(operations);
            Assert.AreEqual(1, flattenOperations.Count());
        }  
    }
}