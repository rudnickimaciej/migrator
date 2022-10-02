using Migrator.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APKA.Domain2
{
    [Entity]
    public class ClientOrder
    {
        public Client Client { get; set; }
        public Order Order { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
