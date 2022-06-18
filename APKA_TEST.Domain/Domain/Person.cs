using Migrator;
using System;

namespace APKA_TEST.Domain
{
    [Entity]
    public class Person
    {
        public int Age { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
    }
}
