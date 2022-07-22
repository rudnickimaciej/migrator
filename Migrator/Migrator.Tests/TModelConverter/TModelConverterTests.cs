using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Migrator.Commons;

namespace Migrator.Tests.TModelConverterNamespace
{
    internal class TModelConverterTests
    {
        internal class Sex { }
        internal class Pet { }
        internal class Person
        {
            public int Age { get; set; }
            public string Name { get; set; }
            public Sex Sex { get; set; }
            public List<int> Scores { get; set; }
            public List<Pet> Pets { get; set; }

        }
        [Test]
        public void ConvertType()
        {

          TModel model =  TModelConverter.ConvertTypeToTypeModel(typeof(Person));

        }
    }
}
