using Migrator.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrator.Tests.Migrator.Cases.UpdateSchema.OldSchema
{
    [Entity]

    internal class Gender
    {
        public string Name { get; set; }
    }

    [Entity]
    internal class Pet
    {
        public string Name { get; set; }
        public Gender Gender { get; set; }

    }
    internal enum Level
    {
        Low,
        Medium
    }

    [Entity]
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
        public Gender Gender { get; set; }

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
}
