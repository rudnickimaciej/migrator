
using Migrator.Commons;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Migrator.Tests
{
    internal class E
    {

    }
    internal class E2
    {

    }
    internal class MigratorTests
    {
     

        [SetUp]
        public void Setup()
        {
            _sqlProviderMock = new Mock<ISqlProvider>();
        }
        [Test]
        public void ProcessTypes_AddNewType()
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



            _sqlProviderMock.Setup(p => p.GetSchemasFromDb()).Returns(new List<XmlDoc>() { });
            OperationActionHelper.

            new Migrator.TypeMigrator(_sqlProviderMock.Object).Migrate(new List<Type>() { typeof(Animal) });

        }
    }
}