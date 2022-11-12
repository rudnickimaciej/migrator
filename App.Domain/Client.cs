using Migrator.Attributes;
using System;
using System.Collections.Generic;

namespace App.Domain
{
    [Entity]
    public class Client
    {
        [Length(150)]
        public string FirstName { get; set; }
        [Length(200)]
        public string LastName { get; set; }
        public DateTime Birtday { get; set; }
        public List<Order> Orders { get; set; }
    }
}
