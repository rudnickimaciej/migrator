using Migrator.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APKA.Domain2
{
    [Entity]

    public class Worker
    {
        public int Id { get; set; }
        public int Name { get; set; }
    }
}
