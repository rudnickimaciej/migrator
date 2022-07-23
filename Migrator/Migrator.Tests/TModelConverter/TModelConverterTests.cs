using Migrator.Commons;
using Migrator.Tests.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Migrator.Tests.Domain
{
    internal class Sex
    { }

    internal class Pet
    { }

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

        #endregion simple reference fields

        #region simple list fields

        public List<int> Scores { get; set; }

        public List<string> Strings { get; set; }


        #endregion simple list fields

        #region reference list fields

        public List<Pet> Pets { get; set; }
        #endregion reference list fields


    }
}

namespace Migrator.Tests.TModelConverterNamespace
{
    internal class TModelConverterTests
    {
       
        [TestCase(0, "Person","Age", "System","Int32",SQLType.INT, FieldType.SIMPLE)]
        [TestCase(1, "Person", "Name", "System", "String", SQLType.VARCHAR, FieldType.SIMPLE)]
        [TestCase(2, "Person", "Date", "System", "DateTime", SQLType.DATETIME, FieldType.SIMPLE)]
        [TestCase(3, "Person", "Flag", "System", "Boolean", SQLType.BIT, FieldType.SIMPLE)]
        [TestCase(4, "Person", "Int16", "System", "Int16", SQLType.SMALLINT, FieldType.SIMPLE)]
        [TestCase(5, "Person", "Int64", "System", "Int64", SQLType.BIGINT, FieldType.SIMPLE)]
        [TestCase(6, "Person", "DoubleField", "System", "Double", SQLType.FLOAT, FieldType.SIMPLE)]
        [TestCase(7, "Person", "DecimalField", "System", "Decimal", SQLType.DECIMAL, FieldType.SIMPLE)]
        [TestCase(8, "Person", "Sex", "Migrator.Tests.Domain", "Sex", SQLType.INT, FieldType.REFERENCE)]
        [TestCase(9, "Person", "Scores", "System", "Int32", SQLType.INT, FieldType.SIMPLE_LIST)]
        [TestCase(10, "Person", "Strings", "System", "String", SQLType.VARCHAR, FieldType.SIMPLE_LIST)]
        [TestCase(11, "Person", "Pets", "Migrator.Tests.Domain", "Pet", SQLType.INT, FieldType.REFERENCE_LIST)]


        public void ConvertType2(int index, string entityName, string fieldName, string nameSpace,string netType, SQLType sqlType, FieldType fieldType )
        {
            TModel model = TModelConverter.ConvertTypeToTypeModel(typeof(Person));

            Assert.AreEqual(model.Fields[index].Name, fieldName);
            Assert.AreEqual(model.Fields[index].Namespace, nameSpace);
            Assert.AreEqual(model.Fields[index].EntityName, entityName);
            Assert.AreEqual(model.Fields[index].NetType, netType);
            Assert.AreEqual(model.Fields[index].SqlType, sqlType);
            Assert.AreEqual(model.Fields[index].Type, fieldType);
        }
            [Test]
        public void ConvertType()
        {
            TModel model = TModelConverter.ConvertTypeToTypeModel(typeof(Person));
            Assert.AreEqual(model.EntityName, "Person");
            Assert.AreEqual(model.Fields.Count(), 6);

            Assert.AreEqual(model.Fields[0].Name, "Age");
            Assert.AreEqual(model.Fields[0].Namespace, "System");
            Assert.AreEqual(model.Fields[0].EntityName, "Person");
            Assert.AreEqual(model.Fields[0].NetType, "Int32");
            Assert.AreEqual(model.Fields[0].SqlType,  SQLType.INT);
            Assert.AreEqual(model.Fields[0].Type, FieldType.SIMPLE);

            Assert.AreEqual(model.Fields[1].Name, "Name");
            Assert.AreEqual(model.Fields[1].Namespace, "System");
            Assert.AreEqual(model.Fields[1].EntityName, "Person");
            Assert.AreEqual(model.Fields[1].NetType, "String");
            Assert.AreEqual(model.Fields[1].SqlType, SQLType.VARCHAR);
            Assert.AreEqual(model.Fields[1].Type, FieldType.SIMPLE);

            Assert.AreEqual(model.Fields[2].Name, "Sex");
            Assert.AreEqual(model.Fields[2].Namespace, "Migrator.Tests.Domain");
            Assert.AreEqual(model.Fields[2].EntityName, "Person");
            Assert.AreEqual(model.Fields[2].NetType, "Sex");
            Assert.AreEqual(model.Fields[2].SqlType, SQLType.INT);
            Assert.AreEqual(model.Fields[2].Type, FieldType.REFERENCE);

            Assert.AreEqual(model.Fields[3].Name, "Scores");
            Assert.AreEqual(model.Fields[3].Namespace, "System");
            Assert.AreEqual(model.Fields[3].EntityName, "Person");
            Assert.AreEqual(model.Fields[3].NetType, "Int32");
            Assert.AreEqual(model.Fields[3].SqlType, SQLType.INT);
            Assert.AreEqual(model.Fields[3].Type, FieldType.SIMPLE_LIST);

            Assert.AreEqual(model.Fields[4].Name, "Pets");
            Assert.AreEqual(model.Fields[4].Namespace, "Migrator.Tests.Domain");
            Assert.AreEqual(model.Fields[4].EntityName, "Person");
            Assert.AreEqual(model.Fields[4].NetType, "Pet");
            Assert.AreEqual(model.Fields[4].SqlType, SQLType.INT);
            Assert.AreEqual(model.Fields[4].Type, FieldType.REFERENCE_LIST);

            Assert.AreEqual(model.Fields[5].Name, "Strings");
            Assert.AreEqual(model.Fields[5].Namespace, "System");
            Assert.AreEqual(model.Fields[5].EntityName, "Person");
            Assert.AreEqual(model.Fields[5].NetType, "String");
            Assert.AreEqual(model.Fields[5].SqlType, SQLType.VARCHAR);
            Assert.AreEqual(model.Fields[5].Type, FieldType.SIMPLE_LIST);
        }
    }
}