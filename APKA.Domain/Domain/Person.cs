using Migrator.Attributes;
using System;

namespace APKA.Domain
{
    [Entity]

    public class Person
    {
        [Required(43.3f)]

        public int Age { get; set; }

        [Length()]
        public DateTime Birthday { get; set; }
    }
}
