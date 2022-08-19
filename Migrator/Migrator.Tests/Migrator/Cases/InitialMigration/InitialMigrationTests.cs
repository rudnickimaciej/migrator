using Migrator.Commons;
using Migrator.ISQLProviderNamespace;
using Migrator.SQLServerProviderNamespace;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrator.Tests.Migrator.Cases.InitialMigration
{
  
    internal class InitialMigrationTests
    {
        internal class Sex
        {
            public string Value { get; set; }
        }

        internal class Pet
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public DateTime Born { get; set; }
            public Sex Sex { get; set; }

        }

        internal class Person
        {
            #region simple fields
            public int Age { get; set; }
            public string Name { get; set; }
            public DateTime Date { get; set; }
            public bool Flag { get; set; }
            public Int16 Int16 { get; set; }
            public Int64 Int64 { get; set; }
            public Double DoubleField { get; set; }
            public Decimal DecimalField { get; set; }


            #endregion simple fields

            #region simple reference fields
            public Sex Sex { get; set; }
            public Sex Sex2 { get; set; }
            #endregion simple reference fields

            #region simple list fields

            public List<int> Scores { get; set; }

            public List<string> Strings { get; set; }


            #endregion simple list fields

            #region reference list fields

            public List<Pet> Pets { get; set; }
            public List<Pet> Pets2 { get; set; }
            #endregion reference list fields

            #region enums

            #endregion enums
        }

        ISQLProvider _sqlProvider;

        [SetUp]
        public void Setup()
        {
            var _sqlProviderMock = new Mock<SQLServerProvider>();
            _sqlProvider = _sqlProviderMock.Object; 
        }
        [Test]
        public void InitialMigration_CreateTables()
        {
            TModel personNewModel = TModelConverter.ConvertTypeToTypeModel(typeof(Person));
            TModel petNewModel = TModelConverter.ConvertTypeToTypeModel(typeof(Pet));
            TModel sexNewModel = TModelConverter.ConvertTypeToTypeModel(typeof(Sex));

            var newModels = new List<TModel>() { personNewModel, petNewModel, sexNewModel };
            var oldModels = new List<TModel>();

            

           var pairs =  TModelHelper.PairSchemas(oldModels, newModels);
           var actions =  pairs.Select(p => _sqlProvider.CreateActions(p));
           var flattenActions =  OperationActionHelper.FlattenActions(actions);
           var flattenOpperations= OperationActionHelper.FlattenOperations(flattenActions.Select(a => a.GenerateOperations()));
           var filteredOpeartions = OperationActionHelper.RemoveDuplicates(flattenOpperations);
           var sortedOperations = OperationActionHelper.SortByType(filteredOpeartions);
           string sql = OperationActionHelper.Merge(sortedOperations);
        }
    }
}
