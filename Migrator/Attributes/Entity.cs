using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrator
{
    public class Entity : Attribute
    {
        public Entity()
        {
        }

        public Entity(string name)
        {
            Name = name;
        }

        public string Name
        {
            get { return this.Name; }
            set { this.Name = value; }
        }
    }
}
