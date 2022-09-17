using Migrator.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APKA.Domain2
{
    [Entity]

    public class Order
    {
        public int Id { get; set; }
        [Length(100)]
        public string Name { get; set; }
        public float Price { get; set; }
    }
}
