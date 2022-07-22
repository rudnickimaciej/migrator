using Migrator;
using System;

namespace APKA.Domain
{
    [Entity]
    public class Person
    {
        [Required]
        public int Age { get; set; }

        [Length(29)]
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
    }
}
