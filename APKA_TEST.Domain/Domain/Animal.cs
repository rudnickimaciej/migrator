using Migrator.Core;
using System;
using System.Collections.Generic;

namespace APKA_TEST.Domain
{
    [Entity]
    public class Animal
    {
        public List<Person> Persons{ get; set; }
        public int Paws { get; set; }
        public string Name { get; set; }
        public DateTime Born { get; set; }
        public List<int> ListofInts { get; set; }
        public Person Person { get; set; }
        public Person Person2 { get; set; }


    }
}
