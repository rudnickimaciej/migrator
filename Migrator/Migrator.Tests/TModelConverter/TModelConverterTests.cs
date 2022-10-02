using Migrator.Attributes;
using Migrator.Commons;
using Migrator.Tests.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;

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
        [Length(60)]
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

        #region enums
    
        #endregion enums
    }
    internal enum Level
    {
        Low,
        Medium,
        High
    }
}

namespace Migrator.Tests.TModelConverterNamespace
{
    internal class TModelConverterTests
    {
       
        [TestCase(0, "Person","Age", "System","Int32",SQLType.INT, FieldType.SIMPLE)]
        [TestCase(1, "Person", "FieldName", "System", "String", SQLType.VARCHAR, FieldType.SIMPLE)]
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
    }
}