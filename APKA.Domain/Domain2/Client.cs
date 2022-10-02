using Migrator.Attributes;
using System;
using System.Collections.Generic;

namespace APKA.Domain2
{
    [Entity]
    public class Client
    {

        [Length(5)]
        public char Name { get; set; }
        public char Name2 { get; set; }
        public string LastName { get; set; }
        [Length(200)]
        public List<string> EmailAddress { get; set; }
        public List<Phone> Phones { get; set; }
        public Worker AssignedWorker { get; set; }

    }
}
