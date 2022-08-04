using Migrator;
using Migrator.Commons.Attributes;
using System;
using System.ComponentModel;

namespace APKA.Domain
{
    [Entity]

    public class Person
    {
        [Required(2)]
        public int Age { get; set; }

        [Length(39)]
        public DateTime Birthday { get; set; }
    }
}
