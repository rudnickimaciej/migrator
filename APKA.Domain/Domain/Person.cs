using Migrator.Attributes;
using System;

namespace APKA.Domain
{
    [Entity]

    public class Person
    {

        public int Age { get; set; }

        public DateTime Birthday { get; set; }
    }
}
