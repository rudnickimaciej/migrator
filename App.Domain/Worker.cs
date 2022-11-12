using Migrator.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain
{
    [Entity]
    class Worker
    {
        [Length(300)]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public List<Client> Client { get; set; }
        public Worker Supervisor { get; set; }

    }
}
